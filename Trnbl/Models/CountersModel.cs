
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Diagnostics;
using System.Timers;

namespace Trnbl.Models;

public partial class CountersModel : ObservableObject
{

    public CountersModel()
    {
        TimeCounter = new TimeSpan(0, 0, 0);
        RoundCounter = 1;
        RoundTurnCounter = 1;
        TurnCounter = 1;
        PlayerCounter = 0;
    }
    [ObservableProperty]
    public TimeSpan timeCounter;
    [ObservableProperty]
    public int roundCounter;
    [ObservableProperty]
    public int roundTurnCounter;
    [ObservableProperty]
    public int turnCounter;
    [ObservableProperty]
    public int playerCounter;



    public void ResetCounter()
    {
        TimeCounter = TimeSpan.Zero;
    }

    public void UpdateTimeCounter(object sender, ElapsedEventArgs e)
    {
        TimeCounter = TimeCounter.Add(TimeSpan.FromSeconds(1));
    }

    public override string ToString()
    {
        return string.Format("{0}:{1:00}",
        (int)TimeCounter.TotalMinutes,
        TimeCounter.Seconds);
    }
}

