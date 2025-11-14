using ApiNexo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiNexo.Repository.Repository
{
    public interface IMunicipioRepository
    {
        Task<Municipio?> GetByIdAsync(int id);

        Task<Municipio> Add(Municipio municipio);
        Task<bool> Update(Municipio municipio);
        Task<bool> Delete(int id);
    }
}
