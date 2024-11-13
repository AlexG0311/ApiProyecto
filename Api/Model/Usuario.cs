using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;




namespace Api.Model
{
    public class Usuario
    {
        [Key]
        public int idUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public string Telefono { get; set; }

        // Relación uno a uno con Cliente y Empleado
        // Evitar la serialización de la propiedad Cliente para evitar el ciclo
        [JsonIgnore]
        public Cliente Cliente { get; set; }
        [JsonIgnore]
        public Empleado Empleado { get; set; }
    }



}
