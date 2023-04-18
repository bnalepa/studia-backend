using ApplicationCore.Data;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Infrastructure.EF.Entities;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public class QuizUserServiceEF : IQuizUserService
{
    private readonly QuizDbContext _context;

    public QuizUserServiceEF(QuizDbContext context)
    {
        _context = context;
    }

    public Quiz CreateAndGetQuizRandom(int count)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Quiz> FindAllQuizzess()
    {
        return _context
            .Quizzes
            .AsNoTracking()
            .Include( q => q.Items )
            .ThenInclude( i => i.IncorrectAnswers )
            .Select( QuizMappers.FromEntityToQuiz )
            .ToList();
    }

    public Quiz? FindQuizById(int id)
    {
        var entity = _context
             .Quizzes
             .AsNoTracking()
             .Include( q => q.Items )
             .ThenInclude( i => i.IncorrectAnswers )
             .FirstOrDefault( e => e.Id == id );
        return entity is null ? null : QuizMappers.FromEntityToQuiz( entity );
    }

    public List<QuizItemUserAnswer> GetUserAnswersForQuiz(int quizId, int userId)
    {
        throw new NotImplementedException();
    }

    public QuizItemUserAnswer SaveUserAnswerForQuiz(int quizId, int quizItemId, int userId, string answer)
    {
        QuizItemUserAnswerEntity entity = new QuizItemUserAnswerEntity()
        {
            UserId = userId,
            QuizItemId = quizItemId,
            QuizId = quizId,
            UserAnswer = answer
        };
        try
        {
            var saved = _context.UserAnswers.Add( entity ).Entity;
            _context.SaveChanges();
            return new QuizItemUserAnswer( quizItem: QuizMappers.FromEntityToQuizItem( saved.QuizItem ),
                saved.UserId, saved.QuizId, saved.UserAnswer );
        }
        catch (DbUpdateException e)
        {
            if (e.InnerException.Message.StartsWith( "The INSERT" ))
            {
                throw new Exception( "Quiz, quiz item or user not found. Can't save!" );
            }
            if (e.InnerException.Message.StartsWith( "Violation of" ))
            {
                throw new Exception( e.Message );
            }
            throw new Exception( e.Message );
        }
    }

    void IQuizUserService.SaveUserAnswerForQuiz(int quizId, int userId, int quizItemId, string answer)
    {
        throw new NotImplementedException();
    }
}