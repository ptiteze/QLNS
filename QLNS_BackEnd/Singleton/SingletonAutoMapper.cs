﻿using AutoMapper;
using QLNS_BackEnd.Helper;

namespace QLNS_BackEnd.Singleton
{
    public class SingletonAutoMapper
    {
        private static IMapper? uniqueInstance;
        private static readonly object _lock = new object();
        private SingletonAutoMapper() { }
        public static IMapper GetInstance()
        {
            if (uniqueInstance == null)
            {
                lock (_lock)
                {
                    if (uniqueInstance == null)
                    {
                        // Tạo một instance của MapperConfiguration với các cấu hình từ MappingProfiles
                        var config = new MapperConfiguration(cfg =>
                        {
                            cfg.AddMaps(typeof(MappingProfiles).Assembly);
                        });
                        // Tạo IMapper từ MapperConfiguration
                        uniqueInstance = config.CreateMapper();
                    }
                }
            }
            return uniqueInstance;
        }
    }
}
