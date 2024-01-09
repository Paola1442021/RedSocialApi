using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio;
using ExcepcionesPropias;
using Microsoft.EntityFrameworkCore;

namespace LogicaDeNegocio.ValueObjects
{

    
    public class NombreUsuario :IValidable
    {
        [MaxLength(100)]
        [MinLength(2)]
        public string Value { get; private set; } //SON INMUTABLES

        public NombreUsuario(string value)
        {
            Value = value;
            Validar(); //SE VALIDAN A SÍ MISMOS AL CONSTRUIRSE
        }

        private NombreUsuario()
        {
        }

        public void Validar()
        {
            if (Value.Length < 2 || Value.Length > 100)
            {
                throw new Exception("La longitud del nombre de usuario no cumple con los requisitos.");
            }
        }
    }
}
