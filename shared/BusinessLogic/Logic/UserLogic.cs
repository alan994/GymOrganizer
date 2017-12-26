using BusinessLogic.Model;
using Data.Db;
using Data.Db.Queries;
using Data.Model;
using Helper.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Logger;

namespace BusinessLogic.Logic
{
    public class UserLogic
    {
        private readonly GymOrganizerContext db;
        private readonly Dictionary<string, string> additionalData;
        private readonly ILogger<UserLogic> logger;
        private readonly AppSettings settings;

        public UserLogic(GymOrganizerContext db, Dictionary<string, string> additionalData, ILoggerFactory loggerFactory, AppSettings settings)
        {
            this.db = db;
            this.additionalData = additionalData;
            this.logger = loggerFactory.CreateLogger<UserLogic>();
            this.settings = settings;
        }


        public async Task<Guid> AddUser(UserQueue userQueue)
        {
            using (var client = new HttpClient())
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(userQueue), Encoding.UTF8, "application/json");
                try
                {

                    HttpResponseMessage httpResponse = await client.PostAsync($"{this.settings.AuthApplicationUrl}/api/users", stringContent);
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        var responseStr = await httpResponse.Content.ReadAsStringAsync();
                        var id = Guid.Parse(responseStr.Replace('"', ' ').Trim());
                        this.logger.LogCustomInformation($"User '{userQueue.FirstName} {userQueue.LastName}' - '{userQueue.Email}' with id '{id}' has successfully created", userQueue.TenantId.ToString(), userQueue.UserPerformingAction.ToString());
                        return id;
                    }
                    else
                    {
                        throw new Exception($"Failed to add user. MESSAGE: {await httpResponse.Content.ReadAsStringAsync()}");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to add user. MESSAGE: {ex.Message}");
                }
            }
        }

        public async Task EditUser(UserQueue userQueue)
        {
            User user = await this.db.GetUserById(userQueue.TenantId, userQueue.Id.Value).FirstOrDefaultAsync();
            user.FirstName = userQueue.FirstName;
            user.LastName = userQueue.LastName;
            user.Email = userQueue.Email;
            user.NormalizedEmail = userQueue.Email.ToUpper().Trim();
            user.UserName = userQueue.Email;
            user.NormalizedUserName = userQueue.Email.ToUpper().Trim();
            user.Status = userQueue.Status;
            user.Owed = userQueue.Owed;
            user.Claimed = userQueue.Claimed;

            var userRoles = await this.db.UserRoles.Where(x => x.UserId == user.Id).ToListAsync();
            var applicationRoles = await this.db.Roles.ToListAsync();

            foreach (var role in applicationRoles)
            {
                if (role.NormalizedName == "ADMINISTRATOR")
                {
                    if (userQueue.IsAdmin)
                    {
                        if (userRoles.FirstOrDefault(x => x.RoleId == role.Id) == null)
                        {
                            var ur = new IdentityUserRole<Guid>
                            {
                                RoleId = role.Id,
                                UserId = user.Id
                            };
                            this.db.UserRoles.Add(ur);
                        }
                    }
                    else
                    {
                        var userRole = userRoles.FirstOrDefault(x => x.RoleId == role.Id);
                        if (userRole != null)
                        {
                            this.db.UserRoles.Remove(userRole);
                        }
                    }
                }

                if (role.NormalizedName == "COACH")
                {
                    if (userQueue.IsCoach)
                    {
                        if (userRoles.FirstOrDefault(x => x.RoleId == role.Id) == null)
                        {
                            var ur = new IdentityUserRole<Guid>
                            {
                                RoleId = role.Id,
                                UserId = user.Id
                            };
                            this.db.UserRoles.Add(ur);
                        }
                    }
                    else
                    {
                        var userRole = userRoles.FirstOrDefault(x => x.RoleId == role.Id);
                        if (userRole != null)
                        {
                            this.db.UserRoles.Remove(userRole);
                        }
                    }
                }

                if (role.NormalizedName == "GLOBAL_ADMIN")
                {
                    if (userQueue.IsGlobalAdmin)
                    {
                        if (userRoles.FirstOrDefault(x => x.RoleId == role.Id) == null)
                        {
                            var ur = new IdentityUserRole<Guid>
                            {
                                RoleId = role.Id,
                                UserId = user.Id
                            };
                            this.db.UserRoles.Add(ur);
                        }
                    }
                    else
                    {
                        var userRole = userRoles.FirstOrDefault(x => x.RoleId == role.Id);
                        if (userRole != null)
                        {
                            this.db.UserRoles.Remove(userRole);
                        }
                    }
                }
            }

            await this.db.SaveChangesAsync();

            this.logger.LogCustomInformation($"User '{user.FirstName} {user.LastName}' - '{user.Email}' with id '{user.Id}' has successfully created", userQueue.TenantId.ToString(), userQueue.UserPerformingAction.ToString());
        }

        public async Task DeleteUser(UserQueue userQueue)
        {
            User user = await this.db.GetUserById(userQueue.TenantId, userQueue.Id.Value).FirstOrDefaultAsync();
            user.Status = Data.Enums.ExistenceStatus.Deleted;

            await this.db.SaveChangesAsync();

            this.logger.LogCustomInformation($"User '{user.FirstName} {user.LastName}' - '{user.Email}' with id '{user.Id}' has successfully deleted", userQueue.TenantId.ToString(), userQueue.UserPerformingAction.ToString());
        }
    }
}
