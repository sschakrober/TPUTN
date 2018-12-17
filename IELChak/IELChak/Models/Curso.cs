using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IELChak.Models
{
    public class Curso
    {
        public int CursoID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Horas { get; set; }
        public Boolean Estado { get; set; } = true;
        public int CategoriaID { get; set; }
        public Categoria Categoria { get; set; }
    }
}
