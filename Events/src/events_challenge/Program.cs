using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace events_challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            var message = new List<EmailMessage>(new[]
            {
                    new EmailMessage("xpto@xpto.com.br", "xpto2@xpto.com.br"),
                    new EmailMessage("xpto@xpto.com.br", "xpto3@xpto.com.br"),
                    new EmailMessage("xpto@xpto.com.br", "xpto4@xpto.com.br"),
                    new EmailMessage("xpto@xpto.com.br", "xpto5@xpto.com.br")
            });

            // man, isso aqui demora...
            // preciso saber quando cada mensagem terminar
            var emailMass = new EmailMass(message);
            emailMass.SendCompleted += EmailMass_SendCompleted;
            emailMass.Send();
        }

        private static void EmailMass_SendCompleted(object sender, EmailMessage e)
        {
            Console.WriteLine($"Mail to {e.To} sent!");
        }

        public class EmailMass
        {
            private readonly List<EmailMessage> _pendings;
            public event EventHandler<EmailMessage> SendCompleted;

            public EmailMass(List<EmailMessage> pendings)
            {
                _pendings = pendings;
            }

            public void Send()
            {
                var emailServer = new EmailServer();

                _pendings.AsParallel().ForAll(m =>
                {
                    emailServer.Send(m);
                    SendCompleted?.Invoke(this, m);
                });
            }
        }

        public class EmailServer
        {
            public void Send(EmailMessage message)
            {
                Console.WriteLine($"Sending an e-mail from {message.From} to {message.To}");
                Thread.Sleep(TimeSpan.FromSeconds(2));
            }
        }

        public class EmailMessage : EventArgs
        {
            public EmailMessage(string from, string to)
            {
                From = from;
                To = to;
            }
            public string From { get; }
            public string To { get; }
        }
    }
}
