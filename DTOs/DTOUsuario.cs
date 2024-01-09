using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DTOs
{
    public class DTOUsuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Apellido { get; set; }
        public IFormFile FotoPerfil { get; set; }
        public string? NomArchivoFotoPerfil { get; set; }
        public DateTime FechaCreacionCuenta { get; set; }

        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Password_hash { get; set; }
        public IEnumerable<int> idPublicaciones { get; set; }
        public IEnumerable<int> idgrupos { get; set; }
        public IEnumerable<int> idMensajesRecibidos { get; set; }
        public IEnumerable<int> idMensajesEnviados { get; set; }
        public IEnumerable<int> idNotificaciones { get; set; }
        public IEnumerable<int> idSeguidores { get; set; }
        public IEnumerable<int> idSeguidos { get; set; }


        public DTOUsuario()
        {
            idPublicaciones = new List<int>();
            idgrupos = new List<int>();
            idMensajesRecibidos = new List<int>();
            idMensajesEnviados = new List<int>();
            idNotificaciones = new List<int>();
            idSeguidores = new List<int>();
            idSeguidos = new List<int>();
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
