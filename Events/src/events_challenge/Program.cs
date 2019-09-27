using System;
using System.Collections.Generic;
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
            new EmailMass(message).Send();
        }

        public class EmailMass
        {
            private readonly List<EmailMessage> _pendings;

            public EmailMass(List<EmailMessage> pendings)
            {
                _pendings = pendings;
            }

            public void Send()
            {
                var emailServer = new EmailServer();
                foreach (var message in _pendings)
                    emailServer.Send(message);
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

        public class EmailMessage
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
