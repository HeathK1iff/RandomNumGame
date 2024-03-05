using RandomNumGame.Core.Abstractions;

namespace RandomNumGame.Core.Entities
{
    public abstract class User : INotifable, IAskable
    {
        public delegate string OnAskQuestion(string question);
        public delegate void OnNotification(string promt);

        public Guid Id { get; init; }
        public string UserName { get; init; } = null!;

        public string Ask(string question)
        {
            return OnAskQuestionEvent?.Invoke(question) ?? string.Empty;
        }

        public void Notify(string promt)
        {
            OnNotificationEvent?.Invoke(promt);
        }

        public event OnAskQuestion OnAskQuestionEvent = null!;

        public event OnNotification OnNotificationEvent = null!;
    }

}
