namespace DecoratorPattern {

    public class Notifier {
        public List<string> administrators;

        public Notifier(List<string> administrators)
        {
            this.administrators = administrators;
        }



        public virtual void Send(string message)
        {
            string sb = "";

            foreach (var admin in administrators)
            {
                sb += admin;
                sb += ", ";
            }

            Console.WriteLine(sb.Remove(sb.Length - 2) + ".\n" );
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
            Console.WriteLine($"Отправка СМС-сообщения '{message}' на почту(ы): ");
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
            Console.WriteLine($"Отправка Email-сообщения '{message}' на почту(ы):  "); ;
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
            Console.WriteLine($"Отправка сообщения '{message}' на Facebook на почту(ы): ");
        }

    }



    class Program
    {
        public static void Main(string[] args) {
            var admins = new List<string>()
            {
                "admin@example.com", "sysadmin@example.com"
            };
            var admins2 = new List<string>()
            {
                "admin@example.com", "sysadmin@example.com", "dbadmin@example.com"
            };

            Notifier notifier = new Notifier(admins);
            Notifier notifier2 = new Notifier(admins2);

            Notifier facebookNotifier = new FacebookNotifier(notifier);
            Notifier emailNotifier = new EmailNotifier(notifier);
            Notifier smsNotifier = new SMSNotifier(notifier2);

            facebookNotifier.Send("SampleMessage");
            emailNotifier.Send("ImportantMessage");
            smsNotifier.Send("Server down!");


        }


    }


}