using CoxAutomotiveCodingExercise.API.Dtos;
using CoxAutomotiveCodingExercise.API.Models;

namespace CoxAutomotiveCodingExercise.API.Services
{
    public interface IDataSetService
    {
        public AnswerResponse SendAnswer(Answer answer);
        public Answer CreateAnswer();
    }
}
