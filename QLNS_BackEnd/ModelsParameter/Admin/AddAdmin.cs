﻿namespace QLNS_BackEnd.ModelsParameter.Admin
{
    public class AddAdmin
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int Status { get; set; } = 1;

        public string Phone { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int Role { get; set; } = 2;
    }
}
