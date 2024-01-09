using DTOs;
using LogicaDeNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCU
{
    public interface IAsignarFotoDePerfil
    {
        public void AsignarFotoDePerfil(int unUsuario, string nomArchivo);

    }
}
