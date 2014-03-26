using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using Ninject;
using System.Reflection;

namespace TestingNinjectDependencyInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            IMailSender mailSender = kernel.Get<IMailSender>();

            FormHandler formHandler = new FormHandler(mailSender);
            //FormHandler formHandler = new FormHandler(new MockMailSender());
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
        private readonly ILogging logging;
        public MailSender(ILogging logging)
        {
            this.logging = logging;
        }
        public void Send(string adress, string subject)
        {
            logging.Debug("Sending mail");
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
        private readonly IMailSender sender;
        public FormHandler(IMailSender sender)
        {
            this.sender = sender;
        }
        public void Handle(string adress)
        {
            this.sender.Send(adress, "Ejemplo non-Ninject");
        }
    }
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IMailSender>().To<MailSender>();
            Bind<ILogging>().To<MockLogging>();
        }
    }
    public interface ILogging
    {
        void Debug(string message);
    }
    public class MockLogging : ILogging
    {
        public void Debug(string message)
        {
            Console.WriteLine("-- DEBUG --: {0}", message);
        }
    }
}
