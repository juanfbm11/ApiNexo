using ApiNexo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiNexo.Repository.Repository
{
    public interface IMunicipioQueries
    {
        Task<IEnumerable<Municipio>> Gettall();
    }
}
