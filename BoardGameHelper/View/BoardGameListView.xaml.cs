using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BoardGameHelper.View
{
    /// <summary>
    /// Interaction logic for BoardGameListView.xaml
    /// </summary>
    public partial class BoardGameListView : UserControl
    {
        private BoardGameHelper.ViewModel.BoardGameViewModel boardGameObject { get; set; }
        public event EventHandler ListSelectionChanged;
        public BoardGameListView()
        {
            InitializeComponent();
        }

        public void LoadBoardGames()
        {
            boardGameObject = new BoardGameHelper.ViewModel.BoardGameViewModel();
            boardGameObject.LoadBoardGames();
            BoardGameList.ItemsSource = boardGameObject.DisplayedBoardGames;
            BoardGameList.IsSynchronizedWithCurrentItem = true;
        }

        public void FilterBoardGames(string filter, bool showAll)
        {
            if (boardGameObject == null)
                return;
            boardGameObject.FilterBoardGames(filter, showAll);
        }

        public void AddBoardGame(string name)
        {
            if (boardGameObject == null)
                return;
            boardGameObject.AddBoardGame(name);
        }

        public bool IsSomethingSelected()
        {
            return BoardGameList.SelectedItem != null;
        }

        private void BoardGameList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListSelectionChanged != null)
                ListSelectionChanged(this, new EventArgs());
        }

        public void PlayAndRateSelectedBoardGame(int score)
        {
            if ((BoardGameList.SelectedItem == null) || (boardGameObject == null)) // should be superfluous now, but always check
                return;

            //var item = (Model.BoardGame)BoardGameList.SelectedItem;
            //item.Score = score;

            var name = ((Model.BoardGame)BoardGameList.SelectedItem).Name;
            boardGameObject.PlayAndRateBoardGame(name, score);
        }
    }
}
