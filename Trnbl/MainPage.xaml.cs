using System.Diagnostics.Metrics;
using System.Timers;
using Trnbl.Models;
using Trnbl.ViewModels;

namespace Trnbl
{
    public partial class MainPage : ContentPage
    {
        CountersModel Counters { get; set; }

        private System.Timers.Timer timer;

        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            Counters = new CountersModel();


            timer = new System.Timers.Timer(1000);
            //timer.Elapsed += OnTimerElapsed;
        }
        /*
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Counters.TimeCounter = Counters.TimeCounter.Add(TimeSpan.FromSeconds(1));

            Dispatcher.DispatchAsync(() =>
            {
                TimerButton.Text = Counters.ToString();
            });
        }

        private void OnTimerClicked(object sender, EventArgs e)
        {
            if (timer.Enabled)
            {
                timer.Stop();
            }
            else
            {
                timer.Start();
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            timer.Stop();
            Counters.ResetCounter();

            Dispatcher.DispatchAsync(() =>
            {
                TimerButton.Text = Counters.ToString();
            });
        }
        */
    }

}
