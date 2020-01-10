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
    /// Logika interakcji dla klasy TetrisWindow.xaml
    /// </summary>
    public partial class TetrisWindow : Window
    {
        bool isEndGame = false;
        object semafore = new object();
        bool boardWipe = false;
        List<Vector> gamePice;
        List<Vector> gameNextPice;
        TetraminoPice boardPice;
        Tetris tetrisGame;
        int gameSpeed = 500;
        int removedLineCounter = 0;
        double gameScore = 0;
        int level = 0;
        Vector[] previousGamePice = new Vector[4];
        Rectangle[,] boardChildren = new Rectangle[10, 20];
        DispatcherTimer timer = new DispatcherTimer();
        public TetrisWindow()
        {
            InitializeComponent();
            Show();
            tetrisGame = new Tetris();
            tetrisGame.TetrisStart();
            PiceShow(tetrisGame);
            if (removedLineCounter > 19)
            {
                if (gameSpeed == 0) gameSpeed = gameSpeed + 25;
                removedLineCounter = removedLineCounter - 20;
                gameSpeed = gameSpeed - 25;
                level = (500 - gameSpeed) / 25;
            }
            timer.Interval = TimeSpan.FromMilliseconds(gameSpeed);
            timer.Tick += Timer_Tick;
            timer.Start();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tetrisGame.pice = tetrisGame.Moving(tetrisGame.pice, 0, 1);
            Score.Text = gameScore.ToString();
            gameLevel.Text = level.ToString();
            PiceShow(tetrisGame);
        }

        void PiceShow(Tetris tetris)
        {
            List<int> lastPiceAddedLine = new List<int>();
            boardPice = tetris.pice;
            if (gamePice != null)
            {
                for (int i = 0; i < 4; i++) TetrisGrid.Children.RemoveAt(TetrisGrid.Children.Count - 1);
            }
            if (boardWipe == true)
            {
                TetrisGrid.Children.Clear();
                boardWipe = false;
                for (int ii = 0; ii < 10; ii++)
                {
                    for (int jj = 0; jj < 20; jj++)
                    {
                        if (tetris.gameBoard[ii, jj] == 2) tetris.gameBoard[ii, jj] = 1;
                    }
                }
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (tetris.gameBoard[i, j] == 1)
                    {
                        tetris.gameBoard[i, j] = 2;
                        Rectangle rec = new Rectangle();
                        rec.Fill = Brushes.Gray;
                        Grid.SetColumn(rec, i);
                        Grid.SetRow(rec, j);
                        TetrisGrid.Children.Add(rec);
                        boardChildren[i, j] = rec;
                        if (lastPiceAddedLine.Contains(j) == false) lastPiceAddedLine.Add(j);
                    }
                }
            }
            gamePice = null;
            gamePice = tetris.GamePice(tetris.pice);
            for (int i = 0; i < gamePice.Count; i++)
            {
                Rectangle rec = new Rectangle();
                rec.Fill = boardPice.brush;
                Grid.SetColumn(rec, (int)gamePice[i].X);
                Grid.SetRow(rec, (int)gamePice[i].Y);
                TetrisGrid.Children.Add(rec);
            }
            if (lastPiceAddedLine.Count > 0)
            {
                List<int> removedLines = new List<int>();
                int lastPiceAddedLineCount = lastPiceAddedLine.Count;
                for (int i = 0; i < lastPiceAddedLineCount; i++)
                {
                    int j = lastPiceAddedLine[0];
                    lastPiceAddedLine.RemoveAt(0);
                    if (Tetris.LineChecker(j, tetris.gameBoard) == true)
                    {
                        removedLines.Add(j);
                        for (int ii = 0; ii < 10; ii++)
                        {
                            TetrisGrid.Children.Remove(boardChildren[ii, j]);
                        }
                    }
                }
                if (removedLines.Count > 0)
                {
                    removedLineCounter = removedLineCounter + removedLines.Count;
                    switch (removedLines.Count)
                    {
                        case 1:
                            gameScore = gameScore + 100;
                            break;
                        case 2:
                            gameScore = gameScore + 300;
                            break;
                        case 3:
                            gameScore = gameScore + 600;
                            break;
                        case 4:
                            gameScore = gameScore + 1000;
                            break;
                    }
                    Tetris.RemovedLines(tetrisGame.gameBoard, removedLines);
                    boardWipe = true;
                }
                //TJ
                lock (semafore)
                {
                    if (!isEndGame /* endTJ*/&& tetris.CheckForObstacle(tetris.pice, "EndGame") == true)
                    {
                        
                        string endGame = "Your Score: " + gameScore;
                        //TJ
                        isEndGame = true;
                        var scoreName = new ScoreWindow();
                        scoreName.ShowDialog();
                        string name = scoreName.Result;
                        //endTJ
                        HighScore.TetrisHighScore((int)gameScore, name);
                        timer.Stop();
                        new HighscoreWindow();
                        this.Close();
                    }
                }
            }
            gameNextPice = tetris.GamePice(tetris.nextPice);
            PreviewGrid.Children.Clear();
            for (int i = 0; i < 4; i++)
            {
                Rectangle rec = new Rectangle();
                rec.Fill = tetris.nextPice.brush;
                Grid.SetColumn(rec, (int)gameNextPice[i].X - 3);
                Grid.SetRow(rec, (int)gameNextPice[i].Y);
                PreviewGrid.Children.Add(rec);
            }
        }


        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                new MainWindow().Show();
                this.Close();
            }
            if (e.Key == Key.Up)
            {
                tetrisGame.pice = tetrisGame.Rotation(tetrisGame.pice);
                PiceShow(tetrisGame);
            }
            if (e.Key == Key.Right)
            {
                tetrisGame.pice = tetrisGame.Moving(tetrisGame.pice, 1, 0);
                PiceShow(tetrisGame);
            }
            if (e.Key == Key.Left)
            {
                tetrisGame.pice = tetrisGame.Moving(tetrisGame.pice, -1, 0);
                PiceShow(tetrisGame);
            }
            if (e.Key == Key.Down)
            {
                tetrisGame.pice = tetrisGame.Moving(tetrisGame.pice, 0, 1);
                PiceShow(tetrisGame);
            }
        }


    }
}
