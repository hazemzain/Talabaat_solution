using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Talabaat.APIs.Errors;
using Talabaat.APIs.Helpers;
using Talabaat.APIs.Middleware;
using Talabaat.Core.Entity;
using Talabaat.Core.Reposatory.Interfaces;
using Talabaat.Repository.Data;
using Talabaat.Repository.Repository;

namespace Talabaat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            //builder.Services.AddScoped<IGenericReository<Product>, IGenericReository<Product>>();
            //builder.Services.AddScoped<IGenericReository<ProductBrand>, IGenericReository<ProductBrand>>();
            //builder.Services.AddScoped<IGenericReository<ProductCatogry>, IGenericReository<ProductCatogry>>();
            builder.Services.AddScoped (typeof(IGenericReository<>),typeof(GenaricReposatry<>) );
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));
            builder.Services.Configure<ApiBehaviorOptions>(
                options =>
                {
                    options.InvalidModelStateResponseFactory = (actioncontext) =>
                    {
                        var validationErrorResponse=new ApiValidationErrorResponse()
                        {
                            Errors = actioncontext.ModelState.Values
                                .SelectMany(x => x.Errors.Select(y => y.ErrorMessage))
                                .ToList()

                        };
                        return new BadRequestObjectResult(validationErrorResponse);
                    }; 
                    
                }
            );
           //to make DataBase Update Every RunApp And If any one take Code when Run It the Data Base is created for Him
           // i Want To get object From StoreContext But the General Way Not Success so That I use DepancyEnjection
           //StoreDBContext _Context = new StoreDBContext();
           //_context.Database.MigrateAc();


           //Ask CLR to Create Object From StoreDBContext
           var app = builder.Build();
            var scope = app.Services.CreateScope();
           var service= scope.ServiceProvider;
            var _context=service.GetRequiredService<StoreDBContext>();
            //ASK CLR To Create Object From Logger
           var _logger=service.GetRequiredService<ILoggerFactory>();
            var Logger = _logger.CreateLogger<Program>();
            try
            {
                Logger.LogInformation("The Migration Started");
               await  _context.Database.MigrateAsync();
                Logger.LogInformation("The Migration Ended");
                await StoreDbContextSeed.SeedAsyn(_context);
                //if i have some Static Data In json File in My Computer And Put It in Data Base How To Do It?
                //I Want To Do Seeding Data In Data Base (Seeding fOR Only on time)
            }
            catch (Exception ex) {
                //Can Log Error Using Logger To Log Error (Logger Factory)
                //var Logger=_logger.CreateLogger<Program>();
                Logger.LogError(ex, "Error Accure when Try To Update Data Base or Appling Migration  From Program.c");
               // Console.WriteLine(ex.ToString());
            }
           //if i have some Static Data In json File in My Computer And Put It in Data Base How To Do It?
           //I Want To Do Seeding Data In Data Base (sEEDING fOR Only on time)
           app.UseMiddleware<ExceptionMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("Errors/{0}");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
