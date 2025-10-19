using ApiNexo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiNexo.Repository.Repository
{
    public interface IPagoRepository
    {
        Task<IEnumerable<Pago>> GetAll();
        Task<Pago> Add(Pago pago);
    }
}
