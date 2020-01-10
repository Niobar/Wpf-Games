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
using System.Windows.Threading;

namespace Games
{
    /// <summary>
    /// Interaction logic for SnakeWindow.xaml
    /// </summary>
    public partial class SnakeWindow : Window
    {
        Snake snakeGame;
        Rectangle[,] boardChildren = new Rectangle[10, 20];
        DispatcherTimer timer = new DispatcherTimer();

        public SnakeWindow()
        {
            InitializeComponent();
            Show();
            SnakeGrid.Children.Capacity = 200;
            snakeGame = new Snake();
            snakeGame.SnakeStart();
            SnakeShow(snakeGame, false);
            NewPiceShow(snakeGame);
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!snakeGame.gameOver)
            {
                snakeGame.snakeMove();
                SnakeShow(snakeGame, snakeGame.swallowed);
                if (snakeGame.swallowed)
                {
                    snakeGame.PiceToSwallow();
                    NewPiceShow(snakeGame);
                }
                Score.Text = $"{snakeGame.snakeGameScore}";// just checking will remove snakeGameScore later
            }
            if (snakeGame.gameOver)
            {
                var scoreName = new ScoreWindow();
                scoreName.ShowDialog();
                string name = scoreName.Result;
                timer.Stop();
            }
        }

        void NewPiceShow(Snake game)
        {
            Rectangle rec = new Rectangle();
            rec.Fill = Brushes.LightGray;
            Grid.SetColumn(rec, game.piceToSwallow.x);
            Grid.SetRow(rec, game.piceToSwallow.y);
            SnakeGrid.Children.Add(rec);
            boardChildren[game.piceToSwallow.x, game.piceToSwallow.y] = rec;
        }

        void SnakeShow(Snake game, bool swallowed)
        {
            if (swallowed)
            {
                Rectangle recSwallowed = new Rectangle();
                recSwallowed = boardChildren[game.piceToSwallow.x, game.piceToSwallow.y];
                SnakeGrid.Children.Remove(recSwallowed);
                boardChildren[game.piceToSwallow.x, game.piceToSwallow.y] = null;
            }
            Rectangle recHead = new Rectangle();
            recHead.Fill = Brushes.ForestGreen;
            Grid.SetColumn(recHead, game.snake.Last().x);
            Grid.SetRow(recHead, game.snake.Last().y);
            SnakeGrid.Children.Add(recHead);
            boardChildren[game.snake.Last().x, game.snake.Last().y] = recHead;
            if (!swallowed)
            {
                Rectangle recTail = new Rectangle();
                recTail = boardChildren[game.shadingPice.x, game.shadingPice.y];
                SnakeGrid.Children.Remove(recTail);
                boardChildren[game.shadingPice.x, game.shadingPice.y] = null;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                new MainWindow().Show();
                this.Close();
            }
            if (e.Key == Key.Right) snakeGame.snakeRotation(3);
            if (e.Key == Key.Left) snakeGame.snakeRotation(1);
            if (e.Key == Key.Down) snakeGame.snakeRotation(0);
            if (e.Key == Key.Up) snakeGame.snakeRotation(2);

        }
    }
}

