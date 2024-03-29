﻿using System;
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

namespace Games
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Switcher.MainWindow = this;
            //Switcher.Switch(new MainMenu());

        }

        //public void Navigate(UserControl nextPage)
        //{
        //    this.Content = nextPage;
        //}

        //public void Navigate(UserControl nextPage, object state)
        //{
        //    this.Content = nextPage;
        //    ISwitchable s = nextPage as ISwitchable;
        //    if (s != null)
        //        s.UtilizeState(state);
        //    else
        //        throw new ArgumentException("NextPage is not ISwitchable! "
        //          + nextPage.Name.ToString());
        //}

        private void Tetris_Click(object sender, RoutedEventArgs e)
        {
            new TetrisWindow();
            Close();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void Highscore_Click(object sender, RoutedEventArgs e)
        {
            new HighscoreWindow();
            Close();
        }

        private void Snake_Click(object sender, RoutedEventArgs e)
        {
            new SnakeWindow();
            Close();
        }
    }
}
