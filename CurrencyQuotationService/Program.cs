// See https://aka.ms/new-console-template for more information
using CurrencyQuotationService;


var timer = new System.Timers.Timer(TimeSpan.FromMinutes(2.0).TotalMilliseconds);
timer.Elapsed += Timer_Elapsed;

while (true)
{
    timer.Start();
}

static async void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
{
    System.Timers.Timer timer = (System.Timers.Timer)sender;
    timer.Stop();
    var job = new Job();
    await job.Start();
    timer.Start();
}

