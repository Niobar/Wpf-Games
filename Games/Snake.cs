using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games
{
    public class Snake
    {
        public bool gameOver = false;
        public int[,] gameBoard = new int[10, 20];
        public List<int> avaiableCoordinates = new List<int>();
        public List<SnakePice> snake = new List<SnakePice>();
        public SnakePice piceToSwallow;
        public SnakePice shadingPice;
        int snakeDirection;
        public bool swallowed;
        Random randomizer = new Random();
        public int snakeGameScore = 0;
        public void SnakeStart()
        {
            avaiableCoordinates.Clear();
            for (int i = 0; i < 200; i++)
            {
                avaiableCoordinates.Add(i);
            }
            SnakePice snakeStart;
            snakeStart.x = 5;
            snakeStart.y = 10;
            snake.Add(snakeStart);
            avaiableCoordinates.Remove(105);
            snakeDirection = 0;
            PiceToSwallow();
        }


        public SnakePice PiceToSwallow()
        {
            int listNumber = randomizer.Next(0, avaiableCoordinates.Count);
            piceToSwallow.x = avaiableCoordinates[listNumber] % 10;
            piceToSwallow.y = avaiableCoordinates[listNumber] / 10;
            avaiableCoordinates.RemoveAt(listNumber);
            return piceToSwallow;
        }
        public void snakeRotation(int directionChange)
        {
            snakeDirection = directionChange;
        }
        public void snakeMove()
        {
            swallowed = true;
            SnakePice newSnakeHead;
            switch (snakeDirection)
            {
                case 0:
                    newSnakeHead.x = snake.Last().x;
                    newSnakeHead.y = snake.Last().y + 1;
                    break;
                case 1:
                    newSnakeHead.x = snake.Last().x - 1;
                    newSnakeHead.y = snake.Last().y;
                    break;
                case 2:
                    newSnakeHead.x = snake.Last().x;
                    newSnakeHead.y = snake.Last().y - 1;
                    break;
                case 3:
                    newSnakeHead.x = snake.Last().x + 1;
                    newSnakeHead.y = snake.Last().y;
                    break;
                default:
                    // does this ever being used ?
                    newSnakeHead.x = snake.Last().x + 10;
                    newSnakeHead.y = snake.Last().y + 10;
                    break;
            }
            if (EndGameCheck(newSnakeHead)) EndGame();
            else if (!gameOver)
            {
                if (newSnakeHead.x != piceToSwallow.x || newSnakeHead.y != piceToSwallow.y)
                {
                    shadingPice.x = snake[0].x;
                    shadingPice.y = snake[0].y;
                    avaiableCoordinates.Add(shadingPice.x + (shadingPice.y * 10));
                    snake.RemoveAt(0);
                    swallowed = false;
                }
                snake.Add(newSnakeHead);
                avaiableCoordinates.Remove(newSnakeHead.x + (newSnakeHead.y * 10));
                if (swallowed) snakeGameScore = snakeGameScore + 100;
            }
        }
        public bool EndGameCheck(SnakePice snakeHeadPart)
        {
            if (snakeHeadPart.x < 0 || snakeHeadPart.x > 9 || snakeHeadPart.y < 0 || snakeHeadPart.y > 19) return true;
            else
                for (int i = 0; i < snake.Count; i++)
                {
                    if (snakeHeadPart.x == snake[i].x && snakeHeadPart.y == snake[i].y) return true;
                }
            return false;
        }

        public void EndGame()
        {
            gameOver = true;
        }
    }
}
