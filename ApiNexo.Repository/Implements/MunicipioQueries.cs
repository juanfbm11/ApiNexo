using ApiNexo.Models;
using ApiNexo.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace ApiNexo.Repository.Implements
{
    public class MunicipioQueries : IMunicipioQueries
    {
        private readonly IDbConnection _db;
        public MunicipioQueries(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public Task<IEnumerable<Municipio>> Gettall()
        {
            throw new NotImplementedException();
        }
    }
}
