using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using vinodsharma.Entities;
using vinodsharma.Models;
using System.Data.Entity;
using System.Configuration;
using System.Data.SqlClient;

namespace vinodsharma.Utils
{
    public class Service
    {
        private ApplicationUserManager _userManager;
        private RoleManager<IdentityRole> roleManager;
        private ApplicationDbContext context;
        Random random = new Random();
        public Service(ApplicationUserManager userManager, ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager; this.context = context; this.roleManager = roleManager;
        }

        internal object GetInliner(string query)
        {
            return context.Members.Where(x => x.DistributerID.ToLower().Contains(query.ToLower())).Take(5).ToList().Select(x => new {
                id=x.MemberID,
                name = string.Format("{0} - {1}",x.DistributerID,x.FirstName+" "+x.LastName)
            }).ToList();
        }

        internal void CollectMember(Member member)
        {
            using (var transaction=context.Database.BeginTransaction())
            {                
                context.ApplyLock<Member>();
                var members = context.Database.SqlQuery<Member>("GetParents @memberID", new SqlParameter("@memberID", member.MemberID)).ToList();
                var parent = members.FirstOrDefault();
                var nonCollectedSublings = context.Members.Where(x => x.UplineId == parent.MemberID && x.HasCollected != true).ToList();
                var pointsCollected = nonCollectedSublings.Count / 2;
                nonCollectedSublings = nonCollectedSublings.Take(pointsCollected * 2).ToList();
                foreach (var item in nonCollectedSublings)
                {
                    item.HasCollected = true;
                }
                foreach (var item in members)
                {
                    if (item.IsAlways.GetValueOrDefault())
                    {
                        context.Members.Attach(item);
                        item.Points = item.Points + pointsCollected;
                    }
                    else if(item.IsActive.GetValueOrDefault())
                    {
                        var maxAmount = item.MaxValue.GetValueOrDefault();
                        var pointvalue = int.Parse(ConfigurationManager.AppSettings["PointValue"]);
                        for (int i = pointsCollected; i > 0; i--)
                        {
                            if ((item.Points + i) * pointvalue > maxAmount)
                            {
                                continue;
                            }
                            else {
                                context.Members.Attach(item);
                                item.Points = item.Points + i;
                                break;
                            }
                        }

                    }
                }
                context.Commit();
                transaction.Commit();
            }
        }

        internal Member CreateMember(CreateMemberViewModel model)
        {
            var user = new ApplicationUser() { Email = model.Email, UserName = model.Email };
            _userManager.Create(user);
            var member = new Member();
            member.User = user;
            member.FirstName = model.FirstName;
            member.LastName = model.LastName;
            member.IsActive = true;
            member.MaxValue = model.MaximumAmount;
            member.UplineId = context.Members.AsNoTracking().Single(x=>x.DistributerID==model.InlinerID).MemberID;
            context.Members.Add(member);
            using (var trans=context.Database.BeginTransaction())
            {
                context.ApplyLock<Member>();
                var distriID = CreateId();
                while (true)
                {
                    if (context.Members.Any(x => x.DistributerID == distriID))
                    {
                        distriID = CreateId();
                    }
                    else
                    {
                        member.DistributerID = distriID;
                        context.Commit();
                        trans.Commit();
                        break;
                    }
                }
            }            
            return member;
        }

        internal void VerifyInitializer(string inlinerID)
        {
           var member= context.Members.AsNoTracking().Where(x => x.DistributerID == inlinerID).SingleOrDefault();

            if (member==null)
            {
                throw new CustomException("Invalid Inliner");
            }
            if (!member.IsActive.GetValueOrDefault())
            {
                throw new CustomException("This inliner is not active.");
            }
            var pointvalue = int.Parse(ConfigurationManager.AppSettings["PointValue"]);
            if (!member.IsAlways.GetValueOrDefault() && (pointvalue * (member.Points + 1)) > member.MaxValue.GetValueOrDefault())
            {
                throw new CustomException("Can not add more members to this inliner");
            }
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
            using (var transaction = context.Database.BeginTransaction())
            {
                context.ApplyLock<Member>();
                if ((await _userManager.FindByEmailAsync(adminEmail)) == null)
                {

                    var adminUser = new ApplicationUser() { Email = adminEmail, UserName = adminEmail, EmailConfirmed = true };
                    await _userManager.CreateAsync(adminUser);
                    var adminMember = new Member();
                    adminMember.FirstName = "Vinod";
                    adminMember.LastName = "Sharma";
                    adminMember.User = adminUser;
                    adminMember.IsActive = true;
                    adminMember.Points = 1;
                    adminMember.IsAlways = true;
                    adminMember.DistributerID = CreateId();
                    context.Members.Add(adminMember);
                    adminUser.Roles.Add(new IdentityUserRole { RoleId = roleManager.FindByName(Roles.Admin.ToString()).Id, UserId = adminUser.Id });
                    var email = System.Configuration.ConfigurationManager.AppSettings["user2"];
                    var user = new ApplicationUser() { Email = email, UserName = email, EmailConfirmed = true };
                    await _userManager.CreateAsync(user);
                    var member = new Member();
                    member.FirstName = "Vinod";
                    member.LastName = "Sharma";
                    member.User = user;
                    member.IsActive = true;
                    var id1 = CreateId();
                    while (true)
                    {
                        if (id1 != adminMember.DistributerID)
                        {
                            break;
                        }
                        id1 = CreateId();
                    }
                    member.DistributerID = id1;
                    member.Upliner = adminMember;
                    member.IsAlways = true;
                    member.HasCollected = true;
                    context.Members.Add(member);
                    user.Roles.Add(new IdentityUserRole { RoleId = roleManager.FindByName(Roles.Customer.ToString()).Id, UserId = user.Id });
                    email = System.Configuration.ConfigurationManager.AppSettings["user3"];
                    user = new ApplicationUser() { Email = email, UserName = email, EmailConfirmed = true };
                    await _userManager.CreateAsync(user);
                    member = new Member();
                    member.FirstName = "Vinod";
                    member.LastName = "Sharma";
                    member.User = user;
                    member.IsActive = true;
                    var id2 = CreateId();
                    while (true)
                    {
                        if (id1 != id2)
                        {
                            break;
                        }
                        id2 = CreateId();
                    }
                    member.DistributerID = id2;
                    member.Upliner = adminMember;
                    member.IsAlways = true;
                    member.HasCollected = true;
                    context.Members.Add(member);
                    user.Roles.Add(new IdentityUserRole { RoleId = roleManager.FindByName(Roles.Customer.ToString()).Id, UserId = user.Id });
                    context.Commit();
                    transaction.Commit();
                }
            }
            
        }

        public async Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(userId, token, newPassword);
            context.Commit();
            return result;
        }

        public List<UserlistviewModel> GetUserList()
        {

            return context.Members.Include(x => x.Upliner).Select(x => new UserlistviewModel
            {
                FullName = x.FirstName + " " + x.LastName,
                InlinerName = x.Upliner != null ? x.Upliner.FirstName + " " + x.Upliner.LastName : null,
                IsActive = x.IsActive.HasValue ? x.IsActive.Value : false,
                IntlinerID = x.Upliner != null ? x.Upliner.DistributerID : null,
                MembershipID = x.DistributerID,
                Points = x.Points,
                MemberID = x.MemberID,
                IsAlways = x.IsAlways.HasValue ? x.IsAlways.Value : false
            }).ToList();
        }

        public string CreateId()
        {
            
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var id= "VS"+ new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());            
            return id;
        }
    }
}