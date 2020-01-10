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
using System.Windows.Shapes;
using System.IO;

namespace Games
{
    /// <summary>
    /// Interaction logic for HighscoreWindow.xaml
    /// </summary>
    public partial class HighscoreWindow : Window
    {
        public HighscoreWindow()
        {
            InitializeComponent();
            Show();
            TetrisScore(TetrisHighscore);
        }

        
        public static void TetrisScore(TextBlock tetrisScore)
        {
            HighScore.TetrisHighScore();
            StreamReader tetrisHighScore = new StreamReader("TetrisHighScore.txt");
            string line;
            tetrisScore.Text = "";
            while ((line = tetrisHighScore.ReadLine()) != null)
            {
                tetrisScore.Text += line + "\n";
            }
        }
    }
}
