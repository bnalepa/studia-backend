﻿using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MongoDB.Entities
{
    public class QuizUserServiceMongoDB : IQuizUserService
    {
        private readonly IMongoCollection<QuizMongoEntity> _quizzes;
        private readonly MongoClient _client;

        public QuizUserServiceMongoDB(IOptions<MongoDBSettings> settings)
        {
            _client = new MongoClient(settings.Value.ConnectionUri);
            IMongoDatabase database = _client.GetDatabase(settings.Value.DatabaseName);
            _quizzes = database.GetCollection<QuizMongoEntity>(settings.Value.QuizCollection);
        }

        public IEnumerable<Quiz> FindAllQuizzess()
        {
            var quizMongoEntities = _quizzes.Find(Builders<QuizMongoEntity>.Filter.Empty).ToList();
            return _quizzes
                .Find(Builders<QuizMongoEntity>.Filter.Empty)
                .Project(
                    q =>
                        new Quiz(
                            q.QuizId,
                            q.Items.Select(i => new QuizItem(
                                    i.ItemId,
                                    i.Question,
                                    i.IncorrectAnswers,
                                    i.CorrectAnswer
                                )
                            ).ToList(),
                            q.Title
                        )
                ).ToEnumerable();
        }

        public Quiz? FindQuizById(int id)
        {
            return _quizzes
                .Find(Builders<QuizMongoEntity>.Filter.Eq(q => q.QuizId, id))
                .Project(q =>
                    new Quiz(
                        q.QuizId,
                        q.Items.Select(i => new QuizItem(
                                i.ItemId,
                                i.Question,
                                i.IncorrectAnswers,
                                i.CorrectAnswer
                            )
                        ).ToList(),
                        q.Title
                    )
                ).FirstOrDefault();
        }

        public QuizItemUserAnswer SaveUserAnswerForQuiz(int quizId, int quizItemId, int userId, string answer)
        {
            throw new NotImplementedException();
        }

        public List<QuizItemUserAnswer> GetUserAnswersForQuiz(int quizId, int userId)
        {
            throw new NotImplementedException();
        }

        public Quiz CreateAndGetQuizRandom(int count)
        {
            throw new NotImplementedException();
        }


        void IQuizUserService.SaveUserAnswerForQuiz(int quizId, int userId, int quizItemId, string answer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Quiz> FindAllQuizzesss()
        {
            throw new NotImplementedException();
        }
    }
}
