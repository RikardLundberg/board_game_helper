using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BoardGameHelper.Model
{
    public class BoardGame : INotifyPropertyChanged
    {
        private string name;
        private int timesPlayed;
        private double totalScore; //could be int, but would be confusing with return type double and would cause errors if changed in UI
        private double averageScore;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        public double Score
        {
            get { return averageScore; }
            set
            {
                timesPlayed++;
                totalScore += value;
                averageScore = Math.Round(totalScore / timesPlayed, 2);
                RaisePropertyChanged("AverageScore");
                RaisePropertyChanged("TimesPlayed");
            }
        }

        public int TimesPlayed { get { return timesPlayed; } set { timesPlayed = value; } }

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}