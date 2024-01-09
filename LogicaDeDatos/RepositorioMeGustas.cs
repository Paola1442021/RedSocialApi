using LogicaDeNegocio.Dominio;
using LogicaDeNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeDatos
{
    public class RepositorioMeGustas : IRepositorioMeGustas
    {

        public EmpresaContext Contexto { get; set; }

        public RepositorioMeGustas(EmpresaContext ctx)
        {
            Contexto = ctx;
        }

       

        public void Add(MeGusta obj)
        {
            try
            {
                if (obj != null)
                {
                    Contexto.meGustas.Add(obj);
                    Contexto.SaveChanges();
                }
                else
                {
                    throw new Exception("No se pudo agregar el me gusta a la base de datos.");
                }
            }

            catch (Exception ex)
            {
                throw new Exception("No se pudo crear el me gusta", ex);
            }
        }

        public void Remove(MeGusta obj)
        {
            Contexto.meGustas.Remove(obj);
            Contexto.SaveChanges();
        }

        public void Update(MeGusta obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MeGusta> FindAll()
        {
            return Contexto.meGustas.ToList();
        }
        public IEnumerable<MeGusta> EncontrarMeGustasDePublicacion(Publicacion publicacion)
        {
            return Contexto.meGustas
               .Where(mg => mg.Publicacion == publicacion)
               .Include(mg => mg.Publicacion)
               .Include(mg => mg.Usuario)
               .ToList();
        }

        public MeGusta FindById(int id)
        {
            throw new NotImplementedException();

        }


    }
}
