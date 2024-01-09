using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias
{
    public class NotificacionException: Exception
    {
        public NotificacionException() : base() { }

        public NotificacionException(string msg) : base(msg) { }

        public NotificacionException(string msg, Exception interna) : base(msg, interna) { }
    }
}
