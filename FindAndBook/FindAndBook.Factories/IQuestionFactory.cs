using FindAndBook.Models;

namespace FindAndBook.Factories
{
    public interface IQuestionFactory
    {
        Question CreateQuestion(string senderName, string senderEmail, string subject, string questionMessage);
    }
}
