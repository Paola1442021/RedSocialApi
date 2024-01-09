using ExcepcionesPropias;
using LogicaDeNegocio;
using LogicaDeNegocio.InterfacesRepositorio;
using LogicaDeNegocio.ValueObjects;
using Obligatorio;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace LogicaDeNegocio.Dominio
{
    public class User : IValidable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue(1)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        [Required]

        public NombreUsuario Username { get; set; }
        [Required]

        public Email Email { get; set; }
        [Required]

        public string Apellido { get; set; }
        [Required]
        public List<Publicacion>? Publicaciones { get; set; }
        public List<Grupo>? Grupos { get; set; }
        public List<MeGusta>? MeGustas { get; set; }

        public List<Mensaje>? MensajesRecibidos { get; set; }
        public List<Mensaje>? MensajesEnviados { get; set; }
        public List<Notificacion>? Notificaciones { get; set; }
        public List<User>? Seguidores { get; set; }
        public List<User>? Seguidos { get; set; }

        public string? NomArchivoFotoPerfil { get; set; }
        public DateTime FechaNacimiento { get; set; }
        [Required]
        public DateTime FechaCreacionCuenta { get; set; }
        public string Password_hash {  get; set; }

        public User()
        {
            Publicaciones = new List<Publicacion>();
            Grupos = new List<Grupo>(); 
            MensajesRecibidos = new List<Mensaje>();
            MensajesEnviados = new List<Mensaje>();
            Notificaciones = new List<Notificacion>();
            Seguidores = new List<User>();
            Seguidos = new List<User>();
            MeGustas = new List<MeGusta>();
           


        }

        public User(string nombre, NombreUsuario username, Email email,string apellido,
             DateTime fechaNacimiento, string password_hash)
        {
            this.Nombre = nombre;
            this.Username = username;
            this.Email = email;
            this.Apellido = apellido;
            this.FechaNacimiento = fechaNacimiento;
            this.Password_hash = EncryptPassword(password_hash);
            this.Publicaciones = new List<Publicacion>();
            this.Grupos = new List<Grupo>();
            this.MensajesRecibidos = new List<Mensaje>();
            this.MensajesEnviados = new List<Mensaje>();
            this.Notificaciones = new List<Notificacion>();
            this.Seguidores = new List<User>();
            this.Seguidos = new List<User>();
            this.MeGustas = new List<MeGusta>();
            this.FechaCreacionCuenta = new DateTime();


        }
        //agregue validar para poder usarlo en el repositorio
        public void Validar()
        {
            
            if (Nombre.Length<2 || Nombre.Length > 100)
            {
                throw new UserException("La Longitud tiene que ser mayor a 2 y menor a 100");
            }
            if (Apellido.Length < 2 || Apellido.Length > 100)
            {
                throw new UserException("La Longitud tiene que ser mayor a 2 y menor a 100");
            }
            ValidarEdadMinima();

        }
        public int CalcularEdad()
        {
            // Calcular la edad basándote en la Fecha de Nacimiento
            DateTime fechaActual = DateTime.Now;
            int edad = fechaActual.Year - FechaNacimiento.Year;

            // Ajustar la edad si aún no ha tenido su cumpleaños este año
            if (fechaActual.Month < FechaNacimiento.Month ||
                (fechaActual.Month == FechaNacimiento.Month && fechaActual.Day < FechaNacimiento.Day))
            {
                edad--;
            }

            return edad;
        }

        public void ValidarEdadMinima()
        {
            int edad = CalcularEdad();

            // Verificar si la edad es menor de 13
            if (edad < 13)
            {
                throw new UserException("El usuario debe tener al menos 13 años.");
            }
        }
        public string EncryptPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    stringBuilder.Append(hashBytes[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }

        
    }
}