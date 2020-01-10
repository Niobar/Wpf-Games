using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Games
{
    static class HighScore
    {
        public static string HighScoreName()
        {
            string highScoreName = "none";
            return highScoreName;
        }
        public static void TetrisHighScore(int score = 0, string name = "none")
        {
            List<HighScoreEntry> tetrisHighScore = new List<HighScoreEntry>();

            using (StreamReader highScoreFileReader = new StreamReader("TetrisHighScore.txt"))
            {
                string line;
                while ((line = highScoreFileReader.ReadLine()) != null)
                {
                    HighScoreEntry temp = new HighScoreEntry();
                    temp.Score = Int32.Parse(line.Substring(0, 9));
                    temp.Name = line.Substring(10);
                    tetrisHighScore.Add(temp);
                }
                if (name != "none")
                {
                    HighScoreEntry temp = new HighScoreEntry();
                    temp.Score = score;
                    temp.Name = name;
                    tetrisHighScore.Add(temp);
                }
            }

            tetrisHighScore = tetrisHighScore.OrderByDescending(a => a.Score).ToList();
            {

                int max = 10;
                if (max > tetrisHighScore.Count()) max = tetrisHighScore.Count;
                using (StreamWriter highScoreFileWriter = new StreamWriter("TetrisHighScore.txt"))
                {
                    for (int i = 0; i < max; i++)
                    {
                        highScoreFileWriter.WriteLine(tetrisHighScore[i].ToString());
                    }
                }
            }
        }
    }
}
