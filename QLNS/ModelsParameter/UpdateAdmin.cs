namespace QLNS.ModelsParameter
{
    public class UpdateAdmin
    {
        public int Id { get; set; } = 0;

        public string Password { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
