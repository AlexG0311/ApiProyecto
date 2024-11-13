using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api.Model
{
    public class Factura
    {


        [Key]
        public int idFactura { get; set; }
        public DateTime FechaFactura { get; set; }
        public decimal? Total { get; set; }

        // Clave foránea hacia Producto
        public int Producto_idProducto { get; set; }
        public Producto Producto { get; set; }
    }
}
