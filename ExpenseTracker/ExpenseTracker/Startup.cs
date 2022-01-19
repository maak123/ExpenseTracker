using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Infrastructure.Data;
using ExpenseTracker.Domain.Interfaces;
using ExpenseTracker.Infrastructure.Repositories;
using AutoMapper;
using ExpenseTracker.Business.Resources;
using ExpenseTracker.Domain.Models;
using ExpenseTracker.Business.Services;
using ExpenseTracker.Business.Core;

namespace ExpenseTracker
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

            services.AddCors();
            services.AddControllers();
            
            services.AddSwaggerGen();

            services.AddDbContext<AppDBContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ExpenseTrackerContext")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<CategoryResource, Category>();
                mc.CreateMap<Category, CategoryResource>();
                mc.CreateMap<TransactionResource, Transactions>();
                mc.CreateMap<Transactions, TransactionResource>();
                mc.CreateMap<UserResource, User>();
                mc.CreateMap<User, UserResource>();
                mc.CreateMap<TransactionAddUpdateResource, Transactions>();
                mc.CreateMap<Transactions, TransactionAddUpdateResource>();

            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionService, TransactionService>(); 
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(
             options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
            );

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
