﻿namespace QLNS_BackEnd.DTO
{
    public class InfoLogin
    {
        public int IdUser { get; set; }
        public string Username { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool? Status { get; set; }
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? role { get; set; }
    }
}
