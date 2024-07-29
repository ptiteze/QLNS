using QLNS_BackEnd.DTO;
using QLNS_BackEnd.ModelsParameter.Producer;

namespace QLNS_BackEnd.Interfaces
{
    public interface IProducer
    {
        ProducerDTO GetProducerById(int id);
        List<ProducerDTO> GetAllProducer();
        bool CreateProducer(CreateProducerRequest request);
        bool UpdateProducer(ProducerDTO request);
        bool DeleteProducer(int id);
    }
}
