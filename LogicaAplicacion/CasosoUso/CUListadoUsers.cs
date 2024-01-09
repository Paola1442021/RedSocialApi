using DTOs;
using LogicaAplicacion.InterfacesCU;
using LogicaDeNegocio.Dominio;
using LogicaDeNegocio.InterfacesRepositorio;
using LogicaDeNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosoUso
{
    public class CUListadoUsers : IListadoUsers
    {

        public IRepositorioUsers Repo { get; set; }
       // public IRepositorioMeGustas RepoEstado { get; set; }
        public CUListadoUsers(IRepositorioUsers repo)
        {
            Repo = repo;
        }

       /** public IEnumerable<DTOUsuario> Listado()
        {
            var usuarios = Repo.FindAll();
            var listaDTOUsers = new List<DTOUsuario>();

            foreach (var us in usuarios)
            {
                string nombrEstados = RepoEstado.EncontrarNombreEstado(e.Estado.Id);


                var nombresDePaises = RepoEcosistema.EncontrarPaises(e.Id).ToList();
                var nombresDeAmenazas = RepoEcosistema.EncontrarAmenazas(e.Id).ToList();
                var nombresDeEspecies = RepoEcosistema.EncontrarEspecies(e.Id).ToList();
                var dtoEcosistema = new DTOPublicacion
                {
                    Id = e.Id,
                    Nombre = e.Nombre.Value,
                    Area = e.Area,
                    Descripcion = e.Descripcion.Value,
                    idEstado = e.Estado.Id,
                    nombreEstado = nombrEstados,
                    Latitud = e.Latitud,
                    Longitud = e.Longitud,
                    paises = nombresDePaises,
                    Amenazas = nombresDeAmenazas,
                    Especies = nombresDeEspecies,
                    NomArchivoEcosistema = e.NomArchivoEcosistema,
                };

                listaDTOEcosistemas.Add(dtoEcosistema);
            }

            return listaDTOEcosistemas;
        }
        //public List<DTOAmenaza> ConvertirListaAmenazaAListaDTOAmenaza(List<Amenaza> listaAmenazas)
        //{
        //    List<DTOAmenaza> listaDTOAmenazas = new List<DTOAmenaza>();

        //    foreach (var amenaza in listaAmenazas)
        //    {
        //        DTOAmenaza dtoAmenaza = new DTOAmenaza()
        //        {
        //            Descripcion = amenaza.Descripcion.Value, // Asumiendo que el campo Descripcion es una cadena en DTOAmenaza
        //            Peligrosidad = amenaza.Peligrosidad,
        //            // Otros campos que puedan ser necesarios asignar en DTOAmenaza
        //        };

        //        listaDTOAmenazas.Add(dtoAmenaza);
        //    }

        //    return listaDTOAmenazas;
        //}
        //public List<DTOPais> ConvertirListaPaisesADTOPaises(List<Pais> listaPaises)
        //{
        //    List<DTOPais> listaDTOPaises = new List<DTOPais>();

        //    foreach (var pais in listaPaises)
        //    {
        //        DTOPais dtoPais = new DTOPais()
        //        {
        //            Nombre = pais.Nombre.Value, // Asumiendo que el campo Nombre es una cadena en DTOPais
        //            CodigoIso = pais.codigoISO,
        //            // Otros campos que puedan ser necesarios asignar en DTOPais
        //        };

        //        listaDTOPaises.Add(dtoPais);
        //    }

        //    return listaDTOPaises;
        //}

        //public List<DTOEspecie> ConvertirListaEspeciesADTOEspecies(List<Especie> listaEspecies)
        //{
        //    List<DTOEspecie> listaDTOEspecies = new List<DTOEspecie>();

        //    foreach (var especie in listaEspecies)
        //    {
        //        DTOEspecie dtoEspecie = new DTOEspecie()
        //        {
        //            NombreCien = especie.NombreCien.Value, // Asumiendo que el campo NombreCien es una cadena en DTOEspecie
        //            NombreCalle = especie.NombreCalle.Value, // Asumiendo que el campo NombreCalle es una cadena en DTOEspecie
        //            Descripcion = especie.Descripcion.Value, // Asumiendo que el campo Descripcion es una cadena en DTOEspecie
        //            idEstado = especie.Estado.Id, // Asumiendo que Estado tiene un campo Id en DTOEspecie
        //            RangoDeLongitud = especie.RangoDeLongitud,
        //            RangoDePeso = especie.RangoDePeso,
        //            NomArchivoEspecie = especie.NomArchivoEspecie
        //            // Otros campos que puedan ser necesarios asignar en DTOEspecie
        //        };

        //        listaDTOEspecies.Add(dtoEspecie);
        //    }

        //    return listaDTOEspecies;
        //}*/

    }
}

