using Aranda.Product.Domain.Services;
using Aranda.Product.Infraestructure.Interface;
using Aranda.Product.Infraestructure.Mapper;
using Aranda.Product.Respository.DataAcces;
using Aranda.Product.Respository.GenericRespository;
using Aranda.Product.Respository.Interface;
using Aranda.Product.Respository.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Aranda.Product.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));

            services.AddLogging();
            services.AddSingleton(typeof(ILogger), typeof(Logger<Startup>));

            var mapperConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new ProductProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            //IoC
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductRespository, ProductRespository>();
            services.AddScoped<ICategoryRespository, CategoryRespository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
