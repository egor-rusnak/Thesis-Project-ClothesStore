using ClothesStore.Domain.Interfaces;
using ClothesStore.Domain.Services;
using ClothesStore.Infrastructure.Data;
using ClothesStore.Infrastructure.Services;
using ClothesStore.WebUI.Data;
using ClothesStore.WebUI.Models;
using ClothesStore.WebUI.Models.Identity;
using ClothesStore.WebUI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ClothesStore.WebUI
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
            services.AddMvc().AddRazorRuntimeCompilation();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).UseLazyLoadingProxies());

            services.AddDbContext<UserContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("UserConnection")));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<UserContext>();

            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddTransient<ITimerService, TimerService>();
            services.AddSingleton<ITopClothesService, TopClothesService>();
            services.AddScoped(sp => SessionCart.GetCart(sp));
            services.Configure<IdentityOptions>(e =>
            {
                e.Password.RequireNonAlphanumeric = false;
                e.Password.RequiredLength = 6;
            });
            services.AddScoped<IClothesRepository, ClothesRepository>();
            services.AddTransient<IdentityService>();

            services.AddScoped<IClothesService, ClothesService>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", p => p.RequireClaim("access", Models.Identity.Role.Admin.ToString()));
                options.AddPolicy("Manager", p => p.RequireClaim("access", new[] { Models.Identity.Role.Manager.ToString(), Models.Identity.Role.Admin.ToString() }));
                options.AddPolicy("User", p => p.RequireClaim("access", Models.Identity.Role.User.ToString()));
            });
            services.AddLogging();
            services.AddMemoryCache(); //now can use services  of cache memory 
            services.AddSession(); //turn on session services 
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();// includes sessions to requests

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "clohtes",
                    pattern: "{controller=Clothes}/{action=ClothesList}/{category?}/{type?}"
                    );
            });

            /*  StartAppSerivces(app.ApplicationServices);*/
        }
        /*  private void StartAppSerivces(IServiceProvider provider)
          {
              var userMn=provider.GetRequiredService<UserManager<User>>();
              var resultUsers = userMn.GetUsersForClaimAsync(new System.Security.Claims.Claim("access", Models.Identity.Role.Admin.ToString())).Result;
              if (resultUsers.Count == 0)
              {
                  User user = new User { Email = "Admin1234@mail.com", UserName = "Admin1234", PhoneNumber = "35423321" };
                  var result = userMn.CreateAsync(user, "n123321N").Result;
                  var resultClaim= userMn.AddClaimAsync(user, new System.Security.Claims.Claim("access", Models.Identity.Role.Admin.ToString())).Result;
                  if (result.Succeeded)
                      Console.WriteLine("Created a admin user for first time with username: " + user.UserName + " and pass: n123321N");

                  if (resultClaim.Succeeded)
                      Console.WriteLine("Added admin claim for it!");
              }
          }*/
    }
}
