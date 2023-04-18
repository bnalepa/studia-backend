using ApplicationCore.Models;

namespace WebAPI.DTO;

public class QuizDto
{
    public QuizDto(int id, string title, List<QuizItemDto> items)
    {
        Id = id;
        Title = title;
        Items = items;
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public List<QuizItemDto> Items { get; set; }

    public static QuizDto of(Quiz quiz)
    {
        List<QuizItemDto> quizItemDtos = new List<QuizItemDto>();
        foreach (QuizItem dto in quiz.Items)
        {
            quizItemDtos.Add( QuizItemDto.of( dto ) );
        }

        QuizDto quizDto = new QuizDto
         (
         id: quiz.Id,
         title: quiz.Title,
         items: quizItemDtos
         );

        return quizDto;
    }
}