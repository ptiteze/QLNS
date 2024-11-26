import pickle
import os
import sys
import json

current_dir = os.path.dirname(os.path.abspath(__file__))
pkl_file_path = os.path.join(current_dir, 'data_indices.pkl')

with open(pkl_file_path, 'rb') as file:
    data = pickle.load(file)

grouped_data = data['grouped_data']
indices = data['indices']

def recommend_products(product_id, grouped_data, indices):
    index = grouped_data[grouped_data['id'] == product_id].index[0]
    recommended_product_ids = grouped_data['id'].iloc[indices[index]].tolist()
    recommended_product_ids = [pid for pid in recommended_product_ids if pid != product_id]
    return recommended_product_ids
id = int(sys.argv[1])
recommendation = recommend_products(id, grouped_data, indices)
print(json.dumps(recommendation))