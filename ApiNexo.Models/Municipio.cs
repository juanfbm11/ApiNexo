using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiNexo.Models
{
    [Table("dbo.Municipio")]
    public class Municipio
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public String CodigoDane { get; set; } = String.Empty;
    }
}
