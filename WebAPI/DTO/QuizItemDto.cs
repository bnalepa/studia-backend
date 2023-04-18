using ApplicationCore.Models;

namespace WebAPI.DTO;

public class QuizItemDto
{
    public int Id { get; set; }
    public string Question { get; set; }
    public string CorrectAnswer { get; set; }
    public IEnumerable<string> Options { get; set; }

    public QuizItemDto(int id, string question, IEnumerable<string> options)
    {
        Id = id;
        Question = question;
        Options = options;
    }

    public static QuizItemDto of(QuizItem quiz) => new QuizItemDto

            (
            id: quiz.Id,
            question: quiz.Question,
            options: quiz.IncorrectAnswers.Append( quiz.CorrectAnswer )
            );
}