using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Api.Model
{
    

        public class Producto
        {
        [Key]
        public int idProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stok { get; set; }
        public string Img { get; set; }

        // Relación uno a uno con Descuento
        public Descuento Descuento { get; set; }

        // Relación muchos a muchos con Cliente
        public ICollection<clienteproducto> ClienteProductos { get; set; }

        // Relación uno a muchos con Factura
        public ICollection<Factura> Facturas { get; set; }
    }

}
