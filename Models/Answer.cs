namespace AmadeusG3_Neo_Tech_BackEnd.Models{

    public class Answer
    {
        public int Id { get; set; }
        public DateTime RegistDate { get; set; } = DateTime.UtcNow;

        //Relación 1 a muchos con User
        public User User { get; set; } = new User();

        //Relación 1 a muchos con Question
        public Question Question { get; set; } = new Question();

        //Relación 1 a muchos con Question_Options
        public Question_Options Question_Option { get; set; } = new Question_Options();
        
    }
}