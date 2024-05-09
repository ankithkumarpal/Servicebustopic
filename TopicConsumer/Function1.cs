using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace TopicConsumer
{
    public class Function1
    {

        const string topicName = "tv-smtp-email-topic";

        [FunctionName("Subscription1")]
        public static async Task RunFunction1(
        [ServiceBusTrigger(topicName, "sub1", Connection = "ServiceBusConnection")] string mySbMsg,
        ILogger log)
        {
            EmailMessage emailMessage = JsonConvert.DeserializeObject<EmailMessage>(mySbMsg);

            var tasks = new List<Task>();

            foreach(var recepeint in emailMessage.To)
            {
                tasks.Add(SendEmailBatchAsync(emailMessage.Subject,  emailMessage.Body,  recepeint));
            }

            Task.WhenAll(tasks);
            Console.WriteLine(); 
        }

        [FunctionName("Subscription2")]
        public static async Task RunFunction2(
            [ServiceBusTrigger(topicName, "sub2", Connection = "ServiceBusConnection")] string mySbMsg,
            ILogger log)
        {
            EmailMessage emailMessage = JsonConvert.DeserializeObject<EmailMessage>(mySbMsg);

            var tasks = new List<Task>();

            foreach (var recepeint in emailMessage.To)
            {
                tasks.Add(SendEmailBatchAsync(emailMessage.Subject, emailMessage.Body, recepeint));
            }

            Task.WhenAll(tasks);
            Console.WriteLine();
        }

        [FunctionName("Subscription3")]
        public static async Task RunFunction3(
            [ServiceBusTrigger(topicName, "sub3", Connection = "ServiceBusConnection")] string mySbMsg,
            ILogger log)
        {
            EmailMessage emailMessage = JsonConvert.DeserializeObject<EmailMessage>(mySbMsg);

            var tasks = new List<Task>();

            foreach (var recepeint in emailMessage.To)
            {
                tasks.Add(SendEmailBatchAsync(emailMessage.Subject, emailMessage.Body, recepeint));
            }

            Task.WhenAll(tasks);
            Console.WriteLine();
        }

        public static async Task SendEmailBatchAsync(string subject, string body, string recepeint)
        {
            using (var client = new SmtpClient("smtp.gmail.com", 485))
            {
                client.Port = 587;
                client.Credentials = new NetworkCredential("ankithpal721@gmail.com", "zvmh gyse wdbc shrw");
                client.EnableSsl = true;

                using (var mailMessage = new MailMessage("ankithpal721@gmail.com", recepeint))
                {
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    await client.SendMailAsync(mailMessage);
                }
            }

            Console.WriteLine("Message send successfull");
        }
    }



    public class EmailMessage
    {
        public List<string> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
