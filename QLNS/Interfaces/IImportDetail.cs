using QLNS.DTO;

namespace QLNS.Interfaces
{
    public interface IImportDetail
    {
        Task<List<ImportDetailDTO>> GetImportDetailsBySupplyId(int id);
    }
}
