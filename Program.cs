using System.Text;

namespace DecoratorPattern {

    public class Notifier {
        public List<string> administrators;

        public Notifier(List<string> administrators)
        {
            this.administrators = administrators;
        }



        public virtual void Send(string message) {
            StringBuilder sb = new StringBuilder();

            foreach (var admin in administrators)
            {
                sb.Append(admin);
                sb.Append(", ");
            }

            Console.WriteLine(message + sb.ToString());
        }
    }

    public abstract class NotifierDecorator : Notifier
    {
        protected Notifier notifier;

        public NotifierDecorator(Notifier notifier) : base(notifier.administrators) { 
            this.notifier = notifier ?? throw new ArgumentNullException(nameof(notifier));
        }

        public override void Send(string message) { 
            this.notifier.Send(message);
        }

    }

    class SMSNotifier : NotifierDecorator
    {
        public SMSNotifier(Notifier notifier) : base(notifier) { }


        public override void Send(string message) {
            SendSMS(message);
            base.Send(message);
        }

        public void SendSMS(string message)
        {
            Console.WriteLine($"Отправка СМС-сообщения '{message}'");
        }
    }

    class EmailNotifier : NotifierDecorator
    {
        public EmailNotifier(Notifier notifier) : base(notifier) { }

        public override void Send(string message)
        {
            SendEmail(message);
            base.Send(message);
        }

        public void SendEmail(string message)
        {
            Console.WriteLine($"Отправка Email-сообщения '{message}'", message); ;
        }
    }

    class FacebookNotifier : NotifierDecorator {
        public FacebookNotifier(Notifier notifier) : base(notifier) { }

        public override void Send(string message)
        {
            SendToFacebook(message);
            base.Send(message);
        }

        public void SendToFacebook(string message)
        {
            Console.WriteLine($"Отправка сообщения '{message}' на почты", message);
        }

    }



    class Program
    {
        public static void Main(string[] args) {
            var admins = new List<string>()
            {
                "admin@example.com", "sysadmin@example.com"
            };

            Notifier notifier = new Notifier(admins);

            Notifier FacebookNotifier = new FacebookNotifier(notifier);

            FacebookNotifier.Send("ExampleMessage");
        }


    }


}