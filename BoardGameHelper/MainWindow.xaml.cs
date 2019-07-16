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

namespace BoardGameHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string filterFieldDefault = "Filter..";
        private const string newGameFieldDefault = "Add a new game..";

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void BoardGameViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            /*var boardGameObject = new BoardGameHelper.ViewModel.BoardGameViewModel();
            boardGameObject.LoadBoardGames();
            BoardGameViewControl.DataContext = boardGameObject*/
            BoardGameViewControl.LoadBoardGames();
            BoardGameViewControl.ListSelectionChanged += new EventHandler(BoardGameViewControl_ListSelectionChanged);
            FilterField.Text = filterFieldDefault;
            NewGameField.Text = newGameFieldDefault;
            UpdateControls();
        }

        private void FilterBox_Focus(object sender, RoutedEventArgs e)
        {
            if (FilterField.Text.Equals(filterFieldDefault))
                FilterField.Text = "";
        }

        private void FilterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FilterField.Text.Equals(filterFieldDefault) || FilterField.Text == "") {
                BoardGameViewControl.FilterBoardGames("", true);
                return;
            }
            BoardGameViewControl.FilterBoardGames(FilterField.Text, false);
        }

        private void FilterBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (FilterField.Text == "")
                FilterField.Text = filterFieldDefault;
        }

        private void NewGameField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NewGameField.Text.Equals(newGameFieldDefault))
                NewGameField.Text = "";
        }

        private void NewGameField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NewGameField.Text == "")
                NewGameField.Text = newGameFieldDefault;
        }

        private void NewGameField_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter && NewGameField.Text != "" && !NewGameField.Text.Equals(newGameFieldDefault))
            {
                BoardGameViewControl.AddBoardGame(NewGameField.Text);
                NewGameField.Text = "";
            }
        }

        public void BoardGameViewControl_ListSelectionChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void UpdateControls()
        {
            RateButton.IsEnabled = BoardGameViewControl.IsSomethingSelected() && (GetRateScore() > 0);
        }

        private void RateButton_Click(object sender, RoutedEventArgs e)
        {
            var score = GetRateScore();
            BoardGameViewControl.PlayAndRateSelectedBoardGame(score);
        }

        private int GetRateScore()
        {
            if (RateOneRadio.IsChecked == true) return 1;
            if (RateTwoRadio.IsChecked == true) return 2;
            if (RateThreeRadio.IsChecked == true) return 3;
            if (RateFourRadio.IsChecked == true) return 4;
            if (RateFiveRadio.IsChecked == true) return 5;
            return 0;
        }

        private void RateRadio_Checked(object sender, RoutedEventArgs e)
        {
            UpdateControls();
        }
    }
}
