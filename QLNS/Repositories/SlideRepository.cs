using QLNS.Interfaces;
using QLNS.Models;
using QLNS.Singleton;

namespace QLNS.Repositories
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
