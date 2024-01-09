namespace ExcepcionesPropias
{
    public class MensajeExeption: Exception
    {
        public MensajeExeption() : base() { }

        public MensajeExeption(string msg) : base(msg) { }

        public MensajeExeption(string msg, Exception interna) : base(msg, interna) { }

    }
}