﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.Models;
using QLNS.ModelsParameter.Admin;
using QLNS.ModelsParameter.Catalog;
using QLNS.ModelsParameter.Producer;
using QLNS.ModelsParameter.Product;
using QLNS.ModelsParameter.SupplyInvoice;
using QLNS.ModelsParameter.SupplyList;
using QLNS.ViewModels.Admin;
using System.Text.RegularExpressions;

namespace QLNS.Controllers
{
	public class AdminController : Controller
	{
		private readonly IAdmin _admin;
		private readonly IUser _user;
		private readonly ICatalog _catalog;
		private readonly IProduct _product;
		private readonly IProducer _producer;
		private readonly ISupplyList _supplyList;
		private readonly ISupplyInvoice _supplyInvoice;
		private readonly IImportDetail _importDetail;
		//private readonly IOrder _order;
		//private readonly IOrdered _ordered;
		private readonly IBoardnew _boardnew;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public AdminController(IAdmin admin, IUser user, ICatalog catalog, IProduct product, /*IOrder order, IOrdered ordered,*/
			IBoardnew boardnew, IWebHostEnvironment webHostEnvironment, IProducer producer, ISupplyList supplyList,
			ISupplyInvoice supplyInvoice, IImportDetail importDetail)
		{
			_admin = admin;
			_user = user;
			_catalog = catalog;
			_product = product;
			_producer = producer;
			//_order = order;
			//_ordered = ordered;
			_boardnew = boardnew;
			_supplyList = supplyList;
			_supplyInvoice = supplyInvoice;
			_importDetail = importDetail;
			_webHostEnvironment = webHostEnvironment;
		}
		private bool CheckRole()
		{
            string role = HttpContext.Session.GetString("Role"); ;
            if (role != null && role.Equals("ADMIN")) return true;
			else return false;

        }
		//private readonly 
		public IActionResult Index()
		{
			if(!CheckRole()) return RedirectToAction("Error", "Home");
            return View();
        }
		public async Task<IActionResult> Admin() 
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            List<AdminDTO> admins = await _admin.GetAdmins();
			AdminViewModel Model = new AdminViewModel(){
			  Admins = admins,
			};
			return View(Model);
		}
		public async Task<IActionResult> User() 
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            List<UserDTO> users = await _user.GetUsers();
			UserViewModel Model = new UserViewModel() {
				Users = users,
			};
			return View(Model);
		}
		public async Task<IActionResult> Cate()
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
			List<CatalogDTO> catalogs = await _catalog.GetAllCatalog();
			CatalogViewModel Model = new CatalogViewModel() 
			{
				Catalogs = catalogs,
			};
			return View(Model);
        }
		public async Task<IActionResult> Product()
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            List<CatalogDTO> catalogs = await _catalog.GetAllCatalog();
			List<ProductDTO> products = await _product.GetAllProducts();
			List<SupplyListDTO> supplyLists = await _supplyList.GetAllSupplyList();
			ProductViewModel Model = new ProductViewModel() { 
				Catalogs = catalogs,
				Products = products,
				SupplyList = supplyLists,
			};
			return View(Model);
        }
		public async Task<IActionResult> Producer()
		{
			if (!CheckRole()) return RedirectToAction("Error", "Home");
			List<ProducerDTO> producers = await _producer.GetAllProducer();
			ProducerViewModel model = new ProducerViewModel() { Producers = producers };
			return View(model);
		}
		public async Task<IActionResult> SupplyInvoice()
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
			List<ProductDTO> products = new List<ProductDTO>();
			List<SupplyListDTO> supplyLists = await _supplyList.GetAllSupplyList();
			List<ProducerDTO> producers = await _producer.GetAllProducer();
			foreach(var sp in supplyLists)
			{
				ProductDTO pr = await _product.GetProductById(sp.ProductId);
				if(pr!=null)
                products.Add(pr);
            }
			SupplyInvoiceViewModel model = new SupplyInvoiceViewModel()
			{
				Products = products,
				Producers = producers,
				SupplyList = supplyLists,
			};
			return View(model);
        }
		public async Task<IActionResult> News()
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
			List<Boardnew> boardnews = await _boardnew.GetBoardnews();
			NewsViewModel Model = new NewsViewModel() {
				Boardnews = boardnews,
			};
			return View(Model) ;
        }
		
		// Admin 
		public async Task<IActionResult> AddAdmin()
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            return View();
		}
		public async Task<IActionResult> AddAdminResult(string? admin_username, string? admin_password, string? admin_name,
			string? admin_email, string? admin_phone)
		{
			RequestCheckAdmin requestCheck = new RequestCheckAdmin() 
			{
				username = admin_username,
				email = admin_email,
				phone = admin_phone,
			};
			if(admin_username.IsNullOrEmpty()|| admin_password.IsNullOrEmpty()|| admin_name.IsNullOrEmpty())
			{
                HttpContext.Session.SetString("errorMsg", "Không đươc bỏ trống ô");
				return RedirectToAction("AddAdmin", "Admin");
            }
			if(!admin_email.Contains("@")|| admin_email.IsNullOrEmpty())
			{
                HttpContext.Session.SetString("errorMsg", "Email sai cú pháp");
                return RedirectToAction("AddAdmin", "Admin");
            }
			if(admin_phone.IsNullOrEmpty() || !Regex.IsMatch(admin_phone, @"^[\d\.\-\+]+$"))
			{
                HttpContext.Session.SetString("errorMsg", "Email sai cú pháp");
                return RedirectToAction("AddAdmin", "Admin");
            }
			if(await _admin.CheckExits(requestCheck))
			{
                HttpContext.Session.SetString("errorMsg", "Các thông tin về username hoặc email, số điện thoại bị trùng");
                return RedirectToAction("AddAdmin", "Admin");
            }
			AddAdmin request = new AddAdmin() { 
				Username = admin_username,
				Email = admin_email,
				Name = admin_name,
				Status = 1,
				Password = admin_password,	
				Phone = admin_phone,
			};
			bool check = await _admin.CreateAdmin(request);
			if(check)
			{
				HttpContext.Session.Remove("errorMsg");
                return RedirectToAction("Admin", "Admin");	
			}
			else
			{
                HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra, hãy thử lại");
                return RedirectToAction("AddAdmin", "Admin");
            }
		}
		public async Task<IActionResult> DeleteAdmin(int id)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            bool check = await _admin.DeleteAdmin(id);
			if(check)
			{
                HttpContext.Session.Remove("errorMsg");
                return RedirectToAction("Admin", "Admin");
            }
			else
			{
                HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
                return RedirectToAction("Admin", "Admin");
            }
		}
		public async Task<IActionResult> EditAdmin(int id)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            AdminDTO admin =  await _admin.GetAdmin(id);
			EditAdminViewModel Model = new EditAdminViewModel() { 
			Admin = admin,
			};
			return View(Model);
		}
		public async Task<IActionResult> EditAdminResult(int id, string? password, string? name, string? phone, string? email)
		{
            if (password.IsNullOrEmpty() || name.IsNullOrEmpty())
            {
                HttpContext.Session.SetString("errorMsg", "Không đươc bỏ trống ô");
                return RedirectToAction("EditAdmin", "Admin", new { id = id });
            }
            if (!email.Contains("@") || email.IsNullOrEmpty())
            {
                HttpContext.Session.SetString("errorMsg", "Email sai cú pháp");
                return RedirectToAction("EditAdmin", "Admin", new { id = id });
            }
            if (phone.IsNullOrEmpty() || !Regex.IsMatch(phone, @"^[\d\.\-\+]+$"))
            {
                HttpContext.Session.SetString("errorMsg", "Email sai cú pháp");
                return RedirectToAction("EditAdmin", "Admin", new { id = id });
            }
			UpdateAdmin request = new UpdateAdmin() 
			{ 
				Id = id,
				Name = name, 
				Password = password,
				Phone = phone,
				Email = email,
			};
			bool check = await _admin.UpdateAdmin(request);
            if (check)
            {
                HttpContext.Session.Remove("errorMsg");
                return RedirectToAction("Admin", "Admin");
            }
            else
            {
                HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra, hãy thử lại");
                return RedirectToAction("EditAdmin", "Admin", new { id = id });
            }
        }
		public async Task<IActionResult> UnLockAdmin(int id)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            bool check = await _admin.UnLockAdmin(id);
            if (check)
            {
                HttpContext.Session.Remove("errorMsg");
                return RedirectToAction("Admin", "Admin");
            }
            else
            {
                HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
                return RedirectToAction("Admin", "Admin");
            }
        }
		//User
		public async Task<IActionResult> LockUser(string username)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            bool check = await _user.LockUser(username);
            if (check)
            {
                HttpContext.Session.Remove("errorMsg");
                return RedirectToAction("User", "Admin");
            }
            else
            {
                HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
                return RedirectToAction("User", "Admin");
            }
        }
        public async Task<IActionResult> UnLockUser(string username)
        {
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            bool check = await _user.UnLockUser(username);
            if (check)
            {
                HttpContext.Session.Remove("errorMsg");
                return RedirectToAction("User", "Admin");
            }
            else
            {
                HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
                return RedirectToAction("User", "Admin");
            }
        }
		// Catalog
		public async Task<IActionResult> AddCate()
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
			return View();
        }
		public async Task<IActionResult> AddCateResult(string? name)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
			List<CatalogDTO> catalogs = await _catalog.GetAllCatalog();
			if(name == null)
			{
                HttpContext.Session.SetString("errorMsg", "Không được bỏ trống thông tin");
                return RedirectToAction("AddCate", "Admin");
            }
			foreach(CatalogDTO item in catalogs) 
			{
				if(item.Name.ToLower() == name.ToLower())
				{
                    HttpContext.Session.SetString("errorMsg", "Thông tin bị trùng");
                    return RedirectToAction("AddCate", "Admin");
                }
			}
			bool check = await _catalog.AddCatalog(name);
			if (check)
			{
                HttpContext.Session.Remove("errorMsg");
                return RedirectToAction("Cate", "Admin");
			}
			else
			{
                HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
                return RedirectToAction("AddCate", "Admin");
            }
        }
		public async Task<IActionResult> RemoveCate(int id)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
			List<ProductDTO> list = await _product.GetProductsByCatalogId(id);
			if (list.Count != 0)
			{
                HttpContext.Session.SetString("errorMsg", "Không thể xóa");
                return RedirectToAction("Cate", "Admin");
            }
			bool check = await _catalog.DeleteCatalog(id);
            if (check)
            {
                HttpContext.Session.Remove("errorMsg");
                return RedirectToAction("Cate", "Admin");
            }
            else
            {
                HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
                return RedirectToAction("Cate", "Admin");
            }

        }
		public async Task<IActionResult> EditCate(int id)	
		{
			if (!CheckRole()) return RedirectToAction("Error", "Home");
			CatalogDTO catalog = await _catalog.GetCatalogById(id);
			EditCatalogViewModel model = new EditCatalogViewModel() 
			{
				Catalog = catalog,
			};
			return View(model);
		}
		public async Task<IActionResult> EditCateResult(int id, string name)
		{
			if (!CheckRole()) return RedirectToAction("Error", "Home");
			List<CatalogDTO> catalogs = await _catalog.GetAllCatalog();
			catalogs.RemoveAll(c => c.Id == id);
			if (name == null)
			{
				HttpContext.Session.SetString("errorMsg", "Không được bỏ trống thông tin");
				return RedirectToAction("AddCate", "Admin");
			}
			foreach (CatalogDTO item in catalogs)
			{
				if (item.Name.ToLower() == name.ToLower())
				{
					HttpContext.Session.SetString("errorMsg", "Thông tin bị trùng");
					return RedirectToAction("EditCate", "Admin", new { id = id });
				}
			}
			UpdateCatalogRequest request = new UpdateCatalogRequest()
			{
				id = id,
				name = name,
			};
			bool check = await _catalog.UpdateCatalog(request);
			if (check)
			{
				HttpContext.Session.Remove("errorMsg");
				return RedirectToAction("Cate", "Admin");
			}
			else
			{
				HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
				return RedirectToAction("EditCate", "Admin", new { id = id });
			}
		}
		// Product
		public async Task<IActionResult> AddProduct()
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
			List<CatalogDTO> catalogs = await _catalog.GetAllCatalog();
			AddProductViewModel model = new AddProductViewModel() { Catalogs = catalogs };
			return View(model);
        }
		[HttpPost]
		public async Task<IActionResult> AddProductResult([FromForm] InputProduct input)
		{
			if (!CheckRole()) return RedirectToAction("Error", "Home");
			if (input.product_name.IsNullOrEmpty() || input.product_desc.IsNullOrEmpty() || input.product_content.IsNullOrEmpty()
				|| input.product_unit.IsNullOrEmpty())
			{
				HttpContext.Session.SetString("errorMsg", "Không đươc bỏ trống ô");
				return RedirectToAction("AddProduct", "Admin");
			}
			try
			{
				if (input.imageFile != null && input.imageFile.Length > 0)
				{
					string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images/products/img-test");
					string fileName = Path.GetFileName(input.imageFile.FileName);
					string filePath = Path.Combine(uploadDir, fileName);
					
					InputProductRequest request = new InputProductRequest()
					{
						CatalogId = input.product_cate,
						Content = input.product_content,
						Created = input.product_day,
						Description = input.product_desc,
						Discount = input.product_discount,
						ExpiryDate = input.product_expixy,
						Name = input.product_name,
						ImageLink = fileName,
						Price = input.product_price,
						Quantity = 0,
						Status = input.product_status,
						Unit = input.product_unit,
					};
					bool check = await _product.CreateProduct(request);
					if (check)
					{
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await input.imageFile.CopyToAsync(fileStream);
                        }
                        HttpContext.Session.Remove("errorMsg");
						return RedirectToAction("Product", "Admin");
						
					}
					else
					{
						HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
						return RedirectToAction("AddProduct", "Admin");
						
					}
					
				}
				else
				{
					HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra với nhập ảnh");
					return RedirectToAction("AddProduct", "Admin");
				}
			}
			catch
			{
				HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
				return RedirectToAction("AddProduct", "Admin");
			}
		}
		public async Task<IActionResult> EditProduct(int id)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
			ProductDTO product = await _product.GetProductById(id);
			List<CatalogDTO> catalogs = await _catalog.GetAllCatalog();
			EditProductViewModel model = new EditProductViewModel()
			{
				Catalogs = catalogs,
				Product = product,
			};
			return View(model);
        }
		[HttpPost]
		public async Task<IActionResult> EditProductResult([FromForm] UpdateProduct input)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            if (input.Name.IsNullOrEmpty() || input.Description.IsNullOrEmpty() || input.Content.IsNullOrEmpty()
                || input.Unit.IsNullOrEmpty())
            {
                HttpContext.Session.SetString("errorMsg", "Không đươc bỏ trống ô");
                return RedirectToAction("EditProduct", "Admin", new { id = input.Id });
            }
			try
			{
                ProductDTO product = await _product.GetProductById(input.Id);
                UpdateProductRequest request = new UpdateProductRequest()
                {
                    Id = input.Id,
                    CatalogId = input.CatalogId,
                    Content = input.Content,
                    Description = input.Description,
                    Discount = input.Discount,
                    ExpiryDate = input.ExpiryDate,
                    Name = input.Name,
                    Price = input.Price,
                    Status = input.Status,
                    Unit = input.Unit,
                };
                if (input.imageFile == null && input.imageFile.Length == 0)
				{
					request.ImageLink = product.ImageLink;
                    bool check = await _product.UpdateProduct(request);
                    if (check)
                    {
                        HttpContext.Session.Remove("errorMsg");
                        return RedirectToAction("Product", "Admin");

                    }
                    else
                    {
                        HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
                        return RedirectToAction("EditProduct", "Admin", new { id = input.Id });

                    }
				}
				else
				{
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images/products/img-test");
					string fileNameDelete = Path.GetFileName(product.ImageLink);
					string filePathDelete = Path.Combine(uploadDir, fileNameDelete);
                    string fileName = Path.GetFileName(input.imageFile.FileName);
                    string filePath = Path.Combine(uploadDir, fileName);
                    request.ImageLink = fileName;
                    bool check = await _product.UpdateProduct(request);
                    if (check)
                    {
                        HttpContext.Session.Remove("errorMsg");
                        if (System.IO.File.Exists(filePathDelete))
                        {
                            // Xóa tệp
                            System.IO.File.Delete(filePathDelete);
                        }
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await input.imageFile.CopyToAsync(fileStream);
                        }
                        return RedirectToAction("Product", "Admin");
                        
                    }
                    else
                    {
                        HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
                        return RedirectToAction("EditProduct", "Admin", new { id = input.Id });

                    }
                }

            }
			catch
			{
                HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
                return RedirectToAction("EditProduct", "Admin", new { id = input.Id });
            }
        }
		public async Task<IActionResult> DeleteProduct(int id)
		{
            ProductDTO product = await _product.GetProductById(id);
			string ImageName = product.ImageLink;
            if (!CheckRole()) return RedirectToAction("Error", "Home");
			bool check = await _product.DeleteProduct(id);
			if ( check)
			{
				
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images/products/img-test");
                string fileNameDelete = Path.GetFileName(ImageName);
                string filePathDelete = Path.Combine(uploadDir, fileNameDelete);
                if (System.IO.File.Exists(filePathDelete))
                {
                    // Xóa tệp
                    System.IO.File.Delete(filePathDelete);
                }
                HttpContext.Session.Remove("errorMsg");
                return RedirectToAction("Product", "Admin");
			}
			else
			{
                HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
                return RedirectToAction("Product", "Admin");
            }
        }
		// Produucer
		public IActionResult AddProducer()
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
			return View();
        }
		public async Task<IActionResult> AddProducerResult([FromForm] CreateProducerRequest request)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
			if(request.Name.IsNullOrEmpty()||request.Numphone.IsNullOrEmpty()||request.Address.IsNullOrEmpty()
				|| request.Email.IsNullOrEmpty())
			{
                HttpContext.Session.SetString("errorMsg", "Không được bỏ trống");
                return RedirectToAction("AddProducer", "Admin");
            }
			try
			{
				bool check = await _producer.CreateProducer(request);
				if (check)
				{
                    HttpContext.Session.Remove("errorMsg");
                    return RedirectToAction("Producer", "Admin");
                }
				else
				{
                    HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
                    return RedirectToAction("AddProducer", "Admin");
                }
			}
			catch
			{
                HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
                return RedirectToAction("AddProducer", "Admin");
            }
        }
		public async Task<IActionResult> DeleteProducer(int id)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            try
			{ 
				bool check = await _producer.DeleteProducer(id);
				if (check)
				{
                    HttpContext.Session.Remove("errorMsg");
                    return RedirectToAction("Producer", "Admin");
                }
				else
				{
                    HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
                    return RedirectToAction("Producer", "Admin");
                }
			}
			catch
			{
                HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
                return RedirectToAction("Producer", "Admin");
            }
		}
		public async Task<IActionResult> EditProducer(int id)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
			ProducerDTO producer = await _producer.GetProducerById(id);
			EditProducerViewModel model = new EditProducerViewModel() { Producer = producer };
			return View(model);
        }
		public async Task<IActionResult> EditProducerResult([FromForm] ProducerDTO request)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
			try
			{
                if (request.Name.IsNullOrEmpty() || request.Numphone.IsNullOrEmpty() || request.Address.IsNullOrEmpty()
                || request.Email.IsNullOrEmpty())
                {
                    HttpContext.Session.SetString("errorMsg", "Không được bỏ trống");
                    return RedirectToAction("AddProducer", "Admin");
                }
                bool check = await _producer.UpdateProducer(request);
				if (check)
				{
                    HttpContext.Session.Remove("errorMsg");
                    return RedirectToAction("Producer", "Admin");
                }
				else
				{
                    HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
                    return RedirectToAction("EditProducer", "Admin");
                }
			}
			catch
			{
                HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
                return RedirectToAction("EditProducer", "Admin");
            }
        }
		// Supply List
		public async Task<IActionResult> AddSupplyList(int id)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
			try
			{
				CreateSupplyListRequest request = new CreateSupplyListRequest()
				{
					ProductId = id,
					Quantity = 100,
				};
				bool check = await _supplyList.CreateSupplyList(request);
				if (check)
				{
                    HttpContext.Session.Remove("errorMsg");
                    return RedirectToAction("Product", "Admin");
				}
				else
				{
                    HttpContext.Session.SetString("errorMsg", "Chọn Nhập không thành công");
                    return RedirectToAction("Product", "Admin");
                }
			}
			catch
			{
                HttpContext.Session.SetString("errorMsg", "Chọn Nhập không thành công");
                return RedirectToAction("Product", "Admin");
            }
        }
		public async Task<IActionResult> DeleteSupplyList(int id)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            try
            {
                bool check = await _supplyList.DeleteSupplyList(id);
                if (check)
                {
                    HttpContext.Session.Remove("errorMsg");
                    return RedirectToAction("Product", "Admin");
                }
                else
                {
                    HttpContext.Session.SetString("errorMsg", "Hủy Nhập không thành công");
                    return RedirectToAction("Product", "Admin");
                }
            }
            catch
            {
                HttpContext.Session.SetString("errorMsg", "Hủy Nhập không thành công");
                return RedirectToAction("Product", "Admin");
            }
        }
		public async Task<IActionResult> EditSupplyList(int id, int quantity)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
			try
			{
				CreateSupplyListRequest request = new CreateSupplyListRequest()
				{
					ProductId = id,
					Quantity = quantity,
				};
				bool check = await _supplyList.UpdateSupplyList(request);
				if (check)
				{
                    HttpContext.Session.Remove("errorMsg");
                    return RedirectToAction("SupplyInvoice", "Admin");
                }
				else
				{
                    HttpContext.Session.SetString("errorMsg", "Sửa Nhập không thành công");
                    return RedirectToAction("SupplyInvoice", "Admin");
                }
			}
			catch
			{
                HttpContext.Session.SetString("errorMsg", "Sửa Nhập không thành công");
                return RedirectToAction("SupplyInvoice", "Admin");
            }
        }
		public async Task<IActionResult> Supply([FromForm]int producer, [FromForm] DateOnly create)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            try
            {
				string Username = HttpContext.Session.GetString("Username");
				List<AdminDTO> admins = await _admin.GetAdmins();
				AdminDTO admin = admins.Where(a => a.Username == Username).FirstOrDefault();
				CreateSupplyInvoiceRequest request = new CreateSupplyInvoiceRequest()
				{
					AdId = admin.Id,
					ProducerId = producer,
					SupplyTime = create,
                };
				bool check = await _supplyInvoice.CreateSupplyInvoice(request);
                if (check)
                {
                    HttpContext.Session.Remove("errorMsg");
                    return RedirectToAction("SupplyInvoice", "Admin");
                }
                else
                {
                    HttpContext.Session.SetString("errorMsg", "Nhập không thành công");
                    return RedirectToAction("SupplyInvoice", "Admin");
                }
            }
            catch
            {
                HttpContext.Session.SetString("errorMsg", "Nhập không thành công");
                return RedirectToAction("SupplyInvoice", "Admin");
            }
        }
		public async Task<IActionResult> ShowSupply()
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
			List<SupplyInvoiceDTO> supplyInvoices = await _supplyInvoice.GetAllSupplyInvoice();
            supplyInvoices = supplyInvoices.OrderByDescending(s => s.SupplyTime).ToList();
            List<AdminDTO> admins = await _admin.GetAdmins();
			List<ProducerDTO> producers = await _producer.GetAllProducer();
			ShowSupplyViewModel model = new ShowSupplyViewModel()
			{
				adminList = admins,
				invoiceList = supplyInvoices,
				producerList = producers,
			};
			return View(model);
        }
		public async Task<IActionResult> ShowImportDetail(int id)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
			SupplyInvoiceDTO si = await _supplyInvoice.GetSupplyInvoiceById(id);
			AdminDTO ad = await _admin.GetAdmin(si.AdId);
			ProducerDTO producer = await _producer.GetProducerById(si.ProducerId);
			List<ImportDetailDTO> imports = await _importDetail.GetImportDetailsBySupplyId(id);
			List<ProductDTO> products = await _product.GetAllProducts();
			ImportDetailViewModel model = new ImportDetailViewModel()
			{
				Admin = ad,
				Imports = imports,
				Producer = producer,
				Products = products,
				SupplyInvoice = si,
			};
			return View(model);
        }
    }
}
