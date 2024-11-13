using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Model
{
    public class DetalleFactura
    {

        [Key]
        public int Factura_idFactura { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        // Clave foránea hacia Factura
        public Factura Factura { get; set; }
    }
}
