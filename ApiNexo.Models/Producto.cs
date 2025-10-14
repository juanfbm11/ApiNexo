using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiNexo.Models
{
    [Table("dbo.Producto")]
    public class Producto
    {
        [Key]
       public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Precio { get; set; } = string.Empty;
        public string Plataforma { get; set; } = string.Empty;
        public int CategoriaId { get; set; }
        public int UsuarioId  { get; set; }
                

    }
}
