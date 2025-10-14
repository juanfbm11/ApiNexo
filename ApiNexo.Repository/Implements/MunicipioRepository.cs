using ApiNexo.Models;
using ApiNexo.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace ApiNexo.Repository.Implements
{
    public class MunicipioRepository : IMunicipioRepository
    {
        private readonly IDbConnection _db;
        public MunicipioRepository(IDbConnection db)
        {
            _db=db ?? throw new ArgumentNullException(nameof(db));
        }

        public Task<Municipio> Add(Municipio municipio)
        {
            throw new NotImplementedException();
        }
    }
}
