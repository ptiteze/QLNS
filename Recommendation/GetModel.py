import pyodbc
import pandas as pd
from sklearn.preprocessing import MultiLabelBinarizer
import numpy as np
from sklearn.neighbors import NearestNeighbors
from surprise import Dataset, Reader, SVD
from surprise.model_selection import train_test_split

import pickle

#-------------------------------------------
# Kết nối tới SQL Server
conn = pyodbc.connect(
    'DRIVER={SQL Server};'
    'SERVER=DESKTOP-LL3CDGR;'   
    'DATABASE=QLNS3;' 
    'UID=sa;'           
    'PWD=nhitnho;'            
)
# ---------------- predict Cosine Simularity----------------------
query = "EXEC SP_DATA1"
df = pd.read_sql_query(query, conn)

# Ghi kết quả ra file Excel
# output_file = 'output.xlsx'
# df.to_excel(output_file, index=False)
grouped_data = df.groupby('id')['used'].apply(list).reset_index()
grouped_data.head(5)
mlb = MultiLabelBinarizer()
grouped_data['used_id_binary'] = list(mlb.fit_transform(grouped_data['used']))
# Thêm cột catalog
grouped_data = grouped_data.merge(df[['id', 'catalog']].drop_duplicates(), on='id')
print(grouped_data.head())
X = np.hstack([grouped_data[['catalog']], np.array(grouped_data['used_id_binary'].tolist())])
# Áp dụng KNN
knn = NearestNeighbors(n_neighbors=7, metric='cosine', algorithm='brute').fit(X)
distances, indices = knn.kneighbors(X)

with open('data_indices.pkl', 'wb') as file:
    pickle.dump({'grouped_data': grouped_data, 'indices': indices}, file)

# ---------------- predict Cosine Collabora----------------------

query2 = "EXEC SP_DATA2"
df2 = pd.read_sql_query(query2, conn)
reader = Reader(rating_scale=(1, 5))
data = Dataset.load_from_df(df2[['id_user', 'product_id', 'rating']], reader)
trainset, testset = train_test_split(data, test_size=0.2)
model = SVD()
model.fit(trainset)
with open('svd_model.pkl', 'wb') as file:
    pickle.dump({'model': model}, file)
# Đóng kết nối
conn.close()

print(f"Kết quả đã được lưu vào")