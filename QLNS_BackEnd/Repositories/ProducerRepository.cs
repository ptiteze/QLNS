using Azure.Core;
using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Models;
using QLNS_BackEnd.ModelsParameter.Producer;
using QLNS_BackEnd.Singleton;

namespace QLNS_BackEnd.Repositories
{
	public class ProducerRepository : IProducer
	{
		public bool CreateProducer(CreateProducerRequest request)
		{
			try
			{
				Producer producer = new Producer()
				{
					Address = request.Address,
					Name = request.Name,
					Email = request.Email,
					Numphone = request.Numphone,
				};
				SingletonDataBridge.GetInstance().Producers.Add(producer);
				SingletonDataBridge.GetInstance().SaveChanges();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool DeleteProducer(int id)
		{
			try
			{
				SupplyInvoice si = SingletonDataBridge.GetInstance().SupplyInvoices.Where(s => s.ProducerId == id).FirstOrDefault();
				if (si != null) return false;
				Producer producer = SingletonDataBridge.GetInstance().Producers.Find(id);
				SingletonDataBridge.GetInstance().Producers.Remove(producer);
				SingletonDataBridge.GetInstance().SaveChanges();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public List<ProducerDTO> GetAllProducer()
		{
			return SingletonAutoMapper.GetInstance().Map<List<ProducerDTO>>(
				SingletonDataBridge.GetInstance().Producers);
		}

		public ProducerDTO GetProducerById(int id)
		{
			return SingletonAutoMapper.GetInstance().Map<ProducerDTO>(
				SingletonDataBridge.GetInstance().Producers.Find(id));
		}

		public bool UpdateProducer(ProducerDTO request)
		{
			try
			{
				Producer producer = SingletonDataBridge.GetInstance().Producers.Find(request.Id);
				producer.Name = request.Name;
				producer.Address = request.Address;
				producer.Numphone = request.Numphone;
				producer.Email = request.Email;
				SingletonDataBridge.GetInstance().Producers.Update(producer);
				SingletonDataBridge.GetInstance().SaveChanges();
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
