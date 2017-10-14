using System;

namespace FindAndBook.Models
{
    public class Question
    {
        public Question()
        {
            
        }

        public Question(string senderName, string senderEmail, string subject, string questionMessage)
        {
            SenderName = senderName;
            SenderEmail = senderEmail;
            Subject = subject;
            QuestionMessage = questionMessage;
        }

        public Guid? Id { get; set; }

        public string SenderName { get; set; }

        public string SenderEmail { get; set; }

        public string Subject { get; set; }

        public string QuestionMessage { get; set; }
    }
}
