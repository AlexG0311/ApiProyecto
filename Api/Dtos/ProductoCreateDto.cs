namespace Api.Dtos
{
    public class ProductoCreateDto
    {
        public int idProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stok { get; set; }
        public string Img { get; set; }


    }
}
