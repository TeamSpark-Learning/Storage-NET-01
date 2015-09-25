using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using msswit2013_lab1.core.Queue;
using msswit2013_lab1.core.Table.Service;

namespace msswit2013_lab1.console
{
    class Program
    {
        static void Main(string[] args)
        {
            QueueService queueService;
            GoodService goodService;

            queueService = new QueueService();
            queueService.Initialize();

            goodService = new GoodService();
            goodService.Initialize();

            Console.WriteLine("Initialized");

            while (true)
            {
                var message = queueService.GetMessage();
                if (message == null)
                {
                    Console.WriteLine("sleep...");
                    Thread.Sleep(new TimeSpan(0, 0, 10));
                }
                else
                {
                    var good = goodService.GetById(message.AsString);
                    good.IsApproved = true;
                    goodService.InsertOrReplace(good);

                    queueService.DeleteMessage(message.Id, message.PopReceipt);

                    Console.WriteLine("processed {0}", good.Title);
                }

            }
        }
    }
}
