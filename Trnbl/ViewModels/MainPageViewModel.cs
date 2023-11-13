using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Timers;
using Trnbl.Models;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Extensions;

namespace Trnbl.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    private System.Timers.Timer timer;
    public MainPageViewModel()
    {
        Players = new ObservableCollection<PlayerModel>();
        Counters = new CountersModel();
        DiceRolls = new DiceRollsModel();

        timer = new System.Timers.Timer(1000);
        timer.Elapsed += OnTimerElapsed;
    }

    [ObservableProperty]
    ObservableCollection<PlayerModel> players;

    private PlayerModel _draggedPlayer;

    [ObservableProperty]
    CountersModel counters;

    [ObservableProperty]
    DiceRollsModel diceRolls;

    [ObservableProperty]
    string orderingSelection;

    [ObservableProperty]
    string name;

    [ObservableProperty]
    Color timerButtonBackgroundColor = Color.FromArgb("#512BD4");

    [ObservableProperty]

    int ordNum;

    [ObservableProperty]
    int dieQuantity;

    [ObservableProperty]
    int dieSides;

    [ObservableProperty]
    List<string> diceRollsHistory;

    [ObservableProperty]
    string diceRollsLatest;

    [ObservableProperty]
    public string timerButton = "0:00";

    [ObservableProperty]
    public bool timerRunning = false;


    [RelayCommand]
    public void OrderPlayersAscending()
    {
        Players = Players.OrderBy(x => x.OrderNumber).ToObservableCollection();
        ResetActive();
    }

    [RelayCommand]
    public void OrderPlayersDescending()
    {
        Players = Players.OrderByDescending(x => x.OrderNumber).ToObservableCollection();
        ResetActive();
    }

    public void ResetActive()
    {
        if (Players.Count == 0)
        {
            return;
        }

        int currentIndex = Players.IndexOf(Players.Where(p => p.Active == true).FirstOrDefault());

        Players[currentIndex].Active = false;
        Players[0].Active = true;
        Counters.RoundTurnCounter = 1

            ;
    }

    [RelayCommand]
    public async Task RollDiceAsync()
    {
        if (DieSides < 1 || DieQuantity < 1)
        {
            return;
        }
        else
        {
            Random rnd = new Random();
            DiceModel dice = new DiceModel();

            for (int i = 0; i < DieQuantity; i++)
            {
                dice.Rolls.Add(rnd.Next(1, DieSides));
            };

            DiceRolls.UpdateDieRolls(dice);
            await PopUp(DiceRolls.Latest.Rolls.Sum());
            UpdateDiceUI();
        }
    }

    [RelayCommand]
    public void ClearDice()
    {
        DiceRolls.ClearDieRolls();
    }

    [RelayCommand]
    public void AddPlayer()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            return;
        }

        PlayerModel player = new PlayerModel(Name, OrdNum);

        Players.Add(player);
        Counters.PlayerCounter++;

        switch (OrderingSelection)
        {
            case "1":
                OrderPlayersAscending();
                break;
            case "2":
                OrderPlayersDescending();
                break;
        }

        if (Counters.PlayerCounter == 1)
        {
            Players[0].Active = true;
        }
    }

    [RelayCommand]
    public void DeletePlayer(PlayerModel player)
    {
        if (player.Active)
        {
            int id = Players.IndexOf(player);

            if (id == (Players.Count - 1))
            {
                Players[0].Active = true;
            }
            else if (Players.Count > 1)
            {
                Players[id + 1].Active = true;
            }

        }


        if (Players.Contains(player))
        {
            Players.Remove(player);
            Counters.PlayerCounter--;
        }

        if (Counters.PlayerCounter < Counters.RoundTurnCounter && Counters.PlayerCounter != 0)
        {
            Counters.RoundTurnCounter = Counters.PlayerCounter;
        }
    }

    [RelayCommand]
    public void PlayerDragStarted(PlayerModel player)
    {
        _draggedPlayer = player;
    }

    [RelayCommand]
    public void PlayerDropped(PlayerModel player)
    {
        _draggedPlayer = player;
    }

    [RelayCommand]
    public void NextTurn()
    {
        Console.Write(DiceRolls);


        if (Counters.PlayerCounter < 1)
        {
            return;
        }

        if (Counters.PlayerCounter != 1)
        {
            Players[Counters.RoundTurnCounter - 1].Active = false;

        }

        Counters.TurnCounter++;
        Counters.RoundTurnCounter++;



        if (Counters.RoundTurnCounter > Counters.PlayerCounter)
        {
            Players[Players.Count - 1].Active = false;
            Players[0].Active = true;
            Counters.RoundCounter++;
            Counters.RoundTurnCounter = 1;
            return;
        }

        Players[Counters.RoundTurnCounter - 1].Active = true;

    }

    [RelayCommand]
    public void PreviousTurn()
    {
        if (Counters.PlayerCounter < 1)
        {
            return;
        }

        if (Counters.RoundCounter == 1 && Counters.TurnCounter == 1 && Counters.RoundTurnCounter == 1)
        {
            return;
        }
        else if (Counters.RoundCounter > 1 && Counters.TurnCounter > 1 && Counters.RoundTurnCounter == 1)
        {
            Players[0].Active = false;
            Counters.RoundCounter--;
            Counters.TurnCounter--;
            Counters.RoundTurnCounter = Counters.PlayerCounter;
            Players[Players.Count - 1].Active = true;
        }

        else
        {
            Players[Counters.RoundTurnCounter - 1].Active = false;
            Counters.TurnCounter--;
            Counters.RoundTurnCounter--;
            Players[Counters.RoundTurnCounter - 1].Active = true;

        }
    }

    [RelayCommand]
    private void StartTimer()
    {
        if (timer.Enabled)
        {
            timer.Stop();
            TimerRunning = false;
        }
        else
        {
            timer.Start();
            TimerRunning = true;
        }
    }

    [RelayCommand]
    private void ClearTimer()
    {
        if (TimerRunning)
        {
            return;
        };

        timer.Stop();
        Counters.ResetCounter();

        TimerButton = Counters.ToString();

    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        Counters.TimeCounter = Counters.TimeCounter.Add(TimeSpan.FromSeconds(1));

        TimerButton = Counters.ToString();
    }

    private void UpdateDiceUI()
    {
        List<string> listDieHistory = new List<string>();
        string listDieLatest = string.Empty;
        string combinedString = string.Empty;

        if (DiceRolls.History.Count != 0)
        {
            int DiceRollsInHistoryCount = DiceRolls.History.Count;
            int minId;

            if (DiceRollsInHistoryCount > 5)
            {
                minId = DiceRollsInHistoryCount - 5;
            }
            else
            {
                minId = 0;
            }

            for (int i = DiceRollsInHistoryCount - 1; i >= minId; i--)
            {
                combinedString = string.Join(" + ", DiceRolls.History[i].Rolls);
                combinedString += " = ";
                combinedString += DiceRolls.History[i].Rolls.Sum().ToString();
                listDieHistory.Add(combinedString);
            }

            var temp = listDieHistory;
            temp.Reverse();
            DiceRollsHistory = temp;
            temp = null;
            listDieHistory = null;
        }
        else
        {
            listDieHistory.Add("Game on!");
            DiceRollsHistory = listDieHistory;
        }

        combinedString = string.Join(" + ", DiceRolls.Latest.Rolls);
        combinedString += "\n= ";
        combinedString += DiceRolls.Latest.Rolls.Sum().ToString();
        listDieLatest += combinedString;


        if (!string.IsNullOrWhiteSpace(combinedString))
        {
            DiceRollsLatest = listDieLatest;
        }

    }

    [RelayCommand]
    async Task OpenNotes(PlayerModel player)
    {
        await Shell.Current.GoToAsync($"{nameof(NotesPage)}?PlayerName={player.Name}",
            new Dictionary<string, object> {
                { nameof(NotesPage), player }
            });
    }

    public async Task PopUp(int sumOfDice)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        ToastDuration duration = ToastDuration.Long;
        double fontSize = 32;
        var toast = Toast.Make(sumOfDice.ToString(), duration, fontSize);

        await toast.Show(cancellationTokenSource.Token);
    }



}

