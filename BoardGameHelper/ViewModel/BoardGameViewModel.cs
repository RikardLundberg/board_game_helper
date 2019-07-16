using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BoardGameHelper.Model;

namespace BoardGameHelper.ViewModel
{
    public class BoardGameViewModel
    {
        public List<BoardGame> BoardGames { get; set; }
        public ObservableCollection<BoardGame> DisplayedBoardGames { get; set; }

        private string CurrentFilter { get; set; }

        public void LoadBoardGames()
        {
            BoardGames = new List<BoardGame>();
            CurrentFilter = "";
            DisplayedBoardGames = new ObservableCollection<BoardGame>();
            BoardGames.Add(new BoardGame { Name = "Battlestar Galactica", Score = 5.0, TimesPlayed = 1 });
            BoardGames.Add(new BoardGame { Name = "Caverna", Score = 3.0, TimesPlayed = 1 });
            foreach (var boardGame in BoardGames)
                DisplayedBoardGames.Add(boardGame);
        }

        public void FilterBoardGames(string filter, bool showAll)
        {
            CurrentFilter = showAll ? "" : filter;
            if (showAll)
            {
                DisplayedBoardGames.Clear();
                foreach (var boardGame in BoardGames)
                    DisplayedBoardGames.Add(boardGame);
                return;
            }
            ApplyFilter(filter);
        }

        private void ApplyFilter(string filter)
        {
            DisplayedBoardGames.Clear();
            foreach (var boardGame in BoardGames.Select(x => x).Where(x => x.Name.IndexOf(filter, StringComparison.InvariantCultureIgnoreCase) >= 0))
                DisplayedBoardGames.Add(boardGame);
        }

        public void AddBoardGame(string name)
        {
            var boardGame = new BoardGame { Name = name, Score = 0, TimesPlayed = 0 };
            BoardGames.Add(boardGame);
            DisplayedBoardGames.Add(boardGame);
        }

        public void PlayAndRateBoardGame(string name, int score)
        {
            foreach (var game in BoardGames)
                if (game.Name.Equals(name))
                {
                    game.Score = score;
                    break;
                }
            var displayedNames = DisplayedBoardGames.Select(x => x.Name).ToArray();
            ApplyFilter(CurrentFilter);
        }
    }
}
