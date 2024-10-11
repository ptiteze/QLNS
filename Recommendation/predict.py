import pickle

with open('data_indices.pkl', 'rb') as file:
    data = pickle.load(file)

grouped_data = data['grouped_data']
indices = data['indices']

def recommend_products(product_id, grouped_data, indices):
    index = grouped_data[grouped_data['id'] == product_id].index[0]
    recommended_product_ids = grouped_data['id'].iloc[indices[index]].tolist()
    recommended_product_ids = [pid for pid in recommended_product_ids if pid != product_id]
    return recommended_product_ids
x = 22
recommendation = recommend_products(x, grouped_data, indices)
#print(f"Gợi ý cho sản phẩm {x}: {recommendation}")
print(recommendation)