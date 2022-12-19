using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digger
{
    static class RandomHelper
    {
        static int randomNumb = 0;
        static public int RandomXY(int x, int mapSize, int flag)
        {
            var rand = new Random(randomNumb);
            randomNumb++;
            int xyHelp = 0;
            if (flag == 0)
            {
                if (x == 0) { xyHelp = rand.Next(0, 2); }
                else if (x == mapSize - 1) { xyHelp = rand.Next(-1, 0); }
                else { xyHelp = rand.Next(-1, 2); }
            }
            
            return xyHelp;
        }
    }
}
