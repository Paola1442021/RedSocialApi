using LogicaDeNegocio.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeDatos
{
    public class EmpresaContext : DbContext
    {
        public DbSet<Publicacion> publicaciones {get; set;}
        public DbSet<Grupo> grupos { get; set; }
        public DbSet<MeGusta> meGustas { get; set; }
        public DbSet<Mensaje> mensajes { get; set; }
        public DbSet<Notificacion> notificaciones { get; set; }
        
        public DbSet<User> users {get; set;}
       

        public EmpresaContext(DbContextOptions<EmpresaContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().OwnsOne(usu => usu.Username).HasIndex(nom => nom.Value).IsUnique(true);
            modelBuilder.Entity<User>().OwnsOne(usu => usu.Email).HasIndex(emai => emai.Value);
/*
            modelBuilder.Entity<Mensaje>()
        .HasOne(m => m.Receptor)
        .WithMany(u => u.MensajesRecibidos)
        .HasForeignKey(m => m.ReceptorId)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Mensaje>()
                .HasOne(m => m.Emisor)
                .WithMany(u => u.MensajesEnviados)
                .HasForeignKey(m => m.EmisorId)
                .OnDelete(DeleteBehavior.Restrict);*/

            modelBuilder.Entity<Mensaje>()
        .HasOne(m => m.Emisor)
        .WithMany(u => u.MensajesEnviados)
        .HasForeignKey(m => m.EmisorId)
        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Mensaje>()
                .HasOne(m => m.Receptor)
                .WithMany(u => u.MensajesRecibidos)
                .HasForeignKey(m => m.ReceptorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MeGusta>()
                .HasOne(mg => mg.Usuario)
                .WithMany(u => u.MeGustas)
                .HasForeignKey(mg => mg.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MeGusta>()
                .HasOne(mg => mg.Publicacion)
                .WithMany(p => p.MeGustas)
                .HasForeignKey(mg => mg.PublicacionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Eliminar la cascada para MensajesRecibidos y MensajesEnviados
            modelBuilder.Entity<User>()
                .HasMany(u => u.MensajesRecibidos)
                .WithOne(m => m.Receptor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.MensajesEnviados)
                .WithOne(m => m.Emisor)
                .OnDelete(DeleteBehavior.Restrict);
            /*
            modelBuilder.Entity<Etiqueta>().OwnsOne(esp => esp.NombreCien).HasIndex(nomC => nomC.Value).IsUnique(true);
            modelBuilder.Entity<Etiqueta>().OwnsOne(esp => esp.NombreCalle).HasIndex(nomCa => nomCa.Value).IsUnique(true);
            modelBuilder.Entity<Etiqueta>().OwnsOne(esp => esp.Descripcion).HasIndex(desc => desc.Value);
             


            modelBuilder.Entity<Seguidor>().OwnsOne(pa => pa.Nombre).HasIndex(pa => pa.Value).IsUnique(true);
            
            modelBuilder.Entity<Notificacion>().OwnsOne(esta => esta.Nombre).HasIndex(nom => nom.Value).IsUnique(true);

            modelBuilder.Entity<Publicacion>().OwnsOne(ame => ame.Descripcion).HasIndex(desc => desc.Value);

 */
            base.OnModelCreating(modelBuilder);

        }
    }
}
