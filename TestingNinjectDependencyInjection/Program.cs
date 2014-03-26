using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingNinjectDependencyInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            FormHandler formHandler = new FormHandler();
            formHandler.Handle("test@test.es");

            Console.ReadLine();
        }
    }
    public interface IMailSender
    {
        void Send(string adress, string subject);
    }
    public class MailSender : IMailSender
    {
        public void Send(string adress, string subject)
        {
            Console.WriteLine("Send mail to [{0}] with subject [{1}]", adress, subject);
        }
    }
    public class MockMailSender : IMailSender
    {
        public void Send(string adress, string subject)
        {
            Console.WriteLine("Mocking send mail to [{0}] with subject [{1}]", adress, subject);
        }
    }
    public class FormHandler
    {
        public void Handle(string adress)
        {
            IMailSender sender = new MockMailSender();
            sender.Send(adress, "Ejemplo non-Ninject");
        }
    }

}
