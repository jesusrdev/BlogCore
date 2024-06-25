using BlogCore.Data;
using BlogCore.Models;
using BlogCore.Utilidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Inicializador
{
    public class InicializadorBD : IInicializadorBD
    {
        private readonly ApplicationDbContext _bd;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserManager<IdentityRole> _roleManager;


        public InicializadorBD(ApplicationDbContext bd, UserManager<ApplicationUser> userManager, UserManager<IdentityRole> roleManager)
        {
            _bd = bd;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public void Inicializar()
        {
            try
            {
                if (_bd.Database.GetPendingMigrations().Count() > 0)
                {
                    _bd.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }

            if (_bd.Roles.Any(ro => ro.Name == CNT.Administrador)) return;

            // Creacion de roles
            _roleManager.CreateAsync(new IdentityRole(CNT.Administrador)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(CNT.Registrado)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(CNT.Cliente)).GetAwaiter().GetResult();


            // Creacion del usuario inicial
            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "rojasalarcone@gmail.com",
                Email = "rojasalarcone@gmail.com",
                EmailConfirmed = true,
                Nombre = "Jesus",
            }, "Admin-123").GetAwaiter().GetResult();


            ApplicationUser usuario = _bd.ApplicationUser.Where(u => u.Email == "rojasalarcone@gmail.com").FirstOrDefault();
            _userManager.AddToRoleAsync(usuario, CNT.Administrador);
        }
    }
}
