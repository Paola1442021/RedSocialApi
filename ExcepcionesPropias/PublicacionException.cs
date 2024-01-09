using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias
{
    public class PublicacionException : Exception
    {
        public PublicacionException() : base() { }

        public PublicacionException(string msg) : base(msg) { }

        public PublicacionException(string msg, Exception interna) : base(msg, interna) { }
    }
}
