using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebStore.DomainNew.Dto.User;
using WebStore.DomainNew.Entities;
using WebStore.Interfaces;

namespace WebStore.Clients.Services
{
 
    public class UsersClient:BaseClient,IUsersClient
    {
        protected override string ServiceAddress { get; } = "api/users";
        public UsersClient(IConfiguration configuration) : base(configuration)
        {
        }
        #region IUserStore
        public async Task<string> GetUserIdAsync(User user, CancellationToken
            cancellationToken)
        {

            var url = $"{ServiceAddress}/userId";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>();
        }
        public async Task<string> GetUserNameAsync(User user, CancellationToken
            cancellationToken)
        {
            var url = $"{ServiceAddress}/userName";
            var result = await PostAsync(url, user);
            var ret = await result.Content.ReadAsAsync<string>();
            return ret;
        }
        public Task SetUserNameAsync(User user, string userName,
            CancellationToken cancellationToken)
        {
            user.UserName = userName;
            var url = $"{ServiceAddress}/userName/{userName}";
            return PostAsync(url, user);
        }
        public async Task<string> GetNormalizedUserNameAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/normalUserName";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>();
        }
        public Task SetNormalizedUserNameAsync(User user, string normalizedName,
            CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            var url = $"{ServiceAddress}/normalUserName/{normalizedName}";
            return PostAsync(url, user);
        }
        public async Task<IdentityResult> CreateAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/user";
            var result = await PostAsync(url, user);
            var ret = await result.Content.ReadAsAsync<bool>();
            return ret ? IdentityResult.Success : IdentityResult.Failed();
        }
        public async Task<IdentityResult> UpdateAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/user";
            var result = await PutAsync(url, user);

            var ret = await result.Content.ReadAsAsync<bool>();
            return ret ? IdentityResult.Success : IdentityResult.Failed();
        }
        public async Task<IdentityResult> DeleteAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/user/delete";
            var result = await PostAsync(url, user);
            var ret = await result.Content.ReadAsAsync<bool>();
            return ret ? IdentityResult.Success : IdentityResult.Failed();
        }
        public Task<User> FindByIdAsync(string userId, CancellationToken
            cancellationToken)
        {
            var url = $"{ServiceAddress}/user/find/{userId}";
            return GetAsync<User>(url);
        }
        public async Task<User> FindByNameAsync(string normalizedUserName,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/user/normal/{normalizedUserName}";
            var result = await GetAsync<User>(url);
            return result;
        }
        #endregion
        #region IUserRoleStore
        public Task AddToRoleAsync(User user, string roleName, CancellationToken
            cancellationToken)
        {
            var url = $"{ServiceAddress}/role/{roleName}";
            return PostAsync(url, user);
        }
        public Task RemoveFromRoleAsync(User user, string roleName,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/delete/{roleName}";
            return PostAsync(url, user);
        }
        public async Task<IList<string>> GetRolesAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/roles";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<IList<string>>();
        }
        public async Task<bool> IsInRoleAsync(User user, string roleName,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/inrole/{roleName}";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>();
        }
        public async Task<IList<User>> GetUsersInRoleAsync(string roleName,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/usersInRole/{roleName}";
            IList<User> result = await GetAsync<List<User>>(url);
            return result.ToList();
        }
        #endregion
        #region IUserPasswordStore
        public async Task SetPasswordHashAsync(User user, string passwordHash,
            CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            var url = $"{ServiceAddress}/setPasswordHash";
            await PostAsync(url, new PasswordHashDto()
            {
                User = user,
                Hash =
                    passwordHash
            });
        }
        public async Task<string> GetPasswordHashAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getPasswordHash";
            var result = await PostAsync(url, user);
            var ret = await result.Content.ReadAsAsync<string>();
            return ret;
        }
        public async Task<bool> HasPasswordAsync(User user, CancellationToken
            cancellationToken)
        {
            var url = $"{ServiceAddress}/hasPassword";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>();
        }
        #endregion
        #region IUserClaimStore
        public async Task<IList<Claim>> GetClaimsAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getClaims";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<List<Claim>>();
        }
        public Task AddClaimsAsync(User user, IEnumerable<Claim> claims,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/addClaims";
            return PostAsync(url, new AddClaimsDto()
            {
                User = user,
                Claims =
                    claims
            });
        }
        public Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/replaceClaim";
            return PostAsync(url, new ReplaceClaimsDto()
            {
                User = user,
                Claim =
                    claim,
                NewClaim = newClaim
            });
        }
        public Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/removeClaims";
            return PostAsync(url, new RemoveClaimsDto()
            {
                User = user,
                Claims =
                    claims
            });
        }
        public async Task<IList<User>> GetUsersForClaimAsync(Claim claim,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getUsersForClaim";
            var result = await PostAsync(url, claim);
            return await result.Content.ReadAsAsync<List<User>>();
        }
        #endregion
        #region IUserTwoFactorStore
        public Task SetTwoFactorEnabledAsync(User user, bool enabled,
            CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
            var url = $"{ServiceAddress}/setTwoFactor/{enabled}";
            return PostAsync(url, user);
        }
        public async Task<bool> GetTwoFactorEnabledAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getTwoFactorEnabled";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>();
        }
        #endregion
        #region IUserEmailStore
        public Task SetEmailAsync(User user, string email, CancellationToken
            cancellationToken)
        {
            user.Email = email;
            var url = $"{ServiceAddress}/setEmail/{email}";
            return PostAsync(url, user);
        }
        public async Task<string> GetEmailAsync(User user, CancellationToken
            cancellationToken)
        {
            var url = $"{ServiceAddress}/getEmail";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>();
        }
        public async Task<bool> GetEmailConfirmedAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getEmailConfirmed";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>();
        }
        public Task SetEmailConfirmedAsync(User user, bool confirmed,
            CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            var url = $"{ServiceAddress}/setEmailConfirmed/{confirmed}";
            return PostAsync(url, user);
        }
        public Task<User> FindByEmailAsync(string normalizedEmail,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/user/findByEmail/{normalizedEmail}";
            return GetAsync<User>(url);
        }
        public async Task<string> GetNormalizedEmailAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getNormalizedEmail";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>();
        }
        public Task SetNormalizedEmailAsync(User user, string normalizedEmail,
            CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            var url = $"{ServiceAddress}/setEmail/{normalizedEmail}";
            return PostAsync(url, user);
        }
        #endregion
        #region IUserPhoneNumberStore
        public Task SetPhoneNumberAsync(User user, string phoneNumber,
            CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            var url = $"{ServiceAddress}/setPhoneNumber/{phoneNumber}";
            return PostAsync(url, user);
        }
        public async Task<string> GetPhoneNumberAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getPhoneNumber";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>();
        }
        public async Task<bool> GetPhoneNumberConfirmedAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getPhoneNumberConfirmed";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>();
        }
        public Task SetPhoneNumberConfirmedAsync(User user, bool confirmed,
            CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            var url = $"{ServiceAddress}/setPhoneNumberConfirmed/{confirmed}";
            return PostAsync(url, user);
        }
        #endregion
        #region IUserLoginStore
        public Task AddLoginAsync(User user, UserLoginInfo login,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/addLogin";
            return PostAsync(url, new AddLoginDto()
            {
                User = user,
                UserLoginInfo = login
            });
        }
        public Task RemoveLoginAsync(User user, string loginProvider, string
            providerKey, CancellationToken cancellationToken)
        {
            var url =
                $"{ServiceAddress}/removeLogin/{loginProvider}/{providerKey}";
            return PostAsync(url, user);
        }
        public async Task<IList<UserLoginInfo>> GetLoginsAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getLogins";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<List<UserLoginInfo>>();
        }
        public Task<User> FindByLoginAsync(string loginProvider, string
            providerKey, CancellationToken cancellationToken)
        {
            var url =
                $"{ServiceAddress}/user/findbylogin/{loginProvider}/{providerKey}";
            return GetAsync<User>(url);
        }
        #endregion
        #region IUserLockoutStore
        public async Task<DateTimeOffset?> GetLockoutEndDateAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getLockoutEndDate";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<DateTimeOffset?>();
        }
        public Task SetLockoutEndDateAsync(User user, DateTimeOffset?
            lockoutEnd, CancellationToken cancellationToken)
        {
            user.LockoutEnd = lockoutEnd;
            var url = $"{ServiceAddress}/setLockoutEndDate";
            return PostAsync(url, new SetLockoutDto()
            {
                User = user,
                LockoutEnd
                    = lockoutEnd
            });
        }
        public async Task<int> IncrementAccessFailedCountAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/IncrementAccessFailedCount";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<int>();
        }
        public Task ResetAccessFailedCountAsync(User user, CancellationToken
            cancellationToken)
        {
            var url = $"{ServiceAddress}/ResetAccessFailedCount";
            return PostAsync(url, user);
        }
        public async Task<int> GetAccessFailedCountAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/GetAccessFailedCount";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<int>();
        }
        public async Task<bool> GetLockoutEnabledAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/GetLockoutEnabled";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>();
        }
        public async Task SetLockoutEnabledAsync(User user, bool enabled,
            CancellationToken cancellationToken)
        {
            user.LockoutEnabled = enabled;
            var url = $"{ServiceAddress}/SetLockoutEnabled/{enabled}";
            await PostAsync(url, user);
        }
        #endregion
        public void Dispose()
        {
            Client.Dispose();
        }

    }
}
