using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace tutorial.Models
{
    public class PersonaCE
    {
        public int id { get; set; }
        [Required]                              // Solicita si o si que se llene este campo
        [Display(Name = "Ingrese Nombres")]      // Modifica el label del campo a llenar. Antes era "nombres"
        public string nombres { get; set; }
        [Required]
        [Display(Name = "Ingrese Apellidos")]
        public string apellidos { get; set; }
        [Required]
        [Display(Name = "Ingrese Sexo")]
        public string sexo { get; set; }
        [Required]
        [Display(Name = "Ingrese Edad")]
        public int edad { get; set; }
        [Required]
        [Display(Name = "Ciudad")]
        public int codCiudad { get; set; }

        public string nombreCiudad { get; set; }
        public string nombreCompleto { get { return nombres + " " + apellidos; } } // metodo para mostrar el nombre y apellido juntos.
        public System.DateTime fechaRegistro { get; set; }

    }
       
    [MetadataType(typeof(PersonaCE))]

    public partial class Persona
    {
        public string nombreCompleto { get { return nombres + " " + apellidos; } } // metodo para mostrar el nombre y apellido juntos.
        public string nombreCiudad { get; set; } // Lo debo agregar para poder mapear correctamente
    }
    
}