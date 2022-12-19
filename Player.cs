using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digger
{
    class Player : ICreature
    {
        public static int PlayerPositionX { private set; get; }
        public static int PlayerPositionY { private set; get; }
        public Player()
        {
            PlayerPositionX = 0;  // Using for treaking of Monsters.
            PlayerPositionY = 0;
        }
        public CreatureCommand Act(int x, int y)
        {
            PlayerPositionX = x;
            PlayerPositionY = y;
            var a = Game.KeyPressed.ToString();
            int xHelp = 0;
            int yHelp = 0;
            if (a == "W" & y != 0) { if (Game.Map[x, y - 1] is Sack == false) { yHelp = -1; xHelp = 0; } }
            else if (a == "S" & y != Game.MapHeight - 1) { if (Game.Map[x, y + 1] is Sack == false) { yHelp = 1; xHelp = 0; } }
            else if (a == "A" & x != 0) { if (Game.Map[x - 1, y] is Sack == false) { xHelp = -1; yHelp = 0; } }
            else if (a == "D" & x != Game.MapWidth - 1) { if (Game.Map[x + 1, y] is Sack == false) { xHelp = 1; yHelp = 0; } }
            return new CreatureCommand() { DeltaX = xHelp, DeltaY = yHelp, TransformTo = null };
        }
        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Monster || conflictedObject is Sack)
            {
                return true;
            }
            return false;
        }

        public int GetDrawingPriority()
        {
            return 0;
        }


        public string GetImageFileName()
        {
            return "Digger.png";
        }
    }
}
