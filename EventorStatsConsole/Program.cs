namespace EventorStatsConsole
{
    using System;
    using System.Reflection;
    using Autofac;
    using StatsEngine;

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Please, provide your ApiKey and press [Enter]: ");
            var apiKey = Console.ReadLine();
            var baseUrl = "https://eventor.orientering.no/api/";

            var container = CreateContainer(apiKey, baseUrl);
            
            string excelFileName = @"c:\temp\eventor.xls";
            var fromDate = new DateTime(2011, 01, 01);

            var service = container.Resolve<AggregateService>();
            service.CreateExcelFile(excelFileName, fromDate);

            Console.WriteLine("The file is generated at " + excelFileName + ". Press [Enter] to exit.");
            Console.ReadLine();
        }

        private static IContainer CreateContainer(string apiKey, string baseUrl)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(AggregateService)));
            containerBuilder.RegisterInstance(new EventorWebService(apiKey, baseUrl));
            var container = containerBuilder.Build();
            return container;
        }
    }
}
