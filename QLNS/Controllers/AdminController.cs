﻿using Azure.Core;
using DevExpress.ReportServer.ServiceModel.DataContracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.Models;
using QLNS.ModelsParameter.Admin;
using QLNS.ModelsParameter.Catalog;
using QLNS.ModelsParameter.Order;
using QLNS.ModelsParameter.Producer;
using QLNS.ModelsParameter.Product;
using QLNS.ModelsParameter.SupplyInvoice;
using QLNS.ModelsParameter.SupplyList;
using QLNS.ModelsParameter.User;
using QLNS.ViewModels.Admin;
using QLNS.ViewModels.Cart;
using QLNS.ViewModels.Order;
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
		private readonly IOrder _order;
		private readonly IOrdered _ordered;
		private readonly IBoardnew _boardnew;
		private readonly IAccount _acount;
		private readonly IUsed _used;
		private readonly ISale _sale;
		private readonly IRecommendation _recommendation;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public AdminController(IAdmin admin, IUser user, ICatalog catalog, IProduct product, IOrder order, IOrdered ordered,
			IBoardnew boardnew, IWebHostEnvironment webHostEnvironment, IProducer producer, ISupplyList supplyList,
			ISupplyInvoice supplyInvoice, IImportDetail importDetail, IAccount account, IUsed used, ISale sale, IRecommendation recommendation)
		{
			_admin = admin;
			_user = user;
			_catalog = catalog;
			_product = product;
			_producer = producer;
			_order = order;
			_ordered = ordered;
			_boardnew = boardnew;
			_supplyList = supplyList;
			_supplyInvoice = supplyInvoice;
			_importDetail = importDetail;
			_acount = account;
			_used = used;
			_recommendation = recommendation;
			_sale = sale;
			_webHostEnvironment = webHostEnvironment;
		}
		private bool CheckRole()
		{
            string role = HttpContext.Session.GetString("Role"); ;
            if (role != null && role.Equals("staff")) return true;
			else return false;

        }
        private bool CheckRoleAdmin()
        {
            string role = HttpContext.Session.GetString("Role"); ;
            if (role != null && role.Equals("admin")) return true;
            else return false;

        }
		//private readonly 
		public async Task<IActionResult> Index()
		{
			if (!CheckRole()&&!CheckRoleAdmin()) return RedirectToAction("Error", "Home");
			List<OrderDTO> orders = await _order.GetOrders();
			List<OrderDTO> orderslast = new List<OrderDTO>();
            DateOnly DbDaysAgo = DateOnly.FromDateTime(DateTime.Now.AddDays(-14));
            DateOnly sevenDaysAgo = DateOnly.FromDateTime(DateTime.Now.AddDays(-7));
			orderslast = orders.Where(o =>o.Sentdate >= DbDaysAgo && o.Sentdate < sevenDaysAgo && o.Status == 2).ToList();
            orders = orders.Where(o => o.Sentdate >= sevenDaysAgo && o.Status == 2).ToList();
            int num_order = orders.Count();
            var orderIds = orders.Select(o => o.Id).ToList();
            List<OrderedDTO> ordereds = new List<OrderedDTO>();
			foreach(int i in orderIds)
			{
				ordereds.AddRange(await _ordered.GetOrderedsByOrderId(i));
			}


			int totalPrice = 0/*, sum_user = 0*/;
			List<UserDTO> users = await _user.GetUsers();
			int num_user = users.Where(u=>u.Created>= sevenDaysAgo).Count();
			int sum_pr = 0, sum_1 = 0,sum_2 = 0, sum_3 = 0, sum_4 = 0;
			foreach(OrderedDTO or in ordereds)
			{
				sum_pr += or.Qty;
				ProductDTO pr = await _product.GetProductById(or.ProductId);
				totalPrice += or.Price*or.Qty;
				switch (pr.CatalogId)
				{
					case 1:
						sum_1 += or.Qty*or.Price;
						break;
                    case 2:
                        sum_2 += or.Qty * or.Price;
                        break;
                    case 3:
                        sum_3 += or.Qty * or.Price;
                        break;
                    default:
                        sum_4 += or.Qty * or.Price;
                        break;
                };
			}
			IndexViewModel model = new IndexViewModel()
			{
				order = num_order,
				product = sum_pr,
				revenue = totalPrice,
				user = num_user,
				revenue1 = sum_1,
				revenue2 = sum_2,
				revenue3 = sum_3,
				revenue4 = sum_4,
			};
            return View(model);
        }
		public async Task<IActionResult> Admin() 
		{
            //if (!CheckRoleAdmin()) return RedirectToAction("Index", "Admin");
            List<AdminDTO> admins = await _admin.GetAdmins();
			List<AccountDTO> accounts = await _acount.GetAccounts();
			AdminViewModel Model = new AdminViewModel(){
			  Admins = admins,
			  Accounts = accounts,
			};
			return View(Model);
		}
		public async Task<IActionResult> User() 
		{
            if (!CheckRole()) return RedirectToAction("Index", "Admin");
            List<UserDTO> users = await _user.GetUsers();
			List<AccountDTO> accounts = await _acount.GetAccounts();
			UserViewModel Model = new UserViewModel() {
				Users = users,
                Accounts = accounts,
            };
			return View(Model);
		}
		public async Task<IActionResult> Cate()
		{
            if (!CheckRole()) return RedirectToAction("Index", "Admin");
            List<CatalogDTO> catalogs = await _catalog.GetAllCatalog();
			CatalogViewModel Model = new CatalogViewModel() 
			{
				Catalogs = catalogs,
			};
			return View(Model);
        }
		public async Task<IActionResult> Product()
		{
            if (!CheckRole()) return RedirectToAction("Index", "Admin");
            List<CatalogDTO> catalogs = await _catalog.GetAllCatalog();
			List<ProductDTO> products = await _product.GetAllProducts();
			List<SupplyListDTO> supplyLists = await _supplyList.GetAllSupplyList();
			if(products != null && products.Any())
			{
                products.Reverse();
            }
            ProductViewModel Model = new ProductViewModel() { 
				Catalogs = catalogs,
				Products = products,
				SupplyList = supplyLists,
			};
			return View(Model);
        }
		public async Task<IActionResult> Producer()
		{
			if (!CheckRole()) return RedirectToAction("Index", "Admin");
            List<ProducerDTO> producers = await _producer.GetAllProducer();
			ProducerViewModel model = new ProducerViewModel() { Producers = producers };
			return View(model);
		}
		public async Task<IActionResult> SupplyInvoice()
		{
            if (!CheckRole()) return RedirectToAction("Index", "Admin");
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
            if (!CheckRole()) return RedirectToAction("Index", "Admin");
            List<Boardnew> boardnews = await _boardnew.GetBoardnews();
			NewsViewModel Model = new NewsViewModel() {
				Boardnews = boardnews,
			};
			return View(Model) ;
        }
		
		// Admin 
		public async Task<IActionResult> AddAdmin()
		{
            if (!CheckRoleAdmin()) return RedirectToAction("Index", "Admin");
			return View();
		}
		public async Task<IActionResult> AddAdminResult(int role, string? admin_username, string? admin_password, string? admin_name,
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
				Role = role
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
            if (!CheckRoleAdmin()) return RedirectToAction("Index", "Admin");
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
            //if (!CheckRoleAdmin()) return RedirectToAction("Index", "Admin");
			string UserName = HttpContext.Session.GetString("Username");
            string role = HttpContext.Session.GetString("Role");
            AdminDTO admin =  await _admin.GetAdmin(id);
			if(admin==null) return RedirectToAction("Admin", "Admin");
            List<AccountDTO> accounts = await _acount.GetAccounts();
			AccountDTO account = accounts.Where(a => a.Id == admin.IdAccount).FirstOrDefault();
            if (account == null) return RedirectToAction("Admin", "Admin");
			if(role=="staff"&& UserName!=account.Username) return RedirectToAction("Admin", "Admin");
            EditAdminViewModel Model = new EditAdminViewModel() 
			{ 
			Admin = admin,
			Account = account
			};
			return View(Model);
		}
		public async Task<IActionResult> EditAdminResult(int role,int id, string? password, string? name, string? phone, string? email)
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
            if (!CheckRoleAdmin()) return RedirectToAction("Index", "Admin");
			bool check = await _acount.UnLock(id);
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
		public async Task<IActionResult> LockUser(int id)
		{
            if (!CheckRole()) return RedirectToAction("Index", "Admin");
            bool check = await _acount.Lock(id);
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
        public async Task<IActionResult> UnLockUser(int id)
        {
            if (!CheckRole()) return RedirectToAction("Index", "Admin");
            bool check = await _acount.UnLock(id);
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
                        //await _recommendation.BuildDataset();
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
			List<UsedDTO> useds = await _used.GetAllUseds();
			List<UsedDTO> usedOfProduct = await _used.GetUsedsByProduct(id);
            EditProductViewModel model = new EditProductViewModel()
			{
				Catalogs = catalogs,
				Product = product,
				UsedsOfProduct = usedOfProduct,
				Useds = useds,
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
                if (input.imageFile == null || input.imageFile.Length == 0)
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
                    HttpContext.Session.SetString("errorMsg", "Không thể xóa nhà cung cáp");
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
                    return RedirectToAction("EditProducer", "Admin", new { id = request.Id });
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
                    return RedirectToAction("EditProducer", "Admin", new { id = request.Id });
                }
			}
			catch
			{
                HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
                return RedirectToAction("EditProducer", "Admin", new { id = request.Id });
            }
        }
		// Supply List
		public async Task<IActionResult> AddSupplyList(int id)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
			try
			{
				ProductDTO pr = await _product.GetProductById(id);
				CreateSupplyListRequest request = new CreateSupplyListRequest()
				{
					ProductId = id,
                    Quantity = 100,
                    ImportPrice = (pr.Price*100)/2,
					
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
                    return RedirectToAction("SupplyInvoice", "Admin");
                }
                else
                {
                    HttpContext.Session.SetString("errorMsg", "Hủy Nhập không thành công");
                    return RedirectToAction("SupplyInvoice", "Admin");
                }
            }
            catch
            {
                HttpContext.Session.SetString("errorMsg", "Hủy Nhập không thành công");
                return RedirectToAction("SupplyInvoice", "Admin");
            }
        }
		public async Task<IActionResult> EditSupplyList(int id, int quantity, int price)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
			try
			{
				ProductDTO pr = await _product.GetProductById(id);
				CreateSupplyListRequest request = new CreateSupplyListRequest()
				{
					ProductId = id,
					ImportPrice = price,
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
                string Id = HttpContext.Session.GetString("id_user");
				if(Id.IsNullOrEmpty()) return RedirectToAction("Error", "Home");
                int IdUser = int.Parse(Id);
                AdminDTO admin = await _admin.GetAdmin(IdUser);
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
            List<AdminDTO> admins = await _admin.GetAdmins();
            List<CatalogDTO> catalogs = await _catalog.GetAllCatalog();
            List<ProducerDTO> producers = await _producer.GetAllProducer();
            List<ViewSupply> supplyInvoices = await _supplyInvoice.ViewSupplies();
            supplyInvoices = supplyInvoices.OrderByDescending(s => s.SupplyTime).ToList();
            List<ProductDTO> products = await _product.GetAllProducts();
			ShowSupplyViewModel model = new ShowSupplyViewModel()
			{
				adminList = admins,
				invoiceList = supplyInvoices,
				producerList = producers,
				catalogList = catalogs,
				productList = products
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
		// Order
		public async Task<IActionResult> Order()
		{
            if (!CheckRole()) return RedirectToAction("Index", "Admin");
            List<OrderDTO> orders = await _order.GetOrders();
            if (orders == null) orders = new List<OrderDTO>();
            Dictionary<OrderDTO, int> order = new Dictionary<OrderDTO, int>();

            foreach (OrderDTO o in orders)
            {
                int sumprice = 0;
                List<OrderedDTO> ordereds = await _ordered.GetOrderedsByOrderId(o.Id);
                if (ordereds == null) ordereds = new List<OrderedDTO>();
                Console.WriteLine(ordereds.Count.ToString());
                foreach (OrderedDTO ordered in ordereds)
                {
                    sumprice += ordered.Price * ordered.Qty;
                    Console.WriteLine(sumprice.ToString());
                }
                order.Add(o, sumprice);
            }
            ShowOrderViewModel model = new ShowOrderViewModel()
            {
                orders = order,
                //transactions = transactions,
            };
            return View(model);
        }
		public async Task<IActionResult> Sale()
		{
            if (!CheckRole()) return RedirectToAction("Index", "Admin");
            List<SaleDTO> sales = await _sale.GetSales();
            if (sales == null) sales = new List<SaleDTO>();
			List<AdminDTO> admins = await _admin.GetAdmins();
			ShowSaleViewModel model = new ShowSaleViewModel()
			{
				Admins = admins,
				Sales = sales
			};
			return View(model);
        }
		public async Task<IActionResult> AddSale()
		{
            if (!CheckRole()) return RedirectToAction("Index", "Admin");
			List<ProductDTO> products = await _product.GetAllProducts();
			products = products.Where(p => p.Discount == 0).ToList();
			AddSaleViewModel model = new AddSaleViewModel()
			{
				Products = products,
			};
			return View(model);
        }

		public async Task<IActionResult> AddSaleResult(AddSaleRequest request)
		{

            if (string.IsNullOrEmpty(request.discountData))
            {
                HttpContext.Session.SetString("errorMsg", "Chưa chọn sản phẩm áp dụng giảm giá.");
                return RedirectToAction("AddSale");
            }
			Dictionary<int, int>? discount = new Dictionary<int, int>();
            List<string> list_pr = new List<string>(request.discountData.Trim().Split(" "));
			foreach (string str in list_pr)
			{
                string[] parts = str.Split('-');
                int ProductId = int.Parse(parts[0]);
                int Discount = int.Parse(parts[1]);
				discount.Add(ProductId, Discount);
            }
			CreateSaleRequest saleRequest = new CreateSaleRequest()
			{
				StartDate = request.start_date,
				EndDate = request.end_date,
				AdminId = request.admin_id,
				Des = request.desc,
				Discount = discount
			};
			bool check = await _sale.CreateSale(saleRequest);
			if (check)
			{
                HttpContext.Session.Remove("errorMsg");
                return RedirectToAction("Sale");
			}
			else
			{
                HttpContext.Session.SetString("errorMsg", "Không thể tạo đợt giảm giá");
                return RedirectToAction("AddSale");
            }
        }

		public async Task<IActionResult> ShowSaleDetail(int id)
		{
            if (!CheckRole()) return RedirectToAction("Index", "Admin");
			SaleDTO sale = await _sale.GetSaleById(id);
			if(sale==null)
			{
                HttpContext.Session.Remove("errorMsg");
                return RedirectToAction("Sale");
            }
			AdminDTO admin = await _admin.GetAdmin(sale.AdminId);
            if (admin == null)
            {
                HttpContext.Session.Remove("errorMsg");
                return RedirectToAction("Sale");
            }
			List<SaleDetailDTO> saleDetails = await _sale.GetSaleDetailById(id);
			List<ProductDTO> products = await _product.GetAllProducts();
			ShowSaleDetailViewModel model = new ShowSaleDetailViewModel()
			{
				sale = sale,
				nameEmployee = admin.Name,
				saleDetails = saleDetails,
				products = products
			};
			return View(model);
        }

        public async Task<IActionResult> ShowCart(int id)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            int sumprice = 0;
            List<ProductDTO> products = await _product.GetAllProducts();
            List<OrderedDTO> ordereds = await _ordered.GetOrderedsByOrderId(id);
            if (ordereds == null) ordereds = new List<OrderedDTO>();
            Console.WriteLine(ordereds.Count.ToString());
            foreach (OrderedDTO ordered in ordereds)
            {
                sumprice += ordered.Price * ordered.Qty;
            }
            ShowCartViewModel model = new ShowCartViewModel()
            {
                Ordereds = ordereds,
                Products = products,
                sumPrice = sumprice,

            };
            return View(model);
        }
		public async Task<IActionResult> Confirm(int id)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            string id_user = HttpContext.Session.GetString("id_user"); ;
			if(id_user.IsNullOrEmpty()) return RedirectToAction("Error", "Home");
            OrderDTO order = await _order.GetOrderById(id);
			order.Status = 1;
			order.AdminId = int.Parse(id_user);
			bool check = await _order.UpDateOrder(order);
			if (check)
			{
                HttpContext.Session.Remove("errorMsg");
                return RedirectToAction("Order");
            }
			else
			{
                HttpContext.Session.SetString("errorMsg", "Nhận đơn không thành công");
				return RedirectToAction("Order");

            }
        }
		public async Task<IActionResult> DeleteOrder(int id)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            OrderDTO order = await _order.GetOrderById(id);
			if(order == null) { return RedirectToAction("Error", "Home"); }
            bool check = await _order.DeleteOrder(id);
            if (check)
            {
                HttpContext.Session.Remove("errorMsg");
                return RedirectToAction("Order");
            }
            else
            {
                HttpContext.Session.SetString("errorMsg", "Hủy đơn không thành công");
                return RedirectToAction("Order");

            }
        }
		public async Task<IActionResult> SuccessOrder(int id)
		{
			string id_user = HttpContext.Session.GetString("id_user");
			if(id_user.IsNullOrEmpty()) return RedirectToAction("Order");
            OrderDTO order = await _order.GetOrderById(id);
            order.Status = 2;
			order.AdminId = int.Parse(id_user);
            bool check = await _order.UpDateOrder(order);
            if (check)
            {
                HttpContext.Session.Remove("errorMsg");
                return RedirectToAction("Order");
            }
            else
            {
                HttpContext.Session.SetString("errorMsg", "Nhận đơn không thành công");
                return RedirectToAction("Order");

            }
        }
        // In Week
        public async Task<IActionResult> OrderInWeek()
		{
            //if (!CheckRole()) return RedirectToAction("Error", "Home");
            List<OrderDTO> orders = await _order.GetOrders();
            DateOnly sevenDaysAgo = DateOnly.FromDateTime(DateTime.Now.AddDays(-7));
			List<OrderDTO> ordersInWeek = orders.Where(o => o.Sentdate >= sevenDaysAgo).ToList();
            if (ordersInWeek == null) ordersInWeek = new List<OrderDTO>();
            Dictionary<OrderDTO, int> order = new Dictionary<OrderDTO, int>();

            foreach (OrderDTO o in ordersInWeek)
            {
                int sumprice = 0;
                List<OrderedDTO> ordereds = await _ordered.GetOrderedsByOrderId(o.Id);
                if (ordereds == null) ordereds = new List<OrderedDTO>();
                Console.WriteLine(ordereds.Count.ToString());
                foreach (OrderedDTO ordered in ordereds)
                {
                    sumprice += ordered.Price * ordered.Qty;
                    Console.WriteLine(sumprice.ToString());
                }
                order.Add(o, sumprice);
            }
            ShowOrderViewModel model = new ShowOrderViewModel()
            {
                orders = order,
            };
            return View(model);
        }
		public async Task<IActionResult> UserInWeek()
		{
            //if (!CheckRole()) return RedirectToAction("Error", "Home");
            List<UserDTO> users = await _user.GetUsers();
			List<AccountDTO> accounts = await _acount.GetAccounts();
            DateOnly sevenDaysAgo = DateOnly.FromDateTime(DateTime.Now.AddDays(-7));
			List<UserDTO> usersInWeek = users.Where(u => u.Created >= sevenDaysAgo).ToList();
			if (usersInWeek.IsNullOrEmpty()) usersInWeek = new List<UserDTO>();
            UserViewModel Model = new UserViewModel()
            {
                Users = usersInWeek,
				Accounts = accounts,
            };
            return View(Model);
        }
		public async Task<IActionResult> ProductInWeek()
		{
            //if (!CheckRole()) return RedirectToAction("Error", "Home");
            List<CatalogDTO> catalogs = await _catalog.GetAllCatalog();
			HashSet<ProductDTO> products = new HashSet<ProductDTO>();
			Dictionary<int, int> qt = new Dictionary<int, int>(); // idProduct, quantity
            List<OrderDTO> orders = await _order.GetOrders();
            DateOnly sevenDaysAgo = DateOnly.FromDateTime(DateTime.Now.AddDays(-7));
			orders = orders.Where(o => o.Sentdate >= sevenDaysAgo).ToList();
            var orderIds = orders.Select(o => o.Id).ToList();
            List<OrderedDTO> ordereds = new List<OrderedDTO>();
            foreach (int i in orderIds)
            {
                ordereds.AddRange(await _ordered.GetOrderedsByOrderId(i));
            }
            foreach (OrderedDTO or in ordereds)
            {
                
                ProductDTO pr = await _product.GetProductById(or.ProductId);
				products.Add(pr);
                if (qt.ContainsKey(pr.Id))
                {
                    qt[pr.Id] += or.Qty;
                }
                else
                {
                    qt.Add(pr.Id, or.Qty);
                }

            }
			ProductInWeekViewModel model = new ProductInWeekViewModel()
			{
				Catalogs = catalogs,
				Products = products.ToList(),
				quantity = qt,
				SupplyList = null,
            };
            return View(model);
        }
		public async Task<IActionResult> ProductExpiry()
		{
			List<SupplyInvoiceDTO> spa = await _supplyInvoice.GetAllSupplyInvoice();
            List<CatalogDTO> catalogs = await _catalog.GetAllCatalog();
            List<ProductDTO> products = await _product.GetAllProducts();
            List<SupplyInvoiceDTO>  sp = spa.OrderByDescending(s => s.SupplyTime).ToList();
			Dictionary<ImportDetailDTO, DateOnly> spt = new Dictionary<ImportDetailDTO, DateOnly>();
			List<ImportDetailDTO> ip = new List<ImportDetailDTO>();
			foreach(SupplyInvoiceDTO supply in sp)
			{
				List<ImportDetailDTO> ipx = await _importDetail.GetImportDetailsBySupplyId(supply.Id);
				if (ipx.IsNullOrEmpty()) ipx = new List<ImportDetailDTO>();
				ip = ipx.Where(i=>i.Status==false).ToList();
				foreach(ImportDetailDTO import in ip)
				{
					ProductDTO pr = products.Where(p => p.Id == import.ProductId).FirstOrDefault();
					DateOnly date = supply.SupplyTime.AddDays(pr.ExpiryDate??0);
                    spt.Add(import, date);

                }

            }
			ProductExpiryViewModel model = new ProductExpiryViewModel() 
			{
				Catalogs = catalogs,
				Products = products,
				SP = sp,
				SPT = spt,
			};
			return View(model);
		}
        public async Task<IActionResult> ChangeUsed(int id)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            ProductDTO product = await _product.GetProductById(id);
            List<CatalogDTO> catalogs = await _catalog.GetAllCatalog();
            List<UsedDTO> useds = await _used.GetAllUseds();
			List<UsedDTO> usedOfProduct = new List<UsedDTO>();
            usedOfProduct = await _used.GetUsedsByProduct(id);
            EditProductViewModel model = new EditProductViewModel()
            {
                Catalogs = catalogs,
                Product = product,
                UsedsOfProduct = usedOfProduct,
                Useds = useds,
            };
            return View(model);
        }
		public async Task<IActionResult> AddUsed(int id, int prid)
		{
			try
			{
                if (!CheckRole()) return RedirectToAction("Error", "Home");
                UsedProductRequest request = new UsedProductRequest()
                {
                    ProductId = prid,
                    UsedId = id
                };
                bool check = await _used.CreateUsedProduct(request);
                ProductDTO pr = await _product.GetProductById(prid);
                if (pr == null) return RedirectToAction("Product");
                if (check)
                {
                    HttpContext.Session.Remove("errorMsg");
                    return RedirectToAction(nameof(ChangeUsed), "Admin", new { id = prid });
                }
                else
                {
                    HttpContext.Session.SetString("errorMsg", "Không Thể thêm công dụng");
                    return RedirectToAction(nameof(ChangeUsed), "Admin", new { id = prid });
                }
			}
			catch
			{
                HttpContext.Session.Remove("errorMsg");
                return RedirectToAction(nameof(ChangeUsed), "Admin", new { id = prid });
            }
			
		}
		public async Task<IActionResult> RemoveUsed(int id, int prid)
		{
			try
			{
                if (!CheckRole()) return RedirectToAction("Error", "Home");
                UsedProductRequest request = new UsedProductRequest()
                {
                    ProductId = prid,
                    UsedId = id
                };
                bool check = await _used.DeleteUsedProduct(request);
				ProductDTO pr = await _product.GetProductById(prid);
				if (pr == null) return RedirectToAction("Product");
                if (check)
                {
                    HttpContext.Session.Remove("errorMsg");
                    return RedirectToAction(nameof(ChangeUsed), "Admin", new { id = prid });
                }
                else
                {
                    HttpContext.Session.SetString("errorMsg", "Không thể xóa công dụng");
                    return RedirectToAction(nameof(ChangeUsed), "Admin", new { id = prid });
                }
			}
			catch
			{
                HttpContext.Session.Remove("errorMsg");
                return RedirectToAction(nameof(ChangeUsed), "Admin", new { id = prid });
            }
			
		}
		public async Task<IActionResult> CreateUsed([FromForm] AddUsedRequest request)
		{
			if (!CheckRole()) return RedirectToAction("Error", "Home");
			bool check = await _used.CreateUsed(request.used);
			if (check)
			{
                //await _recommendation.BuildDataset();
                HttpContext.Session.Remove("errorMsg");
				return RedirectToAction(nameof(ChangeUsed), "Admin", new { id = request.prid });
			}
			else
			{
				HttpContext.Session.SetString("errorMsg", "Không thể tạo công dụng");
				return RedirectToAction(nameof(ChangeUsed), "Admin", new { id = request.prid });
			}
		}
		public async Task<IActionResult> data()
		{
			bool check = await _recommendation.BuildDataset();
            return RedirectToAction("Index", "Admin");
        }
		public async Task<IActionResult> OrderOfUser(int id)
		{
            List<OrderDTO> orders = await _order.GetOrdersByUserId(id);
            if (orders == null) orders = new List<OrderDTO>();
            Dictionary<OrderDTO, int> order = new Dictionary<OrderDTO, int>();

            foreach (OrderDTO o in orders)
            {
                int sumprice = 0;
                List<OrderedDTO> ordereds = await _ordered.GetOrderedsByOrderId(o.Id);
                if (ordereds == null) ordereds = new List<OrderedDTO>();
                foreach (OrderedDTO ordered in ordereds)
                {
                    sumprice += ordered.Price * ordered.Qty;
                    Console.WriteLine(sumprice.ToString());
                }
                order.Add(o, sumprice);
            }
            ShowOrderViewModel model = new ShowOrderViewModel()
            {
                orders = order,
            };
            return View(model);
        }
		public async Task<IActionResult> AdminOrderReport()
		{

			return RedirectToAction("ReportOrder", new RequestReportData());
		}
		public async Task<IActionResult> ReportOrder(RequestReportData request)
		{
			
			if (request.startDate > request.endDate)
			{
                HttpContext.Session.SetString("errorMsg", "Lỗi khi chọn thời gian");
				return View(new ReportOrderViewModel());
            }
            List<CatalogDTO> catalogs = await _catalog.GetAllCatalog();
            List<ReportData> reportDatas = await _order.DataOrder(request);
            List<ProductDTO> products = await _product.GetAllProducts();
            Decimal value1 = 0;
			
            if (!reportDatas.IsNullOrEmpty())
			{
                value1 = reportDatas.Sum(x => x.Amount);
            }
			ReportOrderViewModel model = new ReportOrderViewModel()
			{
				startDate = request.startDate,
				endDate = request.endDate,
				reportData = reportDatas,
				value = value1,
				catalogList = catalogs,
				productList = products
			};
            HttpContext.Session.Remove("errorMsg");
            return View(model);
		}
	}
}
