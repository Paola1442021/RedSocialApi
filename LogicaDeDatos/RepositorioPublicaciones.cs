using Microsoft.EntityFrameworkCore;
using LogicaDeNegocio.InterfacesRepositorio;
using LogicaDeNegocio.Dominio;
using ExcepcionesPropias;

namespace LogicaDeDatos
{
    public class RepositorioPublicaciones : IRepositorioPublicaciones
    {

        public EmpresaContext Contexto { get; set; }

        public RepositorioPublicaciones(EmpresaContext ctx)
        {
            Contexto = ctx;
        }


        public void Add(Publicacion obj)
        {
            try
            {
                if (obj != null)
                {
                    obj.Validar();
                    Contexto.publicaciones.Add(obj);
                    Contexto.SaveChanges();

                    // Después de guardar la publicación, obtén el usuario correspondiente
                    User usuario = Contexto.users.FirstOrDefault(u => u.Id == obj.User.Id);

                    if (usuario != null)
                    {
                        // Agrega la publicación al usuario
                        usuario.Publicaciones.Add(obj);
                        Contexto.SaveChanges(); // Guarda los cambios en el contexto
                    }
                    else
                    {
                        // Maneja el caso en el que no se encuentre el usuario
                        throw new Exception($"No se encontró el usuario con ID {obj.User.Id}.");
                    }
                }
                else
                {
                    throw new Exception("No se pudo agregar la publicación.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo crear la publicación", ex);
            }
        }


        public IEnumerable<Publicacion> FindAll()
        {
            return Contexto.publicaciones
                .Include(p => p.User)
                .Include(p => p.MeGustas)
                .Include(p => p.Comentarios)
                .Include(p => p.PublicacionPadre)
                .ToList();
        }

        public Publicacion FindById(int id)
        {
            var p = Contexto.publicaciones
                        .Include(p => p.User)
                .Include(p => p.MeGustas)
                .Include(p => p.Comentarios)
                .Include(p => p.PublicacionPadre)
                        .FirstOrDefault(p => p.Id == id);
            return p;
        }



        public void Remove(Publicacion obj)
        {
            Contexto.publicaciones.Remove(obj);
            Contexto.SaveChanges();
        }



        public void Update(Publicacion obj)
        {
            Contexto.publicaciones.Update(obj);
            Contexto.SaveChanges();
        }


        public void AgregarMeGusta(MeGusta meGusta, Publicacion publicacion)
        {
            try
            {
                if (meGusta != null && publicacion != null)
                {
                    // Verificar si el usuario ya dio MeGusta a esta publicación
                    if (publicacion.MeGustas == null)
                    {
                        publicacion.MeGustas = new List<MeGusta>();
                    }

                    if (!publicacion.MeGustas.Any(mg => mg.Usuario.Id == meGusta.Usuario.Id))
                    {
                        // Agregar MeGusta a la lista de MeGustas de la publicación
                        publicacion.MeGustas.Add(meGusta);

                        // Guardar los cambios en la base de datos
                        Contexto.SaveChanges();
                    }
                    else
                    {
                        // El usuario ya dio MeGusta a esta publicación
                        // Puedes lanzar una excepción o manejarlo de acuerdo a tus requerimientos
                        throw new Exception("El usuario ya dio MeGusta a esta publicación.");
                    }
                }
                else
                {
                    throw new Exception("MeGusta o publicación no pueden ser nulos.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo agregar MeGusta a la publicación.", ex);
            }
        }

        public void EliminarMeGusta(MeGusta meGusta, Publicacion publicacion)
        {
            try
            {
                if (meGusta != null && publicacion != null && publicacion.MeGustas != null)
                {
                    // Verificar si el usuario dio MeGusta a esta publicación
                    var meGustaExistente = publicacion.MeGustas.FirstOrDefault(mg => mg.Usuario.Id == meGusta.Usuario.Id);

                    if (meGustaExistente != null)
                    {
                        // Eliminar MeGusta de la lista de MeGustas de la publicación
                        publicacion.MeGustas.Remove(meGustaExistente);

                        // Guardar los cambios en la base de datos
                        Contexto.SaveChanges();
                    }
                    else
                    {
                        // El usuario no dio MeGusta a esta publicación
                        // Puedes lanzar una excepción o manejarlo de acuerdo a tus requerimientos
                        throw new Exception("El usuario no dio MeGusta a esta publicación.");
                    }
                }
                else
                {
                    throw new Exception("MeGusta o publicación no pueden ser nulos, o la lista de MeGustas no está inicializada.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo eliminar MeGusta de la publicación.", ex);
            }
        }
        public IEnumerable<int> ObtenerIdsDePublicaciones(List<Publicacion> publis)
        {
            List<int> idsEncontrados = new List<int>();

            foreach (Publicacion publi in publis)
            {
                int idEncontrado = Contexto.publicaciones
                    .Where(p => p.Id == publi.Id) // Aquí se corrige para usar el Id de la publicación
                    .Select(p => p.Id)
                    .FirstOrDefault();

                if (idEncontrado != 0 && idEncontrado !=null)
                {
                    idsEncontrados.Add(idEncontrado);
                }
            }

            return idsEncontrados;
        }

        public Publicacion FindById(int? publicacionPadre)
        {
            var p = Contexto.publicaciones
                         .Include(p => p.User)
                 .Include(p => p.MeGustas)
                 .Include(p => p.Comentarios)
                 .Include(p => p.PublicacionPadre)
                         .FirstOrDefault(p => p.Id == publicacionPadre);
            return p;
        }
        public void RemovePublicacion(int publicacionId, int userId)
        {
            // Obtener la publicación
            var publicacion = Contexto.publicaciones
                 .Include(p => p.User)
                .Include(p => p.MeGustas)
                .Include(p => p.Comentarios)
                .Include(p => p.PublicacionPadre)
                .FirstOrDefault(p => p.Id == publicacionId);

            if (publicacion != null)
            {
                // Verificar si el usuario es el propietario de la publicación
                if (publicacion.User.Id == userId)
                {
                    // Eliminar la publicación de las listas asociadas
                    publicacion.User?.Publicaciones?.Remove(publicacion);

                    // Eliminar la publicación de las listas de MeGustas y Comentarios
                    publicacion.MeGustas?.ForEach(mg => Contexto.meGustas.Remove(mg));
                    publicacion.Comentarios?.ForEach(com => Contexto.publicaciones.Remove(com));


                    // Finalmente, eliminar la publicación de la base de datos
                    Contexto.publicaciones.Remove(publicacion);
                    Contexto.SaveChanges();
                }
                else
                {
                    // El usuario no es el propietario de la publicación
                    // Puedes lanzar una excepción o manejarlo de acuerdo a tus requerimientos
                    throw new Exception("No tienes permisos para eliminar esta publicación.");
                }
            }
            else
            {
                // La publicación no fue encontrada
                // Puedes lanzar una excepción o manejarlo de acuerdo a tus requerimientos
                throw new Exception("Publicación no encontrada.");
            }
        }



    }
}






