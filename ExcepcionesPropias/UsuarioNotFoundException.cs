namespace ExcepcionesPropias
{
    public class UsuarioNotFoundException : Exception
    {
        public UsuarioNotFoundException() : base() { }

        public UsuarioNotFoundException(string msg) : base(msg) { }

        public UsuarioNotFoundException(string msg, Exception interna) : base(msg, interna) { }

    }
}