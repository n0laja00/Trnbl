using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trnbl.Models;
public partial class DiceModel : ObservableObject
{
    [ObservableProperty]
    public List<int> rolls;

    public DiceModel(List<int> rolls)
    {
        Rolls = rolls;
    }

    public DiceModel()
    {
        Rolls = new List<int>();
    }
}

