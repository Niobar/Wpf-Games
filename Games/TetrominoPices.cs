using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Games
{
    public static class TetrominoPices
    {
        public static TetraminoPice[] tetraminos = new TetraminoPice[7];
        static TetrominoPices()
        {
            //for (int i = 0; i < 7; i++) tetraminos[i].tetrominoPiceNumber = i+1;

            //tetramino T
            tetraminos[0].first.X = -1;
            tetraminos[0].first.Y = 0;
            tetraminos[0].second.X = 1;
            tetraminos[0].second.Y = 0;
            tetraminos[0].third.X = 0;
            tetraminos[0].third.Y = 1;
            tetraminos[0].axis.Y = 0;
            tetraminos[0].brush = Brushes.Violet;
            
            //tetramino s
            tetraminos[1].first.X = -1;
            tetraminos[1].first.Y = 1;
            tetraminos[1].second.X = 0;
            tetraminos[1].second.Y = 1;
            tetraminos[1].third.X = 1;
            tetraminos[1].third.Y = 0;
            tetraminos[1].axis.Y = 0;
            tetraminos[1].brush = Brushes.LightGreen;
            //tetraminos z
            tetraminos[2].first.X = -1;
            tetraminos[2].first.Y = 0;
            tetraminos[2].second.X = 0;
            tetraminos[2].second.Y = 1;
            tetraminos[2].third.X = 1;
            tetraminos[2].third.Y = 1;
            tetraminos[2].axis.Y = 0;
            tetraminos[2].brush = Brushes.Red;
            //tetraminos L
            tetraminos[3].first.X = 0;
            tetraminos[3].first.Y = -2;
            tetraminos[3].second.X = 0;
            tetraminos[3].second.Y = -1;
            tetraminos[3].third.X = 1;
            tetraminos[3].third.Y = 0;
            tetraminos[3].axis.Y = 2;
            tetraminos[3].brush = Brushes.DarkOrange;
            //tetraminos J
            tetraminos[4].first.X = 0;
            tetraminos[4].first.Y = -2;
            tetraminos[4].second.X = 0;
            tetraminos[4].second.Y = -1;
            tetraminos[4].third.X = -1;
            tetraminos[4].third.Y = 0;
            tetraminos[4].axis.Y = 2;
            tetraminos[4].brush = Brushes.Blue;
            //tetraminos I
            tetraminos[5].first.X = 0;
            tetraminos[5].first.Y = -2;
            tetraminos[5].second.X = 0;
            tetraminos[5].second.Y = -1;
            tetraminos[5].third.X = 0;
            tetraminos[5].third.Y = 1;
            tetraminos[5].axis.Y = 2;
            tetraminos[5].brush = Brushes.LightBlue;
            //tetraminos O
            tetraminos[6].first.X = -1;
            tetraminos[6].first.Y = -1;
            tetraminos[6].second.X = 0;
            tetraminos[6].second.Y = -1;
            tetraminos[6].third.X = -1;
            tetraminos[6].third.Y = 0;
            tetraminos[6].axis.Y = 1;
            tetraminos[6].brush = Brushes.Yellow;
        }


    }
}
