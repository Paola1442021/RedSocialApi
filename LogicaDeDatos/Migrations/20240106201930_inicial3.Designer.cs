﻿// <auto-generated />
using System;
using LogicaDeDatos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LogicaDeDatos.Migrations
{
    [DbContext(typeof(EmpresaContext))]
    [Migration("20240106201930_inicial3")]
    partial class inicial3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GrupoUser", b =>
                {
                    b.Property<int>("GruposId")
                        .HasColumnType("int");

                    b.Property<int>("MiembrosId")
                        .HasColumnType("int");

                    b.HasKey("GruposId", "MiembrosId");

                    b.HasIndex("MiembrosId");

                    b.ToTable("GrupoUser");
                });

            modelBuilder.Entity("LogicaDeNegocio.Dominio.Grupo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("grupos");
                });

            modelBuilder.Entity("LogicaDeNegocio.Dominio.MeGusta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PublicacionId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PublicacionId");

                    b.HasIndex("UserId");

                    b.ToTable("meGustas");
                });

            modelBuilder.Entity("LogicaDeNegocio.Dominio.Mensaje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmisorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaHora")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReceptorId")
                        .HasColumnType("int");

                    b.Property<string>("UrlFoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlVideo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("leido")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("EmisorId");

                    b.HasIndex("ReceptorId");

                    b.ToTable("mensajes");
                });

            modelBuilder.Entity("LogicaDeNegocio.Dominio.Notificacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Enlace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<bool>("leido")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("notificaciones");
                });

            modelBuilder.Entity("LogicaDeNegocio.Dominio.Publicacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EsComentario")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaHora")
                        .HasColumnType("datetime2");

                    b.Property<string>("FotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PublicacionPadreId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("VideoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PublicacionPadreId");

                    b.HasIndex("UserId");

                    b.ToTable("publicaciones");
                });

            modelBuilder.Entity("LogicaDeNegocio.Dominio.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacionCuenta")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("NomArchivoFotoPerfil")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password_hash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("UserUser", b =>
                {
                    b.Property<int>("SeguidoresId")
                        .HasColumnType("int");

                    b.Property<int>("SeguidosId")
                        .HasColumnType("int");

                    b.HasKey("SeguidoresId", "SeguidosId");

                    b.HasIndex("SeguidosId");

                    b.ToTable("UserUser");
                });

            modelBuilder.Entity("GrupoUser", b =>
                {
                    b.HasOne("LogicaDeNegocio.Dominio.Grupo", null)
                        .WithMany()
                        .HasForeignKey("GruposId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogicaDeNegocio.Dominio.User", null)
                        .WithMany()
                        .HasForeignKey("MiembrosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LogicaDeNegocio.Dominio.MeGusta", b =>
                {
                    b.HasOne("LogicaDeNegocio.Dominio.Publicacion", "Publicacion")
                        .WithMany("MeGustas")
                        .HasForeignKey("PublicacionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LogicaDeNegocio.Dominio.User", "Usuario")
                        .WithMany("MeGustas")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Publicacion");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("LogicaDeNegocio.Dominio.Mensaje", b =>
                {
                    b.HasOne("LogicaDeNegocio.Dominio.User", "Emisor")
                        .WithMany("MensajesEnviados")
                        .HasForeignKey("EmisorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LogicaDeNegocio.Dominio.User", "Receptor")
                        .WithMany("MensajesRecibidos")
                        .HasForeignKey("ReceptorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Emisor");

                    b.Navigation("Receptor");
                });

            modelBuilder.Entity("LogicaDeNegocio.Dominio.Notificacion", b =>
                {
                    b.HasOne("LogicaDeNegocio.Dominio.User", "Usuario")
                        .WithMany("Notificaciones")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("LogicaDeNegocio.Dominio.Publicacion", b =>
                {
                    b.HasOne("LogicaDeNegocio.Dominio.Publicacion", "PublicacionPadre")
                        .WithMany("Comentarios")
                        .HasForeignKey("PublicacionPadreId");

                    b.HasOne("LogicaDeNegocio.Dominio.User", "User")
                        .WithMany("Publicaciones")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PublicacionPadre");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LogicaDeNegocio.Dominio.User", b =>
                {
                    b.OwnsOne("LogicaDeNegocio.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .HasColumnType("int");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(120)
                                .HasColumnType("nvarchar(120)");

                            b1.HasKey("UserId");

                            b1.HasIndex("Value");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("LogicaDeNegocio.ValueObjects.NombreUsuario", "Username", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .HasColumnType("int");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.HasKey("UserId");

                            b1.HasIndex("Value")
                                .IsUnique();

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("Username")
                        .IsRequired();
                });

            modelBuilder.Entity("UserUser", b =>
                {
                    b.HasOne("LogicaDeNegocio.Dominio.User", null)
                        .WithMany()
                        .HasForeignKey("SeguidoresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogicaDeNegocio.Dominio.User", null)
                        .WithMany()
                        .HasForeignKey("SeguidosId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LogicaDeNegocio.Dominio.Publicacion", b =>
                {
                    b.Navigation("Comentarios");

                    b.Navigation("MeGustas");
                });

            modelBuilder.Entity("LogicaDeNegocio.Dominio.User", b =>
                {
                    b.Navigation("MeGustas");

                    b.Navigation("MensajesEnviados");

                    b.Navigation("MensajesRecibidos");

                    b.Navigation("Notificaciones");

                    b.Navigation("Publicaciones");
                });
#pragma warning restore 612, 618
        }
    }
}