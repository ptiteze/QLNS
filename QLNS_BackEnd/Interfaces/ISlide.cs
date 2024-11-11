using QLNS_BackEnd.Models;

namespace QLNS_BackEnd.Interfaces
{
    public interface ISlide
    {
        Task<List<Slide>> GetAllSlides();
    }
}
