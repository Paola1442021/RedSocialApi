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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LogicaAplicacion.CasosoUso
{
    public class CUAltaUsuario : IAltaUser
    {
        public IRepositorioUsers RepoUsuarios { get; set; }
        public CUAltaUsuario(IRepositorioUsers repo) 
        {
            RepoUsuarios = repo;
        }
        public void Alta(DTOUsuario usuario)
        {
            
       
                User usu = new()
                {
                    Apellido = usuario.Apellido,
                    Email = new Email(usuario.Email),
                    Nombre = usuario.Nombre,
                    Username = new NombreUsuario(usuario.Username),
                    FechaNacimiento= usuario.FechaNacimiento,
                    Password_hash = usuario.EncryptPassword(usuario.Password_hash),
                    FechaCreacionCuenta = usuario.FechaCreacionCuenta,
                    NomArchivoFotoPerfil = usuario.NomArchivoFotoPerfil
                };

            RepoUsuarios.Add(usu);
            usuario.Id = usu.Id;
   
        }
    }
}
