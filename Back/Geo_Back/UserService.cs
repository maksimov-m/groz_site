using Geo_Back.DBs;
using Geo_Back.Models;
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

        public async Task<bool> AddUser(TeacherModel model)
        {
            await _db.Teachers.AddAsync(model);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            Console.WriteLine("Teacher was added!");
            return true;
        }

        public async Task<bool> AddUser(StudentModel model)
        {
            await _db.Students.AddAsync(model);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            Console.WriteLine("Student was added!");
            return true;
        }

    }
}
