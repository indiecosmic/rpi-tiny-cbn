using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThingM.Blink1;

namespace TinyCbn
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            Task t = RunAsync(cts);

            Console.ReadLine();
            cts.Cancel();
            t.Wait();

            using (Blink1 blink = new Blink1())
            {
                blink.Open();
                blink.SetColor(0, 0, 0);
                blink.Close();
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        static async Task RunAsync(CancellationTokenSource cts)
        {
            try
            {
                CbnWrapper wrapper = new CbnWrapper();
                while (!cts.IsCancellationRequested)
                {
                    var colours = await wrapper.GetColours();
                    Console.WriteLine("New colors");
                    using (Blink1 blink = new Blink1())
                    {
                        blink.Open();
                        colours.Reverse();

                        blink.Blink(2, 200, 200, colours.First());

                        foreach (var color in colours)
                        {
                            if (cts.IsCancellationRequested)
                                break;
                            Console.WriteLine(string.Format("Fading to ({0},{1},{2})", color.Red, color.Green, color.Blue));
                            blink.FadeToColor(1000, color, true);
                            Thread.Sleep(5000);
                        }
                    }
                }
            }
            catch { }
        }
    }
}
