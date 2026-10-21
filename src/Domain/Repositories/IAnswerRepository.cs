using Domain.Entities.SPEC;
namespace Domain.Repositories;

public interface IAnswerRepository
{
    List<Answer> GetAnswersByStudentId(Guid studentid);
    List<Answer> GetAllAnswers();
    Task CreateAnswer(Answer answer);
    Task CreateBulkAnswers(List<Answer> answers);
}

