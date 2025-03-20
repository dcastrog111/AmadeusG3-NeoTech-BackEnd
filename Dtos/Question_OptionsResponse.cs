namespace AmadeusG3_Neo_Tech_BackEnd.Dtos{
    
    public class Question_OptionsResponse{

        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Dato { get; set; } = string.Empty;
        public string UrlImg { get; set; } = string.Empty;
        public int QuestionId { get; set; }
    }

}