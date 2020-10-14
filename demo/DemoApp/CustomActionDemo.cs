using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RulesEngine.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using DemoApp.Actions;
using Microsoft.Extensions.Logging.Abstractions;
using RulesEngine.Actions;
using static RulesEngine.Extensions.ListofRuleResultTreeExtension;

namespace DemoApp
{
    public class CustomActionDemo
    {
        public void Run()
        {
            Console.WriteLine($"Running {nameof(CustomActionDemo)}....");
            var basicInfo = "{\"name\": \"hello\",\"email\": \"abcy@xyz.com\",\"creditHistory\": \"good\",\"country\": \"canada\",\"loyalityFactor\": 3,\"totalPurchasesToDate\": 10000}";
            var orderInfo = "{\"totalOrders\": 5,\"recurringItems\": 2}";
            var telemetryInfo = "{\"noOfVisitsPerMonth\": 10,\"percentageOfBuyingToVisit\": 15}";

            var converter = new ExpandoObjectConverter();

            dynamic personalData = JsonConvert.DeserializeObject<ExpandoObject>(basicInfo, converter);
            dynamic orderData = JsonConvert.DeserializeObject<ExpandoObject>(orderInfo, converter);
            dynamic telemetryData = JsonConvert.DeserializeObject<ExpandoObject>(telemetryInfo, converter);

            var inputs = new dynamic[]
                {
                    personalData,
                    orderData,
                    telemetryData
                };

            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "CustomActionDemo.json", SearchOption.AllDirectories);
            if (files == null || files.Length == 0)
                throw new Exception("Rules not found.");

            var fileData = File.ReadAllText(files[0]);
            var workflowRules = JsonConvert.DeserializeObject<List<WorkflowRules>>(fileData);

            var bre = new RulesEngine.RulesEngine(workflowRules.ToArray(), null, new ReSettings
            {
                CustomActions = new Dictionary<string, Func<ActionBase>>
                {
                    {nameof(LowerCreditHistoryAction), () => new LowerCreditHistoryAction(new NullLogger<LowerCreditHistoryAction>())}
                }
            });

            string discountOffered = "No discount offered.";

            List<RuleResultTree> resultList = bre.ExecuteRule("Discount", inputs);

            resultList.OnSuccess((eventName) =>
            {
                discountOffered = $"Discount offered is {eventName} % over MRP.";
            });

            resultList.OnFail(() =>
            {
                discountOffered = "The user is not eligible for any discount.";
            });

            Console.WriteLine(discountOffered);
        }
    }
}
