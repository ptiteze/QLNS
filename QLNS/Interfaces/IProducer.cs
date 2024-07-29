using QLNS.DTO;
using QLNS.ModelsParameter.Producer;

namespace QLNS.Interfaces
{
    public interface IProducer
    {
        Task<ProducerDTO> GetProducerById(int id);
        Task<List<ProducerDTO>> GetAllProducer();
        Task<bool> CreateProducer(CreateProducerRequest request);
        Task<bool> UpdateProducer(ProducerDTO request);
        Task<bool> DeleteProducer(int id);
    }
}
