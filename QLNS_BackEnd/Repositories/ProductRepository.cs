using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Models;
using QLNS_BackEnd.ModelsParameter.Cart;
using QLNS_BackEnd.ModelsParameter.Product;
using QLNS_BackEnd.Singleton;
using System.Data;
using System.Diagnostics;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QLNS_BackEnd.Repositories   
{
    public class ProductRepository : IProduct
    {
        private readonly IConfiguration _configuration;
        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool AddProduct(InputProductRequest request)
		{
            try
            {
                Product product = SingletonAutoMapper.GetInstance().Map<Product>(request);
                product.Quantity = 0;
                SingletonDataBridge.GetInstance().Products.Add(product);
                SingletonDataBridge.GetInstance().SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
		}

        public bool CheckPurchase(RequestCheckCart request)
        {
            Console.WriteLine(request.userId.ToString()+"  aa  "+request.productId.ToString());
            List<Order> orders = SingletonDataBridge.GetInstance().Orders.ToList();
            List<Ordered> ordereds = SingletonDataBridge.GetInstance().Ordereds.ToList();
            var HasPurchased = orders
                                .Where(order => order.UserId == request.userId && order.Status == 2)
                                .Any(order => ordereds.Any(ordered => ordered.OrderId == order.Id && ordered.ProductId == request.productId));
            return  HasPurchased;
        }
        public bool DeleteProduct(int id)
		{
            try
            {
                if (SingletonDataBridge.GetInstance().Ordereds.Any(o => o.ProductId == id))
                    return false;

                Product product = SingletonDataBridge.GetInstance().Products.Find(id);
                if(product.Quantity != 0) return false;
                SingletonDataBridge.GetInstance().Products.Remove(product);
                SingletonDataBridge.GetInstance().SaveChanges();    
				return true;
            }
            catch
            {
                return false;
            }
		}

		public async Task<List<ProductDTO>> GetAllProducts()
        {
            return SingletonAutoMapper.GetInstance().Map<List<ProductDTO>>(
                await SingletonDataBridge.GetInstance().Products.ToListAsync());//.Where(p => p.Status == 1)
        }

        public ProductDTO GetProductById(int id)
        {
            return SingletonAutoMapper.GetInstance().Map<ProductDTO>(
               SingletonDataBridge.GetInstance().Products.Where(p => p.Id == id).FirstOrDefault());
        }

        public async Task<List<ProductDTO>> GetProductsBestSelling()
        {
            List<ProductDTO> products = new List<ProductDTO>();
            List<int> bestSellingProductIds = new List<int>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_BEST_SELLING", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                ProductDTO pr = GetProductById(reader.GetInt32(0));
                                if(pr != null && pr.Status == 1) products.Add(pr);
                            }
                        }
                    }
                }
                return products;

            }catch(Exception ex) 
            {
                return null;
            }
        }

        public List<ProductDTO> GetProductsByCatalogId(int id)
        {
            return SingletonAutoMapper.GetInstance().Map<List<ProductDTO>>(
                SingletonDataBridge.GetInstance().Products.Where(p => p.Status == 1 && p.CatalogId == id).ToList());
        }

        public List<ProductDTO> GetRecommendedProducts(int id)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "python";
            start.Arguments = $"\"D:\\source\\repos\\QLNS\\Recommendation\\predict.py\" {id}"; // Truyền id
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            start.CreateNoWindow = true;
            // Chạy process
            using (Process process = Process.Start(start))
            {
                using (System.IO.StreamReader reader = process.StandardOutput)
                {
                    try
                    {
                        process.WaitForExit();
                        string result = reader.ReadToEnd().Trim();
                        Console.WriteLine(result);
                        int[] numbers = result.Trim(new char[] { '[', ']' })
                              .Split(',')
                              .Select(int.Parse)
                              .ToArray();
                       
                        List<ProductDTO> products = new List<ProductDTO>();
                        foreach (int number in numbers)
                        {
                            ProductDTO pr = GetProductById(number);
                            if(pr != null && pr.Status == 1)
                            products.Add(pr);
                        }
                        process.Close();
                        return products;
                    }
                    catch(Exception  ex) 
                    {
                        Console.WriteLine("Errors: " + ex.Message);
                    }
                    
                }
                //string errors = process.StandardError.ReadToEnd();
                //if (!string.IsNullOrEmpty(errors))
                //{
                //    Console.WriteLine("Errors: " + errors);
                //}

                //process.WaitForExit();
                process.Close(); 
            }
            return null;
        }

		public List<ProductDTO> GetRecommendedProductsByRated(int id)
		{
			ProcessStartInfo start = new ProcessStartInfo();
			start.FileName = "python";
			start.Arguments = $"\"D:\\source\\repos\\QLNS\\Recommendation\\rating_predict.py\" {id}"; // Truyền id
			start.UseShellExecute = false;
			start.RedirectStandardOutput = true;
			start.RedirectStandardError = true;
			start.CreateNoWindow = true;
			// Chạy process
			using (Process process = Process.Start(start))
			{
				using (System.IO.StreamReader reader = process.StandardOutput)
				{
					try
					{
                        process.WaitForExit();
                        string result = reader.ReadToEnd().Trim();
						Console.WriteLine(result);
						int[] numbers = result.Trim(new char[] { '[', ']' })
							  .Split(',')
							  .Select(int.Parse)
							  .ToArray();

						List<ProductDTO> products = new List<ProductDTO>();
						foreach (int number in numbers)
						{
                            ProductDTO pr = GetProductById(number);
                            if (pr != null && pr.Status==1)
                                products.Add(pr);
                        }
						process.Close();
						return products;
					}
					catch (Exception ex)
					{
						Console.WriteLine("Errors: " + ex.Message);
					}

				}
				process.Close();
			}
			return null;
		}

		public bool UpdateProduct(UpdateProductRequest request)
		{
            try
            {
                Product product = SingletonDataBridge.GetInstance().Products.Find(request.Id);
                product.Name = request.Name;
                product.Description = request.Description;  
                product.Status = request.Status;
                product.CatalogId = request.CatalogId; 
                product.Discount = request.Discount;
                product.Price = request.Price;
                product.Content = request.Content;
                product.ImageLink = request.ImageLink;
                product.ExpiryDate = request.ExpiryDate;
                product.Unit = request.Unit;
                SingletonDataBridge.GetInstance().Products.Update(product);
                SingletonDataBridge.GetInstance().SaveChangesAsync();
				return true;
            }
            catch
            {
                return false;
            }
		}
	}
}
