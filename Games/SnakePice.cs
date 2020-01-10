using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games
{
    public struct SnakePice
    {
        public int x;
        public int y;

        public override bool Equals(object obj)
        {
            if (!(obj is SnakePice))
            {
                return false;
            }

            var pice = (SnakePice)obj;
            return x == pice.x &&
                   y == pice.y;
        }

        public override int GetHashCode()
        {
            var hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }
    }
}
