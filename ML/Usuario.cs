using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
       // [Required]

        public string Nombre { get; set; }
        //[Required]
        [DisplayName("Apellido Paterno:")]
        public string ApellidoPaterno { get; set; }
        [DisplayName("Apellido Materno:")]

        public string ApellidoMaterno { get; set; }

        [DisplayName("Fecha de Nacimiento:")]
        //[Required]
        public string FechaNacimiento { get; set; }
        //[Required]
        public string Sexo { get; set; }
        //[Required]
        //[StringLength(18)]
        public string Curp { get; set; }
        //[Required]
        public string UserName { get; set; }
        //[EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        //[StringLength(10)]
        public string Telefono { get; set; }
        //[StringLength(10)]
        public string Celular { get; set; }

        public int IdRol { get; set; }

        public bool Status { get; set; }
        public List<object> Usuarios { get; set; }
        public ML.Pais Pais { get; set; }

        public ML.Estado Estado { get; set; }

        public ML.Municipio Municipio { get; set; }

        public ML.Colonia Colonia { get; set; }

        public ML.Direccion Direccion { get; set; }

        public List<object> Rols { get; set; }
        public List<object> Paises { get; set; }
        public List<object> Estados { get; set; }
        public List<object> Municipios { get; set; }

        public List<object> Colonias { get; set; }
        public List<object> Direccions { get; set; }

        //Propiedad de navegación
        public ML.Rol Rol { get; set; }




    }
} 







