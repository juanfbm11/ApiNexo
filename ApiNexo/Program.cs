
using ApiNexo.Repository.Implements;
using ApiNexo.Repository.Implements.TuProyecto.Repositories.Implements;
using ApiNexo.Repository.Repository;
using Microsoft.Data.SqlClient;
using System.Data;


namespace ApiNexo
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            builder.Services.AddScoped<IDbConnection>(options =>
            {
                var connect = builder.Configuration.GetConnectionString("SqlConnection");
                var con = new SqlConnection(connect);
                return con;
            });

            builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddTransient<IUsuarioQueries, UsuarioQueries>();
            builder.Services.AddTransient<IProductoRepository, ProductoRepository>();
            builder.Services.AddTransient<IProductoQueries, ProductoQueries>();
            builder.Services.AddTransient<IMunicipioRepository,MunicipioRepository >();
            builder.Services.AddTransient<IMunicipioQueries, MunicipioQueries>();
            builder.Services.AddTransient<ICategoriaQueries, CategoriaQueries>();
            builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();




            //builder.Services.AddOpenApi();

            builder.Services.AddSwaggerGen(options =>
            {
                string xmlFile = "ApiNexo.xml";
                String xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

            });


            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
