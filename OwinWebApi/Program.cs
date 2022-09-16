using Microsoft.Owin.Hosting;
using System;
using System.Text.RegularExpressions;

namespace OwinAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            StartOptions startOptions = GetStartOptions(args);

            if(startOptions.Urls.Count == 0)
            {
                Console.WriteLine("No valid urls has been passed to the console, execution cannot proceed.");
                Console.ReadLine();
                Environment.Exit(0);
            }

            using (WebApp.Start<Startup>(startOptions))
            {
                Console.WriteLine("Server started at:");

                foreach (var url in startOptions.Urls)
                    Console.WriteLine(url);

                Console.ReadLine();
            }
        }

        private static StartOptions GetStartOptions(string[] args)
        {
            var startOptions = new StartOptions();

            if (args == null || args.Length == 0)
                return startOptions;

            foreach (var url in args[0].Split(';'))
            {
                if (UrlIsValid(url))
                    startOptions.Urls.Add(url.Trim());
                else
                    Console.WriteLine($"{url} is not a valid url");
            }

            return startOptions;
        }

        private static bool UrlIsValid(string url)
        {
            var pattern = @"https?:\/\/(?:w{1,3}\.)?[^\s.]+(?:\.[a-z]+)*(?::\d+)?((?:\/\w+)|(?:-\w+))*\/?(?![^<]*(?:<\/\w+>|\/?>))";
            return Regex.IsMatch(url, pattern);
        }
    }
}