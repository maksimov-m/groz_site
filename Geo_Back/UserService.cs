using Geo_Back.DBs;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Geo_Back
{
    public class UserService
    {
        private readonly UsersDB _db;

        public UserService(UsersDB usersDB) 
        {
            _db = usersDB;
        }

        
    }
}
