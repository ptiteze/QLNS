namespace QLNS.Models
{
    public class Used
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<UsedProduct> UsedProducts { get; set; } = new List<UsedProduct>();
    }
}
