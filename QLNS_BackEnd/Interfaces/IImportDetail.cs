using QLNS_BackEnd.DTO;

namespace QLNS_BackEnd.Interfaces
{
    public interface IImportDetail
    {
        List<ImportDetailDTO> GetImportDetailsBySupplyId(int id);
    }
}
