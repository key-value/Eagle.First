using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using Eagle.Server.Interface.Wcf;
using Eagle.Server.Services;

namespace Eagle.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            HostCalculatorServiceViaCode();
        }



        /// <summary>
        /// Hosting a service using managed code without any configuraiton information.
        /// Please note that the related configuration data should be removed before calling the method.
        /// </summary>
        static void HostCalculatorServiceViaCode()
        {
            //Uri httpBaseAddress = new Uri("http://localhost:8888/MessageService");
            //Uri tcpBaseAddress = new Uri("net.tcp://localhost:9999/MessageService");

            //using (ServiceHost calculatorSerivceHost = new ServiceHost(typeof(MessageServices), httpBaseAddress, tcpBaseAddress))
            //{
            //    BasicHttpBinding httpBinding = new BasicHttpBinding();
            //    NetTcpBinding tcpBinding = new NetTcpBinding();
            //    tcpBinding.Security.Mode = SecurityMode.None;
            //    calculatorSerivceHost.AddServiceEndpoint(typeof(IMessageService), httpBinding, string.Empty);
            //    calculatorSerivceHost.AddServiceEndpoint(typeof(IMessageService), tcpBinding, string.Empty);

            //    ServiceMetadataBehavior behavior = calculatorSerivceHost.Description.Behaviors.Find<ServiceMetadataBehavior>();
            //    {
            //        if (behavior == null)
            //        {
            //            behavior = new ServiceMetadataBehavior();
            //            behavior.HttpGetEnabled = true;
            //            calculatorSerivceHost.Description.Behaviors.Add(behavior);
            //        }
            //        else
            //        {
            //            behavior.HttpGetEnabled = true;
            //        }
            //    }

            //    calculatorSerivceHost.Opened += delegate
            //    {
            //        Console.WriteLine("Calculator Service has begun to listen  ");
            //    };


            //    calculatorSerivceHost.Open();

            Console.Read();
            //}
        }

        //static void HostCalculatorSerivceViaConfiguration()
        //{
        //    //using (ServiceHost calculatorSerivceHost = new ServiceHost(typeof(MessageServices)))
        //    //{
        //    //    calculatorSerivceHost.Opened += delegate
        //    //    {
        //    //        Console.WriteLine("Calculator Service has begun to listen  ");
        //    //    };

        //    //    calculatorSerivceHost.Open();

        //    //    Console.Read();
        //    //}
        //}
    }
}
