using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IELChak.Data;
using IELChak.Models;
using Microsoft.AspNetCore.Identity;

namespace IELChak.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        UserManager<ApplicationUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        UsuarioRole _usuarioRole;

        public List<SelectListItem> usuarioRole;

        public UsuariosController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _usuarioRole = new UsuarioRole();
            usuarioRole = new List<SelectListItem>();
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var ID = "";
            List<Usuario> usuario = new List<Usuario>();
            var appUsuario = await _context.ApplicationUser.ToListAsync();
            foreach(var Data in appUsuario)
            {
                ID = Data.Id;
                usuarioRole = await _usuarioRole.GetRole(_userManager, _roleManager, ID);

                usuario.Add(new Usuario()
                {
                    Id = Data.Id,
                    UserName = Data.UserName,
                    PhoneNumber = Data.PhoneNumber,
                    Email = Data.Email,
                    Role = usuarioRole[0].Text
                });
            }

            return View(usuario.ToList());
        }

        public async Task<List<Usuario>> GetUsuario(string id)
        {
            List<Usuario> usuario = new List<Usuario>();
            var appUsuario = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            usuarioRole = await _usuarioRole.GetRole(_userManager, _roleManager, id);

            usuario.Add(new Usuario()
            {
                Id = appUsuario.Id,
                UserName = appUsuario.UserName,
                PhoneNumber = appUsuario.PhoneNumber,
                Email = appUsuario.Email,
                Role = usuarioRole[0].Text,
                RoleId = usuarioRole[0].Value,
                AccessFailedCount = appUsuario.AccessFailedCount,
                ConcurrencyStamp = appUsuario.ConcurrencyStamp,
                EmailConfirmed = appUsuario.EmailConfirmed,
                LockoutEnabled = appUsuario.LockoutEnabled,
                LockoutEnd = appUsuario.LockoutEnd,
                NormalizedEmail = appUsuario.NormalizedEmail,
                NormalizedUserName = appUsuario.NormalizedUserName,
                PasswordHash = appUsuario.PasswordHash,
                PhoneNumberConfirmed = appUsuario.PhoneNumberConfirmed,
                SecurityStamp = appUsuario.SecurityStamp,
                TwoFactorEnabled = appUsuario.TwoFactorEnabled,
            });

            return usuario;
        }

        public async Task<List<SelectListItem>> GetRoles()
        {
            List<SelectListItem> rolesLista = new List<SelectListItem>();
            rolesLista = _usuarioRole.Roles(_roleManager);

            return rolesLista;
        }

        public async Task<string> EditUsuario(string id, string userName, string email, string phoneNumber, int accessFailedCount, string concurrerncyStamp, bool emailConfirmed, bool lockoutEnabled, DateTimeOffset
            lockoutEnd, string normalizedEmail, string normalizedUsedName, string passwordHash, bool phoneNumberConfirmed, string securityStamp, bool twoFactorEnabled, string selectRole, ApplicationUser applicationUser)
        {
            var resp = "";
            try
            {
                applicationUser = new ApplicationUser
                {
                    Id = id,
                    UserName = userName,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    EmailConfirmed = emailConfirmed,
                    LockoutEnabled = lockoutEnabled,
                    LockoutEnd = lockoutEnd,
                    NormalizedEmail = normalizedEmail,
                    NormalizedUserName = normalizedUsedName,
                    PasswordHash = passwordHash,
                    PhoneNumberConfirmed = phoneNumberConfirmed,
                    SecurityStamp = securityStamp,
                    TwoFactorEnabled = twoFactorEnabled,
                    AccessFailedCount = accessFailedCount,
                    ConcurrencyStamp = concurrerncyStamp,
                };

                _context.Update(applicationUser);
                await _context.SaveChangesAsync();
                var usuario = await _userManager.FindByIdAsync(id);
                usuarioRole = await _usuarioRole.GetRole(_userManager, _roleManager, id);

                if(usuarioRole[0].Text != "No Role")
                {
                    await _userManager.RemoveFromRoleAsync(usuario, usuarioRole[0].Text);
                }
                if(selectRole == "No Role")
                {
                    selectRole = "Usuario";
                }
                var resultado = await _userManager.AddToRoleAsync(usuario, selectRole);

                resp = "Save";
            }
            catch
            {
                resp = "No Save";
            }

            return resp;
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }
    }
}
