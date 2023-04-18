using ApplicationCore.Models;
using System.Security.Policy;

namespace WebAPI.DTO;

public class QuizItemUserAnswersDto
{
    public int QuizItemId { get; set; }
    public bool IsCorrect { get; set; }

    public string Answer { get; set; }
    public string Question { get; set; }
}