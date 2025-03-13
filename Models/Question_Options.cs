namespace AmadeusG3_Neo_Tech_BackEnd.Models{

    public class Question_Options
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Dato { get; set; } = string.Empty;
        public string UrlImg { get; set; } = string.Empty;

        //relación de 1 a muchos con question
        public Question Question { get; set; } = new Question();
    
    }
}