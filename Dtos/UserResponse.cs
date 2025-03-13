namespace AmadeusG3_Neo_Tech_BackEnd.Dtos{

    public class UserResponse{

        public int Id { get; set; } 
        public string Full_Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Tipo_Usuario { get; set; }
    }
}