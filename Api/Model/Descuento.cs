using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api.Model
{
    public class Descuento
    {

        [Key]
        public int producto_idProducto { get; set; }
        public decimal? Porcentaje { get; set; }
        public decimal? Total { get; set; }

        // Clave foránea hacia Producto
        public Producto Producto { get; set; }
    }
}
