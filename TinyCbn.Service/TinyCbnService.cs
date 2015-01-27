using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThingM.Blink1;
using TinyCbn.General;

namespace TinyCbn.Service
{
    public partial class TinyCbnService : ServiceBase
    {
        CancellationTokenSource cts = new CancellationTokenSource();

        public TinyCbnService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var task = RunAsync(cts);
        }

        protected override void OnStop()
        {
            cts.Cancel();
        }

        private async Task RunAsync(CancellationTokenSource cts)
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
