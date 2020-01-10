using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace Games
{
    /// <summary>
    /// Methods for Tetrs Game
    /// </summary>
    public class Tetris
    {
        public int[,] gameBoard = new int[10, 20];
        List<Vector> piceCoordinates = new List<Vector>();
        Random randomizer = new Random();
        public TetraminoPice nextPice;
        public TetraminoPice pice;

        public void TetrisStart()
        {
            PiceGenerator();
            pice = nextPice;
            PiceGenerator();
        }

        public void NextPice()
        {
            pice = nextPice;
            PiceGenerator();
        }

        public void PiceGenerator()
        {
            nextPice = TetrominoPices.tetraminos[randomizer.Next(0, 7)];
            nextPice.axis.X = 5;
            if (nextPice.first.Y >= 0) nextPice.axis.Y = 0;
            else nextPice.axis.Y = 0 - nextPice.first.Y;
        }

        public List<Vector> GamePice(TetraminoPice pice)
        {
            piceCoordinates.Clear();
            piceCoordinates.Add(new Vector(pice.axis.X, pice.axis.Y));
            piceCoordinates.Add(new Vector((pice.axis.X + pice.first.X), (pice.axis.Y + pice.first.Y)));
            piceCoordinates.Add(new Vector((pice.axis.X + pice.second.X), (pice.axis.Y + pice.second.Y)));
            piceCoordinates.Add(new Vector((pice.axis.X + pice.third.X), (pice.axis.Y + pice.third.Y)));
            return piceCoordinates;
        }

        public TetraminoPice Rotation(TetraminoPice pice)
        {
            TetraminoPice rotationPice = new TetraminoPice();
            if (CheckForObstacle(pice, "Rotation") != true)
            {
                rotationPice.axis.X = pice.axis.X;
                rotationPice.axis.Y = pice.axis.Y;
                rotationPice.first.X = -pice.first.Y;
                rotationPice.first.Y = pice.first.X;
                rotationPice.second.X = -pice.second.Y;
                rotationPice.second.Y = pice.second.X;
                rotationPice.third.X = -pice.third.Y;
                rotationPice.third.Y = pice.third.X;
                rotationPice.brush = pice.brush;
                rotationPice = CheckIfOnBoard(rotationPice);
                if (CheckForObstacle(rotationPice) == true)
                {
                    return pice;
                }

            }
            else rotationPice = changePiceToBoard(pice);

            return rotationPice;
        }

        public TetraminoPice Moving(TetraminoPice pice, int X, int Y)
        {
            TetraminoPice movingPice = new TetraminoPice();
            movingPice.axis.X = pice.axis.X + X;
            movingPice.axis.Y = pice.axis.Y + Y;
            movingPice.first.X = pice.first.X;
            movingPice.first.Y = pice.first.Y;
            movingPice.second.X = pice.second.X;
            movingPice.second.Y = pice.second.Y;
            movingPice.third.X = pice.third.X;
            movingPice.third.Y = pice.third.Y;
            movingPice.brush = pice.brush;
            movingPice = CheckIfOnBoard(movingPice);
            bool checkerForObstacle = CheckForObstacle(movingPice);
            if ( checkerForObstacle == true && Y == 1 && X == 0) movingPice = changePiceToBoard(movingPice, 0, -Y);
            if (checkerForObstacle == true && Y == 0) return pice;
            return movingPice;
        }

        static public TetraminoPice CheckIfOnBoard(TetraminoPice onBoardPice)
        {
            double verticalCorection = 0;
            double horizontalCorection = 0;
            Vector[] checkOnBoardArray = new Vector[4];
            checkOnBoardArray[0] = (new Vector(onBoardPice.axis.X, onBoardPice.axis.Y));
            checkOnBoardArray[1] = (new Vector(onBoardPice.first.X, onBoardPice.first.Y));
            checkOnBoardArray[2] = (new Vector(onBoardPice.second.X, onBoardPice.second.Y));
            checkOnBoardArray[3] = (new Vector(onBoardPice.third.X, onBoardPice.third.Y));
            for (int i = 1; i < 4; i++)
            {
                double tempVertical;
                if (checkOnBoardArray[i].X + checkOnBoardArray[0].X > 9)
                {
                    tempVertical = checkOnBoardArray[i].X + checkOnBoardArray[0].X - 9;
                    if (tempVertical > verticalCorection) verticalCorection = tempVertical;
                }
                if (checkOnBoardArray[i].X + checkOnBoardArray[0].X < 0)
                {
                    tempVertical = checkOnBoardArray[i].X + checkOnBoardArray[0].X;
                    if (tempVertical < verticalCorection) verticalCorection = tempVertical;
                }
                double tempHorizontal;
                if (checkOnBoardArray[i].Y + checkOnBoardArray[0].Y < 0)
                {
                    tempHorizontal = checkOnBoardArray[i].Y + checkOnBoardArray[0].Y;
                    if (tempHorizontal < horizontalCorection) horizontalCorection = tempHorizontal;
                }
            }
            onBoardPice.axis.X = onBoardPice.axis.X - verticalCorection;
            onBoardPice.axis.Y = onBoardPice.axis.Y - horizontalCorection;
            return onBoardPice;
        }

        public bool CheckForObstacle(TetraminoPice obstaclePice, String switcher = "default")
        {
            int[] Y = new int[] { 0, 0, 0, 0 };
            int[] X = new int[] { 0, 0, 0, 0 };
            double[,] obstaclePiceArray = new double[4, 2];
            obstaclePiceArray[0, 0] = obstaclePice.axis.X;
            obstaclePiceArray[0, 1] = obstaclePice.axis.Y;
            obstaclePiceArray[1, 0] = obstaclePice.first.X;
            obstaclePiceArray[1, 1] = obstaclePice.first.Y;
            obstaclePiceArray[2, 0] = obstaclePice.second.X;
            obstaclePiceArray[2, 1] = obstaclePice.second.Y;
            obstaclePiceArray[3, 0] = obstaclePice.third.X;
            obstaclePiceArray[3, 1] = obstaclePice.third.Y;

            Vector[] checkForObstacleArray = new Vector[4];
            switch (switcher)
            {
                case "Rotation":
                    for (int i = 1; i < 4; i++)
                    {
                        string rotationChecker = "Default";
                        if ((X[i] > 0) && (Y[i] >= 0)) rotationChecker = "Down";
                        if ((X[i] >= 0) && (Y[i] < 0)) rotationChecker = "Left";
                        if ((X[i] < 0) && (Y[i] <= 0)) rotationChecker = "Up";
                        if ((X[i] <= 0) && (Y[i] > 0)) rotationChecker = "Right";
                        switch (rotationChecker)
                        {
                            case "Down":
                                for (int ii = 1; ii <= X[i]; ii++)
                                {
                                    if ((Y[i] + Y[0] + ii <= 19) && (gameBoard[X[i] + X[0], Y[i] + Y[0] + ii] > 0)) return true;
                                }
                                break;
                            case "Up":
                                for (int ii = -1; ii >= X[i]; ii--)
                                {
                                    if ((Y[i] + Y[0] + ii > 0) && (gameBoard[X[i] + X[0], Y[i] + Y[0] + ii] > 0)) return true;
                                }
                                break;
                            case "Right":
                                for (int ii = 1; ii <= Y[i]; ii++)
                                {
                                    if ((X[i] + X[0] + ii < 9) && (gameBoard[X[i] + X[0] + ii, Y[i] + Y[0]] > 0)) return true;
                                }
                                break;
                            case "Left":
                                for (int ii = -1; ii >= Y[i]; ii--)
                                {
                                    if ((X[i] + X[0] + ii > 0) && (gameBoard[X[i] + X[0] + ii, Y[i] + Y[0]] > 0)) return true;
                                }
                                break;
                            case "Default":
                                break;
                        }
                    }
                    break;
                case "EndGame":
                    break;
                case "Down":
                    Y[0] = Y[0] + 1;
                    break;
                case "Up":
                    Y[0] = Y[0] - 1;
                    break;
                case "Left":
                    X[0] = X[0] - 1;
                    break;
                case "Right":
                    X[0] = X[0] + 1;
                    break;
                default:
                    break;
            }
            checkForObstacleArray[0] = (new Vector(obstaclePiceArray[0, 0] + X[0], obstaclePiceArray[0, 1] + Y[0]));
            for (int i = 1; i < 4; i++)
            {
                checkForObstacleArray[i] = (new Vector((obstaclePiceArray[i, 0] + obstaclePiceArray[0, 0] + X[i]), (obstaclePiceArray[i, 1] + obstaclePiceArray[0, 1] + Y[i])));
            }

            for (int i = 0; i < checkForObstacleArray.Count(); i++)
            {
                if (checkForObstacleArray[i].Y >= 0)
                {
                    if (checkForObstacleArray[i].Y > 19) return true;
                    if (gameBoard[(int)checkForObstacleArray[i].X, ((int)checkForObstacleArray[i].Y)] > 0) return true; //== 1) || (gameBoard[(int)checkForObstacleArray[i].X, ((int)checkForObstacleArray[i].Y)] == 2)) return true;
                }
            }
            return false;
        }

        public TetraminoPice changePiceToBoard(TetraminoPice changeToBoardPice, int X = 0, int Y = 0)
        {
            Vector[] changeToBoardArray = new Vector[4];
            changeToBoardArray[0] = (new Vector(changeToBoardPice.axis.X, changeToBoardPice.axis.Y));
            changeToBoardArray[1] = (new Vector((changeToBoardPice.first.X + changeToBoardPice.axis.X), (changeToBoardPice.first.Y + changeToBoardPice.axis.Y)));
            changeToBoardArray[2] = (new Vector((changeToBoardPice.second.X + changeToBoardPice.axis.X), (changeToBoardPice.second.Y + changeToBoardPice.axis.Y)));
            changeToBoardArray[3] = (new Vector((changeToBoardPice.third.X + changeToBoardPice.axis.X), (changeToBoardPice.third.Y + changeToBoardPice.axis.Y)));

            for (int i = 0; i < 4; i++)
            {
                gameBoard[(int)changeToBoardArray[i].X + X, ((int)changeToBoardArray[i].Y + Y)] = 1;
            }
            changeToBoardPice = nextPice;
            NextPice();
            return changeToBoardPice;

        }
        public static bool LineChecker(int line, int[,] board)
        {
            bool fullLineCheck = true;
            for (int i = 0; i < 10; i++)
            {
                if (board[i, line] == 0) return false;
            }
            if (fullLineCheck == true) return true;
            else return false;
        }
        public static void RemovedLines(int[,] gameBoard, List<int> removedLines)
        {
            removedLines.Sort();
            for (int removeCounter = 0; removeCounter < removedLines.Count; removeCounter++)
            {
                for (int j = removedLines[removeCounter]; j >= 0; j--)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (j > 0) gameBoard[i, j] = gameBoard[i, j - 1];
                        if (j == 0) gameBoard[i, j] = 0;
                    }
                }
            }
        }
    }
}

