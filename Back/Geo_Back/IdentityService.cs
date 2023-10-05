using System.ComponentModel.DataAnnotations;
using Geo_Back.DBs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace Geo_Back
{
    public class IdentityService
    {

        private readonly UsersDB _db;

        public IdentityService(UsersDB usersDB) 
        {
            _db = usersDB;
        }

        public class LoginResultModelView
        {
            public bool IsSuccess { get; set; }

            public string? UserType { get; set; }

            public Guid? Id { get; set; }

            static public LoginResultModelView UnSuccessful { get;} = new LoginResultModelView()
            {
                Id = null,
                IsSuccess = false,
                UserType = null
            };
        }
        public class LoginModelView
        {
            [Required]
            public string Login { get; set; } = null!;

            [Required]
            public string Password { get; set; } = null!;
        }

        public async Task<LoginResultModelView> Login(LoginModelView loginModelView)
        {
            var user = await _db.Users.AsNoTracking()
                .Where(x => x.Login == loginModelView.Login)
                .FirstOrDefaultAsync();
            if (user == null)
                return LoginResultModelView.UnSuccessful;
            if (user?.Password != loginModelView.Password) 
                return LoginResultModelView.UnSuccessful;
            Console.WriteLine(user.GetType().Name);
            return new LoginResultModelView {Id = user.Id, IsSuccess = true, UserType = user.GetType().Name };
        }

    }
}
