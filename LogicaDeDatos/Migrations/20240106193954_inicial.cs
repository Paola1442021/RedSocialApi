using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaDeDatos.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "grupos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grupos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username_Value = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email_Value = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomArchivoFotoPerfil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCreacionCuenta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Password_hash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrupoUser",
                columns: table => new
                {
                    GruposId = table.Column<int>(type: "int", nullable: false),
                    MiembrosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoUser", x => new { x.GruposId, x.MiembrosId });
                    table.ForeignKey(
                        name: "FK_GrupoUser_grupos_GruposId",
                        column: x => x.GruposId,
                        principalTable: "grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrupoUser_users_MiembrosId",
                        column: x => x.MiembrosId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mensajes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmisorId = table.Column<int>(type: "int", nullable: false),
                    ReceptorId = table.Column<int>(type: "int", nullable: false),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlFoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlVideo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    leido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mensajes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mensajes_users_EmisorId",
                        column: x => x.EmisorId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_mensajes_users_ReceptorId",
                        column: x => x.ReceptorId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "notificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Enlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    leido = table.Column<bool>(type: "bit", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notificaciones_users_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "publicaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EsComentario = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicacionPadreId = table.Column<int>(type: "int", nullable: true),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publicaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_publicaciones_publicaciones_PublicacionPadreId",
                        column: x => x.PublicacionPadreId,
                        principalTable: "publicaciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_publicaciones_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserUser",
                columns: table => new
                {
                    SeguidoresId = table.Column<int>(type: "int", nullable: false),
                    SeguidosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUser", x => new { x.SeguidoresId, x.SeguidosId });
                    table.ForeignKey(
                        name: "FK_UserUser_users_SeguidoresId",
                        column: x => x.SeguidoresId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUser_users_SeguidosId",
                        column: x => x.SeguidosId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "meGustas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PublicacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meGustas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_meGustas_publicaciones_PublicacionId",
                        column: x => x.PublicacionId,
                        principalTable: "publicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_meGustas_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GrupoUser_MiembrosId",
                table: "GrupoUser",
                column: "MiembrosId");

            migrationBuilder.CreateIndex(
                name: "IX_meGustas_PublicacionId",
                table: "meGustas",
                column: "PublicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_meGustas_UserId",
                table: "meGustas",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_mensajes_EmisorId",
                table: "mensajes",
                column: "EmisorId");

            migrationBuilder.CreateIndex(
                name: "IX_mensajes_ReceptorId",
                table: "mensajes",
                column: "ReceptorId");

            migrationBuilder.CreateIndex(
                name: "IX_notificaciones_UsuarioId",
                table: "notificaciones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_publicaciones_PublicacionPadreId",
                table: "publicaciones",
                column: "PublicacionPadreId");

            migrationBuilder.CreateIndex(
                name: "IX_publicaciones_UserId",
                table: "publicaciones",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_Email_Value",
                table: "users",
                column: "Email_Value");

            migrationBuilder.CreateIndex(
                name: "IX_users_Username_Value",
                table: "users",
                column: "Username_Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserUser_SeguidosId",
                table: "UserUser",
                column: "SeguidosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GrupoUser");

            migrationBuilder.DropTable(
                name: "meGustas");

            migrationBuilder.DropTable(
                name: "mensajes");

            migrationBuilder.DropTable(
                name: "notificaciones");

            migrationBuilder.DropTable(
                name: "UserUser");

            migrationBuilder.DropTable(
                name: "grupos");

            migrationBuilder.DropTable(
                name: "publicaciones");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
