using CommunityToolkit.Maui.Extensions;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using System.Timers;
using Trnbl.Models;
using Trnbl.ViewModels;

namespace Trnbl
{
    public partial class MainPage : ContentPage
    {
        CountersModel Counters { get; set; }
        private MainPageViewModel VM => (MainPageViewModel)BindingContext;

        private System.Timers.Timer timer;
        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            Counters = new CountersModel();


            timer = new System.Timers.Timer(1000);
            //timer.Elapsed += OnTimerElapsed;
        }

        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            var btn = (Button)sender;
            var col = Color.FromArgb("#512BD4");

            await Task.WhenAll<bool>
                (
                    btn.FadeTo(0.5),
                    btn.ScaleTo(0.9, 250)
                );

            if ((float)btn.BackgroundColor.Blue == (float)col.Blue)
            {
                btn.BackgroundColor = Color.FromArgb("#FF0000");
            }
            else
            {
                btn.BackgroundColor = col;
            }

            await Task.WhenAll<bool>
                (
                    btn.ScaleTo(1, 250),
                    btn.FadeTo(1, 250)
                );


        }

        private async void RollDiceButton_Pressed(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            int dieQuantity;
            int DieSides;
            bool successDieQuantity = int.TryParse(DieQuantityEntry.Text, out dieQuantity);
            bool successDieSides = int.TryParse(DieSidesEntry.Text, out DieSides);

            if (!successDieQuantity || !successDieSides)
            {
                return;
            }

            if (btn == null)
            {
                return;
            }

            if (dieQuantity != 0 && DieSides != 0)
            {
                btn.ScaleTo(0.9, 100);
                await btn.RotateTo(-5, 50);
                await btn.RotateTo(0, 50);
                await btn.RotateTo(5, 50);
                await btn.RotateTo(0, 50);
                await btn.ScaleTo(1, 100);
            }
            else
            {
                btn.FadeTo(0.9, 25);
                btn.BackgroundColorTo(Colors.Red, 10, 150);
                await btn.TranslateTo(2, 0, 25);
                await btn.TranslateTo(0, 0, 25);
                await btn.TranslateTo(-2, 0, 25);
                await btn.TranslateTo(0, 0, 25);
                await btn.TranslateTo(2, 0, 25);
                await btn.TranslateTo(0, 0, 25);
                await btn.TranslateTo(-2, 0, 25);
                await btn.TranslateTo(0, 0, 25);

                await Task.WhenAll<bool>(
                    btn.FadeTo(1, 25),
                    btn.BackgroundColorTo(Color.FromArgb("#512BD4"), 10, 250)
                );
                return;
            }
            this.LatestDieRollFrame.IsVisible = true;
        }

        private void OrderButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (!e.Value)
            {
                return;
            }

            var radioBtn = (RadioButton)sender;

            if (radioBtn == null)
            {
                return;
            }


            switch (radioBtn.Value)
            {
                case "1":
                    VM.OrderPlayersAscending();
                    break;
                case "2":
                    VM.OrderPlayersDescending();
                    break;
            }



        }

        private void SwipeGestureRecognizer_LatestDieRollFrame(object sender, SwipedEventArgs e)
        {

            this.LatestDieRollFrame.IsVisible = false;
        }
    }

}
