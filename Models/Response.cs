using System.ComponentModel.DataAnnotations.Schema;

namespace AmadeusG3_Neo_Tech_BackEnd.Models{

    [NotMapped]
    public class Response{
        
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
    }
    
}