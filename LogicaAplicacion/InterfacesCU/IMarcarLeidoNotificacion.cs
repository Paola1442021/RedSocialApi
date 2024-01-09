using DTOs;
using LogicaDeNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCU
{
    public interface IMarcarLeidoNotificacion
    {
        public void MarcarLeido(int idNotificacion, int idUser);
    }
}
