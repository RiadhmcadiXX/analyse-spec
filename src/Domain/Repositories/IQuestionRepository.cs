using Domain.Entities.SPEC;

namespace Domain.Repositories;

public interface IQuestionRepository
{
    List<Question> GetQuestionsByIds(List<Guid> ids);
    Question GetQuestionById(Guid id);
    List<Question> GetAllQuestions();
    Task CreateQuestion(Question question);
    Task CreateBulkQuestions(List<Question> questions);
    Task UpdateQuestions(List<Question> questions);
}

