using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using WebAPI.DTO;

namespace WebAPI.Controllers;

public class QuizController : Controller
{
    private readonly IQuizUserService _service;


    public QuizController(IQuizUserService userService)
    {
        _service = userService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [Route( "/{id}" )]
    public ActionResult<QuizDto> FindById(int id)
    {
        var quiz = QuizDto.of( _service.FindQuizById( id ) );
        if (quiz == null)
        {
            return NotFound();
        }

        return Ok( quiz );
    }

    [HttpGet( "/Quizes" )]
    public IEnumerable<QuizDto> FindAll()
    {
        List<QuizDto> quiz = new List<QuizDto>();
        foreach (var item in _service.FindAllQuizzess())
        {
            quiz.Add( QuizDto.of( item ) );
        }
        return quiz;
    }

    [HttpPost]
    [Route( "{quizId}/items/{itemId}" )]
    public void SaveAnswer([FromBody] QuizItemAnswerDto dto, [FromRoute] int quizId,
        [FromRoute] int itemId)
    {
        _service.SaveUserAnswerForQuiz( quizId, dto.UserId, itemId, dto.Answer );
    }

    [HttpGet( "/{userId}/quizesAnswers/{quizId}" )]
    public QuizUserAnswerDto GetCorrectAnswers([FromRoute] int userId, [FromRoute] int quizId)
    {
        //List<QuizItemUserAnswer> quizes = _service.GetUserAnswersForQuiz( userId, quizId );
        //return _service.CountCorrectAnswersForQuizFilledByUser( quizId, userId );

        var answers = _service.GetUserAnswersForQuiz( quizId, userId )
            .Where( x => x.QuizId == quizId );

        return new QuizUserAnswerDto()
        {
            QuizId = quizId,
            UserId = 1,
            QuizAnswers = answers.Select( x => new QuizItemUserAnswersDto()
            {
                Question = x.QuizItem.Question,
                Answer = x.Answer,
                IsCorrect = x.IsCorrect(),
                QuizItemId = x.QuizItem.Id
            } ).ToList()
        };
    }
    [HttpGet("/api/v2/quizzes")]
    public IEnumerable<Quiz> GetAllQuizzes()
    {
        return _service.FindAllQuizzess();
    }
} 