using QLNS.Models;

namespace QLNS.Interfaces
{
    public interface ISlide
    {
        Task<List<Slide>> GetAllSlides();
    }
}
