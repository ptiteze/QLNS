using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Singleton;
using QLNS_BackEnd.DTO;
namespace QLNS_BackEnd.Repositories
{
    public class ImportDetailRepository : IImportDetail
    {
        public List<ImportDetailDTO> GetImportDetailsBySupplyId(int id)
        {
            return SingletonAutoMapper.GetInstance().Map<List<ImportDetailDTO>>(
               SingletonDataBridge.GetInstance().ImportDetails.Where(i=>i.InvoiceId==id).ToList());
        }
    }
}
