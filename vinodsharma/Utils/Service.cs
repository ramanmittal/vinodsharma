using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using vinodsharma.Entities;
using vinodsharma.Models;

namespace vinodsharma.Utils
{
    public class Service
    {
        private ApplicationUserManager _userManager;
        private RoleManager<IdentityRole> roleManager;
        private ApplicationDbContext context;
        public Service(ApplicationUserManager userManager, ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager; this.context = context; this.roleManager = roleManager;
        }
        public async Task Initialize()
        {
            var adminEmail = System.Configuration.ConfigurationManager.AppSettings["user1"];

            if (!await roleManager.RoleExistsAsync(Roles.Admin.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            }
            if (!await roleManager.RoleExistsAsync(Roles.Customer.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Customer.ToString()));
            }
            context.Commit();
            if ((await _userManager.FindByEmailAsync(adminEmail)) == null)
            {
                var adminUser = new ApplicationUser() { Email = adminEmail, UserName = adminEmail, EmailConfirmed = true };
                await _userManager.CreateAsync(adminUser);
                var adminMember = new Member();
                adminMember.FirstName = "Vinod";
                adminMember.LastName = "Sharma";
                adminMember.User = adminUser;
                context.Members.Add(adminMember);
                adminUser.Roles.Add(new IdentityUserRole { RoleId = roleManager.FindByName(Roles.Admin.ToString()).Id, UserId = adminUser.Id });
                var email = System.Configuration.ConfigurationManager.AppSettings["user2"];
                var user = new ApplicationUser() { Email = email, UserName = email, EmailConfirmed = true };
                await _userManager.CreateAsync(user);
                var member = new Member();
                member.FirstName = "Vinod";
                member.LastName = "Sharma";
                member.User = user;
                member.Upliner = adminMember;
                context.Members.Add(member);
                user.Roles.Add(new IdentityUserRole { RoleId = roleManager.FindByName(Roles.Customer.ToString()).Id, UserId = user.Id });
                email = System.Configuration.ConfigurationManager.AppSettings["user3"];
                user = new ApplicationUser() { Email = email, UserName = email, EmailConfirmed = true };
                await _userManager.CreateAsync(user);
                member = new Member();
                member.FirstName = "Vinod";
                member.LastName = "Sharma";
                member.User = user;
                member.Upliner = adminMember;
                context.Members.Add(member);
                user.Roles.Add(new IdentityUserRole { RoleId = roleManager.FindByName(Roles.Customer.ToString()).Id, UserId = user.Id });
                context.Commit();
            }
        }

        public async Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(userId, token, newPassword);
            context.Commit();
            return result;
        }
    }
}