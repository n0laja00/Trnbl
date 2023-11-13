using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Trnbl.Models;

public partial class DiceRollsModel : ObservableObject
{
    public DiceRollsModel()
    {
        Latest = new DiceModel();
        History = new ObservableCollection<DiceModel>();
    }

    [ObservableProperty]
    public DiceModel latest;

    [ObservableProperty]
    public ObservableCollection<DiceModel> history;



    //public DiceRollsModel()
    //{
    //    
    //    DiceModel Latest = new DiceModel();
    //    List<DiceModel> History = new List<DiceModel>();
    //
    //}

    public void UpdateDieRolls(DiceModel dice)
    {
        if (Latest.Rolls.Count != 0)
        {
            History.Add(Latest);
        }
        Latest = dice;
    }

    public void ClearDieRolls()
    {
        Latest = null;
        History = null;
    }
}

