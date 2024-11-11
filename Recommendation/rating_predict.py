import pickle
import os
import sys
import json
import pandas as pd
from surprise import Dataset, Reader, SVD
from surprise.model_selection import train_test_split

current_dir = os.path.dirname(os.path.abspath(__file__))
pkl_file_path = os.path.join(current_dir, 'svd_model.pkl')
df_file_path = os.path.join(current_dir, 'Rating.csv')
df = pd.read_csv(df_file_path)

reader = Reader(rating_scale=(1, 5))
data = Dataset.load_from_df(df[['id_user', 'product_id', 'rating']], reader)
trainset = data.build_full_trainset()

# Khởi tạo và huấn luyện model SVD
model = SVD()
model.fit(trainset)

with open(pkl_file_path, 'wb') as file:
    pickle.dump(model, file)


def recommend_products(user_id, n_recommendations=5):
    # Lấy tất cả các sản phẩm
    all_product_ids = df['product_id'].unique()

    user_items = df[df['id_user'] == user_id]['product_id']
    user_unrated_items = [item for item in all_product_ids if item not in user_items.values]

    # Dự đoán rating cho các sản phẩm chưa đánh giá
    predictions = []
    for item in user_unrated_items:
        pred = model.predict(user_id, item)
        predictions.append((item, pred.est))

    # Sắp xếp sản phẩm theo rating dự đoán
    predictions.sort(key=lambda x: x[1], reverse=True)

    # Chọn N sản phẩm hàng đầu
    top_n_products = predictions[:n_recommendations]

    return [product for product, _ in top_n_products]
id = int(sys.argv[1])
recommended_products = recommend_products(user_id=id)
#print(f"Sản phẩm gợi ý cho User_ID 3: {recommended_products}")
print(recommended_products)