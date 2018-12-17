using IELChak.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using IELChak.Models;

namespace IELChak.ModelsClass
{
    public class CategoriaModels
    {
        private ApplicationDbContext context;
        public CategoriaModels(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<IdentityError> guardarCategoria(string nombre, string descripcion, string estado)
        {
            var errorList = new List<IdentityError>();
            var categoria = new Categoria
            {
                Nombre = nombre,
                Descripcion = descripcion,
                Estado = Convert.ToBoolean(estado)
            };

            context.Add(categoria);
            context.SaveChangesAsync();
            errorList.Add(new IdentityError
            {
                Code = "Save",
                Description = "Save"
            });

            return errorList;
        }
    }
}
