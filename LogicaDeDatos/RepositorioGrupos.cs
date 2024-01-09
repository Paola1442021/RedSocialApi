using ExcepcionesPropias;
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
    public class RepositorioGrupos : IRepositorioGrupos
    {

        public EmpresaContext Contexto { get; set; }

        public RepositorioGrupos(EmpresaContext ctx)
        {
            Contexto = ctx;
        }

        public void Add(Grupo obj)
        {
            try
            {
                if (obj != null)
                {
                    

                        obj.Validar();
                        Contexto.grupos.Add(obj);
                        Contexto.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("No se pudo agregar el grupo a la base de datos.");
                    }
                }
            
            catch (Exception ex)
            {
                throw new Exception("No se pudo crear el grupo", ex);
            }
        }
        public IEnumerable<Grupo> FindAll()
        {
            return Contexto.grupos
                .Include(g => g.Miembros) 
                .ToList();

        }
        public IEnumerable<Grupo> EncontrarGrupoPorNombre(string unNombre)
        {
            return Contexto.grupos
               .Where(gru => gru.Nombre == unNombre)
               .Include(gru => gru.Miembros) // Carga la lista de usuarios relacionados
               .ToList();


        }

        public Grupo FindById(int id)
        {
            return Contexto.grupos
                .Include(gru => gru.Miembros)
                .FirstOrDefault(gru => gru.Id == id);
        }


        public void Remove(Grupo obj)
        {
            Contexto.grupos.Remove(obj);
            Contexto.SaveChanges();
        }

        public void Update(Grupo obj)
        {
            Contexto.grupos.Update(obj);
            Contexto.SaveChanges();
        }

        public void AgregarMiembroAGrupo(User miembro, Grupo grupo)
        {
            var Grupo = Contexto.grupos.FirstOrDefault(gru => gru.Id == grupo.Id);

            if (miembro != null)
            {

                Grupo.Miembros.Add(miembro);
                Contexto.SaveChanges(); 
            }
            else
            {
                // Manejar el caso en el que el miembro no existe en el contexto
                throw new InvalidOperationException("El miembro no existe en el contexto.");
            }
        }



        public void EliminarMiembroDeGrupo(User miembro, Grupo grupo)
        {
            // Obtener el grupo de la base de datos
            var grupoExistente = Contexto.grupos.Include(gru => gru.Miembros).FirstOrDefault(gru => gru.Id == grupo.Id);

            if (grupoExistente != null)
            {
                // Verificar si el miembro es parte del grupo
                if (grupoExistente.Miembros.Contains(miembro))
                {
                    // Eliminar al miembro del grupo
                    grupoExistente.Miembros.Remove(miembro);

                    // Guardar los cambios en la base de datos
                    Contexto.SaveChanges();
                }
                else
                {
                    // El usuario no es miembro del grupo
                    // Puedes lanzar una excepción o manejarlo de acuerdo a tus requerimientos
                    throw new Exception("El usuario no es miembro del grupo.");
                }
            }
            else
            {
                // El grupo no fue encontrado
                // Puedes lanzar una excepción o manejarlo de acuerdo a tus requerimientos
                throw new Exception("Grupo no encontrado en la base de datos.");
            }
        }
        public IEnumerable<int> ObtenerIdsDeGrupos(List<Grupo> grupos)
        {
            List<int> idsEncontrados = new List<int>();

            foreach (Grupo grupo in grupos)
            {
                int idEncontrado = Contexto.grupos
                    .Where(gru => gru.Id == grupo.Id)
                    .Select(gru => grupo.Id)
                    .FirstOrDefault();

                if (idEncontrado != 0)
                {
                    idsEncontrados.Add(idEncontrado);
                }
            }

            return idsEncontrados;
        }

        public IEnumerable<int> ObtenerIdsDeMiembrosDeGrupos(int id)
        {
            List<int> idsEncontrados = new List<int>();
            Grupo grupo = Contexto.grupos.Where(g => g.Id == id).FirstOrDefault();

            if (grupo != null)
            {
                foreach (User miembro in grupo.Miembros)
                {
                    idsEncontrados.Add(miembro.Id);
                }
            }

            return idsEncontrados;
        }


        /*public IEnumerable<Grupo> EncontrarGruposPorIds(List<int> ids)
        {
            List<Grupo> gruposEncontrados = new List<Grupo>();

            foreach (int id in ids)
            {
                Grupo grupo = Contexto.grupos
                    .Where(gru => gru.Id == id)
                    .Include(gru => gru.Miembros)
                    .FirstOrDefault();

                if (grupo != null)
                {
                    gruposEncontrados.Add(grupo);
                }
            }

            return gruposEncontrados;
        }*/

        /* public Etiqueta PorNombreCientifico(string nombreCientifico)
         {
             // Utiliza el método FirstOrDefault() para obtener el primer resultado de la consulta
             Etiqueta esp = Contexto.especies
                 .Include(e => e.Estado) // Carga el Estado relacionado
                 .Include(e => e.Amenazas)
                 .FirstOrDefault(esp => esp.NombreCien.Value == nombreCientifico);

             return esp;
         }


         public IEnumerable<Etiqueta> PorRangoDePeso(decimal rangopeso)
         {
             IEnumerable<Etiqueta> especies = Contexto.especies
                 .Include(e => e.Estado)
                 .Include(e => e.Amenazas)
                 .Where(esp => esp.RangoDePeso == rangopeso)
                 .ToList(); // Usa ToList() para materializar la consulta y obtener una lista

             return especies;
         }

         public Notificacion EncontrarEstado(int unId)
         {
             var estado = Contexto.estadosDeConservacion
                     .Where(est => est.Id == unId)
                     .FirstOrDefault(); // O SingleOrDefault() si se espera exactamente un resultado

             return estado;
         }*/
    }
}

