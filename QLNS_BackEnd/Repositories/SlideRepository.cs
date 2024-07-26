using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Models;
using QLNS_BackEnd.Singleton;

namespace QLNS_BackEnd.Repositories
{
    public class SlideRepository : ISlide
    {
        public List<Slide> GetAllSlides()
        {
            return SingletonAutoMapper.GetInstance().Map<List<Slide>>(
                SingletonDataBridge.GetInstance().Slides.ToList());
        }
    }
}
