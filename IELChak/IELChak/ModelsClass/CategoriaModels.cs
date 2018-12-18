﻿using IELChak.Data;
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
            filtrarDatos(1, "Android");
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

        public List<object[]> filtrarDatos(int numPagina, string valor)
        {
            int count = 0, cant, numRegistros = 0, inicio = 0, reg_por_pagina = 2;
            int can_paginas, pagina;
            string dataFilter = "", paginador = "", Estado = null;
            List<object[]> data = new List<object[]>();
            IEnumerable<Categoria> query;
            var categorias = context.Categoria.OrderBy(c => c.Nombre).ToList();
            numRegistros = categorias.Count;
            inicio = (numPagina - 1) * reg_por_pagina;
            can_paginas = (numRegistros / reg_por_pagina);
            if(valor == "null")
            {
                query = categorias.Skip(inicio).Take(reg_por_pagina);
            }
            else
            {
                query = categorias.Where(c => c.Nombre.StartsWith(valor) || c.Descripcion.StartsWith(valor)).Skip(inicio).Take(reg_por_pagina);
            }
            cant = query.Count();
            foreach (var item in query)
            {
                if (item.Estado == true)
                {
                    Estado = "<a data-toggle='modal' data-target='#ModalEstado' onclick='editarEstado(" + item.CategoriaID + ")' class='btn btn-success'>Activo</a>";
                }
                else
                {
                    Estado = "<a data-toggle='modal' data-target='#ModalEstado' onclick='editarEstado(" + item.CategoriaID + ")' class='btn btn-danger'>No activo</a>";
                }
                dataFilter += "<tr>" +
                    "<td>" + item.Nombre + "</td>" +
                    "<td>" + item.Descripcion + "</td>" +
                    "<td>" + Estado + " </td>" +
                    "<td>" +
                    "<a data-toggle='modal' data-target='#myModal' class='btn btn-success'>Edit</a>|" +
                    "<a data-toggle='modal' data-target='#myModal3' class='btn btn-danger' >Delete</a>" +
                    "</td>" +
                "</tr>";
            }
            object[] dataObj = { dataFilter, paginador };
            data.Add(dataObj);

            return data;
        }

        public List<Categoria> getCategorias(int id)
        {
            return context.Categoria.Where(c => c.CategoriaID == id).ToList();
        }
    }
}

