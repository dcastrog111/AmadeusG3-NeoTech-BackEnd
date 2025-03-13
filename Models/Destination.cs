namespace AmadeusG3_Neo_Tech_BackEnd.Models
{
    public class Destination{

        public int Id { get; set; }
        public string Combination { get; set; } = string.Empty;
        public int First_City_Id { get; set; } 
        public int Second_City_Id { get; set; }
    }

}