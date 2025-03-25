namespace AmadeusG3_Neo_Tech_BackEnd.Dtos
{
    public class QuestionOptionCount
    {
        public string QuestionOptionText { get; set; } = string.Empty;
        public int Question { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}