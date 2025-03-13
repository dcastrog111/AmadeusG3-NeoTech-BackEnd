using System.ComponentModel.DataAnnotations.Schema;

namespace AmadeusG3_Neo_Tech_BackEnd.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Full_Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Tipo_Usuario Tipo_Usuario { get; set; }

        [NotMapped]
        public ICollection<Answer> Answers { get; set; } = [];
    }
}