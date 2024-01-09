using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias
{
    public class MeGustaException : Exception
    {
        public MeGustaException() : base() { }

        public MeGustaException(string msg) : base(msg) { }

        public MeGustaException(string msg, Exception interna) : base(msg, interna) { }
    }
}
