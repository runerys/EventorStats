namespace EventorStatsConsole
{
    using System;
    using System.Reflection;

    using Autofac;

    using StatsEngine;

    public class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Please, provide your apikey and press [Enter]: ");
            var apiKey = Console.ReadLine();            

            var service = GetAggregateService(apiKey);

            string excelFileName = @"c:\temp\eventor.xls";
            var fromDate = new DateTime(2011, 01, 01);

            service.CreateExcelFile(excelFileName, fromDate);

            Console.WriteLine("The file is generated at " + excelFileName + ". Press [Enter] to exit.");
            Console.ReadLine();
        }

        private static AggregateService GetAggregateService(string apiKey)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(AggregateService)));

            var eventorService = new EventorWebService();
            eventorService.ApiKey = apiKey;
            containerBuilder.RegisterInstance(eventorService);

            var container = containerBuilder.Build();

            var service = container.Resolve<AggregateService>();
            return service;
        }
    }
}
