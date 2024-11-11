using Microsoft.EntityFrameworkCore;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Models;
using QLNS_BackEnd.Singleton;

namespace QLNS_BackEnd.Repositories
{
    public class SlideRepository : ISlide
    {
        public async Task<List<Slide>> GetAllSlides()
        {
            return SingletonAutoMapper.GetInstance().Map<List<Slide>>(
               await SingletonDataBridge.GetInstance().Slides.ToListAsync());
        }
    }
}
