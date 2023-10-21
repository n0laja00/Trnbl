using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Timers;
using Trnbl.Models;


namespace Trnbl.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    private System.Timers.Timer timer;
    public MainPageViewModel()
    {
        Players = new ObservableCollection<PlayerModel>();
        PlayersAmount = 0;
        Counters = new CountersModel();

        timer = new System.Timers.Timer(1000);
        timer.Elapsed += OnTimerElapsed;
    }

    [ObservableProperty]
    ObservableCollection<PlayerModel> players;

    [ObservableProperty]
    CountersModel counters;

    [ObservableProperty]
    string name;

    [ObservableProperty]
    public string timerButton = "0:00";

    [ObservableProperty]
    int ordNum;

    public int PlayersAmount { get; set; }

    [RelayCommand]
    public void AddPlayer()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            return;
        }

        PlayerModel player = new PlayerModel(Name, OrdNum);

        Players.Add(player);
        PlayersAmount++;
    }

    [RelayCommand]
    public void DeletePlayer(PlayerModel player)
    {
        if (Players.Contains(player))
        {
            Players.Remove(player);
            PlayersAmount--;
        }
    }

    [RelayCommand]
    public void NextTurn()
    {
        if (PlayersAmount < 1)
        {
            return;
        }

        Counters.TurnCounter++;
        Counters.RoundTurnCounter++;

        if (Counters.RoundTurnCounter > PlayersAmount)
        {
            Counters.RoundCounter++;
            Counters.RoundTurnCounter = 1;
        }
    }

    [RelayCommand]
    public void PreviousTurn()
    {
        if (PlayersAmount < 1)
        {
            return;
        }

        if (Counters.RoundCounter > 1 && Counters.TurnCounter > 1 && Counters.RoundTurnCounter == 1)
        {
            Counters.RoundCounter--;
            Counters.TurnCounter--;
            Counters.RoundTurnCounter = PlayersAmount;
        }
        else
        {
            Counters.TurnCounter--;
            Counters.RoundTurnCounter--;
        }
    }

    [RelayCommand]
    private void StartTimer()
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

    [RelayCommand]
    private void ClearTimer()
    {
        timer.Stop();
        Counters.ResetCounter();


        TimerButton = Counters.ToString();

    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        Counters.TimeCounter = Counters.TimeCounter.Add(TimeSpan.FromSeconds(1));

        TimerButton = Counters.ToString();
    }

}

