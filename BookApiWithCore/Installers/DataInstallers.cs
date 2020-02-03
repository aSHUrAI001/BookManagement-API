using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApiWithCore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookApiWithCore.Installers
{
    public class DataInstallers : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Data.DbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<Data.DbContext>();
            services.AddScoped<IPostService, PostService>();
        }
    }
}
