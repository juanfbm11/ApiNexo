using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace ApiNexo.Models
{
    [Table("dabo.Usuario")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Telefono { get; set; }
        public string Direccion { get; set; } =string.Empty;
        public string Contrasena { get; set; } =string.Empty ;
        // Relación uno a muchos con Producto
        [Write(false)] // No se debe escribir en la base de datos
        public List<Producto>? Productos { get; set; }

        // Relación muchos a uno con Municipio
        public int MunicipioId { get; set; } // Clave foránea para Municipio
        [Write(false)] // No se debe escribir en la base de datos
        public Municipio? Municipio { get; set; }




    }
}
