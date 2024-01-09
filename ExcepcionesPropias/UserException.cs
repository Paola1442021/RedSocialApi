using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias
{
    public class UserException: Exception
    {
        public UserException() : base() { }

        public UserException(string msg) : base(msg) { }

        public UserException(string msg, Exception interna) : base(msg, interna) { }
    }
}
