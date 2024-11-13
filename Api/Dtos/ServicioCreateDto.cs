namespace Api.Dtos
{
    public class ServicioCreateDto
    {
        public int idServicio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public TimeSpan Duracion { get; set; }
        public string Img { get; set; }


    }
}
