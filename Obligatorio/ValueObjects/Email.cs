using Obligatorio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace LogicaDeNegocio.ValueObjects
{
    
    public class Email:IValidable
    {
        [MaxLength(120)]
        [MinLength(12)]
        public string Value { get; private set; } //SON INMUTABLES

        public Email(string value)
        {
            Value = value;
            Validar(); //SE VALIDAN A SÍ MISMOS AL CONSTRUIRSE
        }

        private Email()
        {
        }

        public void Validar()
        {
            if (Value.Length < 12 || Value.Length > 120)
            {
                throw new Exception("El largo del email debe de ser entre 12 y 120 caracteres");
            }

            if (!EsEmailValido(Value))
            {
                throw new Exception("La dirección de correo electrónico no es válida.");
            }
        }

        private bool EsEmailValido(string email)
        {
            try
            {
                // Utilizar una expresión regular para verificar el formato del correo electrónico
                // Esta es una expresión regular simple y puede no cubrir todos los casos posibles
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return regex.IsMatch(email);
            }
            catch (RegexMatchTimeoutException)
            {
                return false; // La expresión regular ha excedido el tiempo de ejecución
            }
        }
    }
}

