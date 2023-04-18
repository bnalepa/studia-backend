namespace WebAPI.DTO;

public class QuizUserAnswerDto
{
    public int QuizId { get; set; }
    public int UserId { get; set; }
    public List<QuizItemUserAnswersDto> QuizAnswers { get; set; }
}