using System;
using System.Linq;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;

namespace FindAndBook.Services
{
    public class QuestionService: IQuestionService
    {
        private readonly IRepository<Question> repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IQuestionFactory factory;

        public QuestionService(IRepository<Question> repository, IUnitOfWork unitOfWork, IQuestionFactory factory)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.factory = factory;
        }

        public Question AddQuestion(string senderName, string senderEmail, string subject, string questionMessage)
        {
            var question = this.factory.CreateQuestion(senderName, senderEmail, subject, questionMessage);
            this.repository.Add(question);
            this.unitOfWork.Commit();

            return question;
        }

        public IQueryable<Question> GetAll()
        {
            return this.repository.All;
        }
    }
}
