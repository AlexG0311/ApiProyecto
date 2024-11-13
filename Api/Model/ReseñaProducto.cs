﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api.Model
{
    public class ReseñaProducto
    {


        [Key]
        public int idReseñaProducto { get; set; }
        public int Estrellas { get; set; }
        public DateTime Fecha { get; set; }

        // Claves foráneas
        public int cliente_idCliente { get; set; }
        public Cliente Cliente { get; set; }

        public int producto_idProducto { get; set; }
        public Producto Producto { get; set; }
    }
}
