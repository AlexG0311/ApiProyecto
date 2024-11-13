using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Model
{
    public class Cliente
    {
        [Key]
        public int idCliente { get; set; }

        // Clave foránea hacia Usuario
        public int Usuario_idUsuario { get; set; }
        public Usuario Usuario { get; set; }

        // Relación con ReseñaProducto y ReseñaServicio
        public ICollection<ReseñaProducto> ReseñasProducto { get; set; }
        public ICollection<ReseñaServicio> ReseñasServicio { get; set; }

        // Relación muchos a muchos con Producto
        public ICollection<clienteproducto> ClienteProductos { get; set; }

        // Relación uno a muchos con Reserva
        public ICollection<Reserva> Reservas { get; set; } // Esta es la propiedad de navegación que faltaba
    }

}
