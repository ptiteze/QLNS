namespace QLNS.ModelsParameter.Admin
{
    public class RequestForgetPass
    {
        public int Id { get; set; } = 0;
        public int Role { get; set; } = 0; // 1: admin, 2: user
    }
}
