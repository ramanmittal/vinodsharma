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
        private ApplicationDbContext context;
        public Service(ApplicationUserManager userManager, ApplicationDbContext context)
        {
            _userManager = userManager; this.context = context;
        }
        public async Task Initialize()
        {
            var adminEmail = System.Configuration.ConfigurationManager.AppSettings["user1"];
            if ((await _userManager.FindByEmailAsync(adminEmail)) == null)
            {
                var adminUser = new ApplicationUser() { Email = adminEmail, UserName = adminEmail, EmailConfirmed = true };
                await _userManager.CreateAsync(adminUser);
                var adminMember = new Member();
                adminMember.FirstName = "Vinod";
                adminMember.LastName = "Sharma";
                adminMember.User = adminUser;
                context.Members.Add(adminMember);

                var email = System.Configuration.ConfigurationManager.AppSettings["user2"];
                var user = new ApplicationUser() { Email = email, UserName = email, EmailConfirmed = true };
                await _userManager.CreateAsync(user);
                var member = new Member();
                member.FirstName = "Vinod";
                member.LastName = "Sharma";
                member.User = user;
                member.Upliner = adminMember;
                context.Members.Add(member);

                email = System.Configuration.ConfigurationManager.AppSettings["user3"];
                user = new ApplicationUser() { Email = email, UserName = email, EmailConfirmed = true };
                await _userManager.CreateAsync(user);
                member = new Member();
                member.FirstName = "Vinod";
                member.LastName = "Sharma";
                member.User = user;
                member.Upliner = adminMember;
                context.Members.Add(member);

                context.Commit();
            }
        }
    }
}