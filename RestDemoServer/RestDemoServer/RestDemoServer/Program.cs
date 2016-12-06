using RESTService.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace RestDemoServer
{
    class Program
    {
        static void Main(string[] args)
        {
            RestDemoServices DemoServices = new RestDemoServices();
            WebServiceHost _serviceHost = new WebServiceHost(DemoServices,

                                                             new Uri("http://localhost:33133/DEMOService"));
            _serviceHost.Open();
            Console.ReadKey();
            _serviceHost.Close();
        }
    }
}
