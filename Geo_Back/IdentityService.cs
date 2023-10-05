using System.ComponentModel.DataAnnotations;
using Geo_Back.DBs;
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

            
        }
        public class LoginModelView
        {
            [Required]
            public string Login { get; set; } = null!;

            [Required]
            public string Password { get; set; } = null!;
        }

        public async LoginResultModelView Login(LoginModelView loginModelView)
        {
            var user = await _db.Users.FindAsync(loginModelView.Login);
            if (user == null) return new LoginResultModelView(){Id = , IsSuccess = };
            if (user?.Password == loginModelView.Password)
            {

            }
            return new LoginResultModelView { };
        }

    }
}
