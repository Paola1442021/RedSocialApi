using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias
{
    public class SeguidorException: Exception
    {
        public SeguidorException() : base() { }

        public SeguidorException(string msg) : base(msg) { }

        public SeguidorException(string msg, Exception interna) : base(msg, interna) { }
    }
}
