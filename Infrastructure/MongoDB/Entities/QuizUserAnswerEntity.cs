using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MongoDB.Entities
{
    public class QuizUserAnswerEntity
    {
        [BsonElement("quizId")]
        public int QuizId { get; set; }

        [BsonElement("userId")]
        public int UserId { get; set; }

        [BsonElement("quizAnswers")]
        public List<QuizItemMongoEntity> QuizAnswers { get; set; }
    }
}
