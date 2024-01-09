using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias
{
    public class GrupoException : Exception
    {
        public GrupoException() : base() { }

        public GrupoException(string msg) : base(msg) { }

        public GrupoException(string msg, Exception interna) : base(msg, interna) { }
    }
}
