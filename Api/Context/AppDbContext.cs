using Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Api.Context
{
    public class AppDbContext : DbContext
    {



        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Usuario> usuario { get; set; }
        public DbSet<Cliente> cliente { get; set; }
        public DbSet<Empleado> empleado { get; set; }
        public DbSet<Producto> producto { get; set; }
        public DbSet<Descuento> descuento { get; set; }
        public DbSet<ReseñaProducto> reseñasproducto { get; set; }
        public DbSet<ReseñaServicio> reseñaservicio { get; set; }
        public DbSet<Servicio> servicio { get; set; }
        public DbSet<Horario> horario { get; set; }
        public DbSet<Asignacion> asignacion { get; set; }
        public DbSet<Factura> factura { get; set; }
        public DbSet<DetalleFactura> detallefactura { get; set; }
        public DbSet<Especialidades> especialidades { get; set; }
        public DbSet<Reserva> reserva { get; set; }
        public DbSet<Estado> estado { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación uno a uno entre Cliente y Usuario
            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Usuario)
                .WithOne(u => u.Cliente)
                .HasForeignKey<Cliente>(c => c.Usuario_idUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación uno a uno entre Empleado y Usuario
            modelBuilder.Entity<Empleado>()
                .HasOne(e => e.Usuario)
                .WithOne(u => u.Empleado)
                .HasForeignKey<Empleado>(e => e.Usuario_idUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación uno a uno entre Descuento y Producto
            modelBuilder.Entity<Descuento>()
                .HasOne(d => d.Producto)
                .WithOne(p => p.Descuento)
                .HasForeignKey<Descuento>(d => d.producto_idProducto)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación uno a muchos entre Cliente y ReseñaProducto
            modelBuilder.Entity<ReseñaProducto>()
                .HasOne(rp => rp.Cliente)
                .WithMany(c => c.ReseñasProducto)
                .HasForeignKey(rp => rp.cliente_idCliente);

            // Relación uno a muchos entre Producto y ReseñaProducto
            modelBuilder.Entity<ReseñaProducto>()
                .HasOne(rp => rp.Producto)
                .WithMany()
                .HasForeignKey(rp => rp.producto_idProducto);

            // Relación uno a muchos entre Cliente y ReseñaServicio
            modelBuilder.Entity<ReseñaServicio>()
                .HasOne(rs => rs.Cliente)
                .WithMany(c => c.ReseñasServicio)
                .HasForeignKey(rs => rs.cliente_idCliente);

            // Relación uno a muchos entre Servicio y ReseñaServicio
            modelBuilder.Entity<ReseñaServicio>()
                .HasOne(rs => rs.Servicio)
                .WithMany(s => s.ReseñasServicio)
                .HasForeignKey(rs => rs.servicio_idServicio);

            // Relación uno a muchos entre Empleado y Horario
            modelBuilder.Entity<Horario>()
                .HasOne(h => h.Empleado)
                .WithMany(e => e.Horarios)
                .HasForeignKey(h => h.Empleado_idEmpleado);

            // Relación uno a muchos entre Horario y Asignacion
            modelBuilder.Entity<Asignacion>()
                .HasOne(a => a.Horario)
                .WithMany(h => h.Asignaciones)
                .HasForeignKey(a => a.Horario_idHorario);

            // Relación uno a muchos entre Producto y Factura
            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Producto)
                .WithMany(p => p.Facturas)
                .HasForeignKey(f => f.Producto_idProducto);

            // Relación uno a muchos entre Factura y DetalleFactura
            modelBuilder.Entity<DetalleFactura>()
                .HasOne(df => df.Factura)
                .WithMany()
                .HasForeignKey(df => df.Factura_idFactura);

            // Relación uno a muchos entre Empleado y Especialidad
            modelBuilder.Entity<Especialidades>()
                .HasOne(e => e.Empleado)
                .WithMany(emp => emp.Especialidades)
                .HasForeignKey(e => e.Empleado_idEmpleado);

            // Relación muchos a muchos entre Cliente y Producto (mediante ClienteProducto)
            modelBuilder.Entity<clienteproducto>()
                .HasKey(cp => new { cp.cliente_idCliente, cp.producto_idProducto });

            modelBuilder.Entity<clienteproducto>()
                .HasOne(cp => cp.Cliente)
                .WithMany(c => c.ClienteProductos)
                .HasForeignKey(cp => cp.cliente_idCliente);

            modelBuilder.Entity<clienteproducto>()
                .HasOne(cp => cp.Producto)
                .WithMany(p => p.ClienteProductos)
                .HasForeignKey(cp => cp.producto_idProducto);

            // Relación uno a muchos entre Cliente y Reserva
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Cliente)
                .WithMany(c => c.Reservas)
                .HasForeignKey(r => r.Cliente_idCliente);

            // Relación uno a muchos entre Servicio y Reserva
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Servicio)
                .WithMany(s => s.Reservas)
                .HasForeignKey(r => r.Servicio_idServicio);

            // Relación uno a muchos entre Asignacion y Reserva
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Asignacion)
                .WithMany()
                .HasForeignKey(r => r.Asignacion_idAsignacion);

            // Relación uno a muchos entre Estado y Reserva
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Estado)
                .WithMany(e => e.Reservas)
                .HasForeignKey(r => r.Estado_idEstado);
        }
        public DbSet<Api.Model.clienteproducto> clienteproducto { get; set; } = default!;





    }
}
