
using System.ComponentModel.DataAnnotations;

namespace Geo_Back.Models
{
    public class UserModel
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        /// <summary>
        /// Будущее не трогать!!!
        /// </summary>
        public Guid? ProfileImageId = null;

        [Required]
        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
