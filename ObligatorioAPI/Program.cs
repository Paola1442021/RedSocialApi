using LogicaAplicacion.CasosoUso;
using LogicaAplicacion.InterfacesCU;
using LogicaDeDatos;
using LogicaDeNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var claveSecreta = "ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE=";

builder.Services.AddAuthentication(aut =>
{
    aut.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    aut.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(aut =>
{
    aut.RequireHttpsMetadata = false;
    aut.SaveToken = true;
    aut.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(claveSecreta)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => opt.IncludeXmlComments("docum.xml"));

ConfigurationBuilder confBuilder = new ConfigurationBuilder();
confBuilder.AddJsonFile("appsettings.json", false, true);
var config = confBuilder.Build();

string strCon = config.GetConnectionString("ConexionPaola");
IServiceCollection serviceCollection = builder.Services.AddDbContextPool<EmpresaContext>(options => options.UseSqlServer(strCon));

// En ConfigureServices
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                     builder =>
                     {
                         builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                     });
});


// Configuración para deshabilitar la verificación del certificado SSL
builder.Services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true; // Necesario solo para ASP.NET Core 3.1 o inferior
});

// Configuración para deshabilitar la verificación del certificado SSL
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true; // Necesario solo para ASP.NET Core 3.1 o inferior
    options.ListenLocalhost(5000, listenOptions =>
    {
        listenOptions.UseHttps();
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
    });
});
// En Configure

//Repositorios
builder.Services.AddScoped<IRepositorioGrupos, RepositorioGrupos>();
builder.Services.AddScoped<IRepositorioMeGustas, RepositorioMeGustas>();
builder.Services.AddScoped<IRepositorioMensajes, RepositorioMensajes>();
builder.Services.AddScoped<IRepositorioNotificaciones, RepositorioNotificaciones>();
builder.Services.AddScoped<IRepositorioPublicaciones, RepositorioPublicaciones>();
builder.Services.AddScoped<IRepositorioUsers, RepositorioUsers>();

//Altas
builder.Services.AddScoped<IAltaGrupo, CUAltaGrupo>();
builder.Services.AddScoped<IAltaMeGusta, CUAltaMeGusta>();
builder.Services.AddScoped<IAltaMensaje, CUAltaMensaje>();
builder.Services.AddScoped<IAltaNotificacion, CUAltaNotificacion>();
builder.Services.AddScoped<IAltaPublicacion, CUAltaPublicacion>();
builder.Services.AddScoped<IAltaUser, CUAltaUsuario>();

//usuario
builder.Services.AddScoped<IBajaUser, CUBajaUser>();
builder.Services.AddScoped<ILogin, CULogin>();
builder.Services.AddScoped<IModificarContrasenia, CUModificarContrasenia>();
builder.Services.AddScoped<IModificarUsername, CUModificarUsername>();
builder.Services.AddScoped<IObtenerUser, CUObtenerUser>();
builder.Services.AddScoped<ISeguirUsuario, CUSeguirUsuario>();
builder.Services.AddScoped<IObtenerPorNombre, CUObtenerPorUsername>();
builder.Services.AddScoped<IEliminarMensaje, CUEliminarMensaje>();
builder.Services.AddScoped<IBajaPublicacion, CUBajaPublicacion>();
builder.Services.AddScoped<IMarcarLeidoNotificacion, CUMarcarLeidoNotificacion>();
builder.Services.AddScoped<IAsignarFotoDePerfil, CUAsignarFotoDePerfil>();
builder.Services.AddScoped<IDejarDeSeguir, CUDejarDeSeguir>();
builder.Services.AddScoped<IMostrarPublicaciones, CUMostrarPublicaciones>();
builder.Services.AddScoped<IMostrarGrupos, CUMostrarGrupos>();
builder.Services.AddScoped<IObtenerSeguidosDeSeguidos, CUObtenerSeguidosDeSeguidos>();
builder.Services.AddScoped<IMostrarNotificaciones, CUMostrarNotificaciones>();
builder.Services.AddScoped<IMostrarSeguidos, CUMostrarSeguidos>();
builder.Services.AddScoped<IListadoSeguidores, CUListadoSeguidores>();



//mensajes
builder.Services.AddScoped<IObtenerMensajes, CUObtenerMensajes>();


/*
//DTOEspecie
builder.Services.AddScoped<IRepositorioEspecies, RepositorioEspecies>();
builder.Services.AddScoped<IAltaMeGusta, CUAltaGrupo>();
builder.Services.AddScoped<IObtenerEspecie, CUIobtenerEspecie>();
builder.Services.AddScoped<IObtenerAmenaza, CUObtenerAmenaza>();

builder.Services.AddScoped<IAgregarEspecieAEcosistema, CUAgregarEspecieAEcosistema>();
builder.Services.AddScoped<IAgregarAmenazaAEspecie, CUAgregarAmenazaAEspecie>();
builder.Services.AddScoped<IModificarEspecie, CUModificarEspecie>();
builder.Services.AddScoped<IListadoEspecie, CUListadoEspecie>();
builder.Services.AddScoped<IPorNombreCientifico, CUPorNombreCientifico>();
builder.Services.AddScoped<IObtenerPorPeso, CUObtenerPorPeso>();
builder.Services.AddScoped<IObtenerEspeciesDeEcosistemas,CUListarEspecieDeEcosistema > ();
builder.Services.AddScoped<IObtenerEcosistemasDondeNo, CUEspeciesQueNoEcosistemas>();

//DTOEcosistema
builder.Services.AddScoped<IRepositorioEcosistemas, RepositorioEcosistemas>();
builder.Services.AddScoped< IAltaMensaje, CUALtaEcosistema > ();
builder.Services.AddScoped<IObtenerEcosistema, CUObtenerEcosistema>();
builder.Services.AddScoped< IListadoEcosistema, CUListadoEcosistema > ();
builder.Services.AddScoped< IAltaNotificacion, CUAgregarPaisAEcosistema > ();
builder.Services.AddScoped< IBajaEcosistema, CUBajaEcosistema > ();
builder.Services.AddScoped< IModificarEcosistema, CUModificarEcossistema > ();
builder.Services.AddScoped< IAgregarAmenazaAEcosistema, CUAgregarAmenazaAEcossitema > ();

//DTOUsuario
builder.Services.AddScoped<IRepositorioUsuarios, RepositorioUsuarios>();
builder.Services.AddScoped< ILogin, CULogin > ();
builder.Services.AddScoped< IAltaPublicacion, CUAltaUsuario > ();

//DTOEstado
builder.Services.AddScoped<IRepositorioEstadoDeConservacion, RepositorioEstado>();
builder.Services.AddScoped <IListadoDeEstadoDeConservacion,CUListadoDeCosnervacion> ();

*/
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
