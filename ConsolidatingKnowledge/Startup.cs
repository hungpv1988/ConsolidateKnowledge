using ConsolidatingKnowledge.DI;
using ConsolidatingKnowledge.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http.Features;
using ConsolidatingKnowledge.CustomFormatter;
using Microsoft.AspNetCore.Mvc;
using ConsolidatingKnowledge.Extensions;
using Microsoft.Net.Http.Headers;

namespace ConsolidatingKnowledge
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
            // register controller with view, and add a filter to all controller
            //services.AddControllersWithViews(o => o.Filters.Add(new AuthorizeFilter()));

            services.AddRazorPages();
            //services.AddDbContext<DBContext>(options =>
            //     options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddOptions<AppConfiguration>().Bind(Configuration.GetSection("AppConfiguration"))
                                                   .ValidateDataAnnotations()
                                                   .Validate(appconfig =>
                                                   {
                                                       return appconfig.AppName.ToString().Length > 0;
                                                   });
            // code to show how to use other methods instead of binding.
            // services.Configure<AppConfiguration>(Configuration.GetSection("AppConfiguration"));
            // Configuration.GetSection("AppConfiguration").Get<AppConfiguration>();
         
            
            services.AddSingleton<IStudentService, StudentService>();
            //services.AddTransient<IStudentService, TransientStudentService>();
            //services.AddScoped<IStudentService, ScopedStudentService>();

            // code segment to add factory to DI
            //services.TryAddSingleton<IStudentService>(sp =>
            //{
            //    var option = sp.GetRequiredService<IOptions<AppConfiguration>>().Value;
            //    if (option.DIType == 1) 
            //    {
            //        return new StudentService();
            //    }
            //    else if (option.DIType == 2) 
            //    {
            //        return new TransientStudentService();
            //    }

            //    return new ScopedStudentService();
            //});


            // Test DI for Middleware, if changed to scope -> throw exception. For Transient, no exception thrown.
            services.AddSingleton<MiddleWareDIService>();

            //register required services for authentication service, and return an authentication builder
            services.AddAuthentication().AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

            // replace default system.text.json and add a custom input formatter
            services.AddMvc(option =>
                                                {
                                                    option.InputFormatters.Add(new FileInfoXmlInputFormatter());
                                                    option.OutputFormatters.Insert(0, new FileInfoOutputFormatter());
                                                })
                    .AddNewtonsoftJson();

            services.Configure<FormOptions>(x =>
           {
               x.MultipartBodyLengthLimit = int.MaxValue;
               x.ValueCountLimit = 10;
               x.ValueLengthLimit = int.MaxValue;
               x.MemoryBufferThreshold = int.MaxValue;
           });
        }

      

        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            // register middleware to handle authentication so that user object can be set. 
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<MiddlewareDI>();

            // Test that set routedata here would override original value localhost/student/5/hung -> 5 would be replaced by 8
            //app.Use(async (context, next) =>
            //{
            //    var routeData = context.GetRouteData();
            //    routeData.Values["id"] = 8;
            //    await next();
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }

   

   
}
