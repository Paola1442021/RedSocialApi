using LogicaDeNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.InterfacesRepositorio
{
    public interface IRepositorioGrupos: IRepositorio<Grupo>
    {
        IEnumerable<Grupo> EncontrarGrupoPorNombre(string unNombre);
        void AgregarMiembroAGrupo(User miembro, Grupo grupo);
        public IEnumerable<int> ObtenerIdsDeGrupos(List<Grupo> grupos);
        public IEnumerable<int> ObtenerIdsDeMiembrosDeGrupos(int id);



    }
}
