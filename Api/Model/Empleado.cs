using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Model
{
    public class Empleado
    {

        [Key]
        public int idEmpleado { get; set; }

        // Clave foránea hacia Usuario
        public int Usuario_idUsuario { get; set; }
        public Usuario Usuario { get; set; }

        // Relación uno a muchos con Horario
        public ICollection<Horario> Horarios { get; set; }

        // Relación uno a muchos con Especialidades
        public ICollection<Especialidades> Especialidades { get; set; }


    }
}
