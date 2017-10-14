using System.Linq;
using FindAndBook.Models;

namespace FindAndBook.Services.Contracts
{
    public interface IQuestionService
    {
        Question AddQuestion(string senderName, string senderEmail, string subject, string questionMessage);

        IQueryable<Question> GetAll();
    }
}
