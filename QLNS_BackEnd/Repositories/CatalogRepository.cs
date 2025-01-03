﻿using Azure.Core;
using Microsoft.EntityFrameworkCore;
using QLNS_BackEnd.Data;
using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Models;
using QLNS_BackEnd.ModelsParameter.Catalog;
using QLNS_BackEnd.Singleton;

namespace QLNS_BackEnd.Repositories
{
    public class CatalogRepository : ICatalog
    {
        private readonly DataContext _dataContext;
        public CatalogRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool AddCatalog(string name)
        {
            try
            {
                bool check = SingletonDataBridge.GetInstance().Catalogs.Any(c => c.Name == name);
                if(check) { return false; }
                Catalog catalog = new Catalog();
                catalog.Name = name;
                SingletonDataBridge.GetInstance().Catalogs.Add(catalog);    
                SingletonDataBridge.GetInstance().SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
           

        }

        public bool DeleteCatalog(int id)
        {
            try
            {
                bool check = SingletonDataBridge.GetInstance().Products.Any(p => p.CatalogId == id);
                if (check) { return false; }
                Catalog catalog = SingletonDataBridge.GetInstance().Catalogs.Find(id);
                SingletonDataBridge.GetInstance().Catalogs.Remove(catalog);
                SingletonDataBridge.GetInstance().SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public async Task<List<CatalogDTO>> GetAllCatalog()
        {
            return SingletonAutoMapper.GetInstance().Map<List<CatalogDTO>>(await _dataContext.Catalogs.ToListAsync());
        }

        public CatalogDTO GetCatalogById(int id)
        {
            return SingletonAutoMapper.GetInstance()
                .Map<CatalogDTO>(_dataContext.Catalogs.Where(c => c.Id == id).FirstOrDefault());
        }

        public bool UpdateCatalog(UpdateCatalogRequest request)
        {
			try
			{
                bool check = SingletonDataBridge.GetInstance().Catalogs.Any(c => c.Name == request.name && c.Id!=request.id);
                if (check) { return false; }
                Catalog catalog = SingletonDataBridge.GetInstance().Catalogs.Find(request.id);
				catalog.Name = request.name;
				SingletonDataBridge.GetInstance().Catalogs.Update(catalog);
				SingletonDataBridge.GetInstance().SaveChanges();
				return true;
			}
			catch
			{
				return false;
			}
		}
    }
}
