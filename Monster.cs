using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digger
{
    class Monster : ICreature
    {
        public bool conflictFlag { private set; get; }
        private int xAction;
        private int yAction;
        private ICreature meetObj;
        public Monster()
        {
            conflictFlag = true;
        }
        public CreatureCommand Act(int x, int y)
        {
            if (x == Player.PlayerPositionX && y < Player.PlayerPositionY) { yAction = 1; } else if (x == Player.PlayerPositionX && y > Player.PlayerPositionY) { yAction = -1; } else { yAction = RandomHelper.RandomXY(y, Game.MapHeight, 0); }
            if (y == Player.PlayerPositionY && x < Player.PlayerPositionX) { xAction = 1; } else if (y == Player.PlayerPositionY && x > Player.PlayerPositionY) { xAction = -1; } else { xAction = RandomHelper.RandomXY(x, Game.MapWidth, yAction); }

            meetObj = Game.Map[x + xAction, y + yAction];
            if (meetObj is Terrain || meetObj is Monster || meetObj is Sack)
            {
                xAction = 0;
                yAction = 0;
            }
            return new CreatureCommand() { DeltaX = xAction, DeltaY = yAction, TransformTo = null };
        }
        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Sack) // Very sad, that impossible doing downcast inside lazy calculation. 
            {
                var sack = (Sack)conflictedObject;
                if(sack.counter >= 2)
                conflictFlag = true;
            }
            return conflictFlag;
        }

        public int GetDrawingPriority() { return 2; }
        public string GetImageFileName() { return "Monster.png"; }
    }
}
