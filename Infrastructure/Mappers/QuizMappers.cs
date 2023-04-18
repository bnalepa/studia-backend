using ApplicationCore.Data;
using ApplicationCore.Models;
using Infrastructure.EF.Entities;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappers;

public class QuizMappers
{
    public static QuizItem FromEntityToQuizItem(QuizItemEntity entity)
    {
        return new QuizItem(
            entity.Id,
            entity.Question,
            entity.IncorrectAnswers.Select( e => e.Answer ).ToList(),
            entity.CorrectAnswer
            );
    }

    public static Quiz FromEntityToQuiz(QuizEntity entity)
    {
        return new Quiz(
            entity.Id,
            ( List<QuizItem> )entity.Items,
            entity.Title
            );
    }

    //public static QuizItemUserAnswer FromEntityToUserAnswer(QuizItemUserAnswerEntity entity)
    //{
    //    return new QuizItemUserAnswer(quizItem: _context,
    //        userId: entity.UserId,
    //        quizId:entity.QuizId,answer:entity.UserAnswer);
    //}
}