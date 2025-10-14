using ApiNexo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiNexo.Repository.Repository
{
    public interface IUsuarioRepository
    {
        Task<Usuario> Add(Usuario usuario);
        Task<bool> Update(Usuario usuario);
        Task<bool> Delete(Usuario usuario);



    }
}
