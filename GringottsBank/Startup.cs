using AutoMapper;
using DataLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProcessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using static Utility.Enum;

namespace GringottsBank
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public delegate ITransactionProcess TransactionProcessResolver(TransactionType type);

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GringottsBank", Version = "v1" });
            });

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            InjectDependecy(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GringottsBank v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void InjectDependecy(IServiceCollection services)
        {
            services.AddTransient<ICustomerProcess, CustomerProcess>();
            services.AddTransient<ICustomerData, CustomerData>();
            
            services.AddTransient<IAccountProcess, AccountProcess>();
            services.AddTransient<IAccountData, AccountData>();

            services.AddTransient<ITransactionData, TransactionData>();
            services.AddTransient<DepositTransactionProcess>();
            services.AddTransient<WithdrawTransactionProcess>();

            services.AddTransient<TransactionProcessResolver>(provider => type =>
            {
                switch (type)
                {
                    case TransactionType.Withdraw:
                        return provider.GetService<WithdrawTransactionProcess>();
                    case TransactionType.Deposit:
                        return provider.GetService<DepositTransactionProcess>();
                    default:
                        throw new KeyNotFoundException();
                }
            });

            services.AddTransient<ITransactionDetailProcess, TransactionDetailProcess>();
        }


    }
}
