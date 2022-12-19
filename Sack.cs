using System;
using System.Reflection;

namespace Digger
{
    class Sack : ICreature
    {
        public int counter { private set; get; }

        public Sack()
        {

            counter = 0;
        }
        public CreatureCommand Act(int x, int y)
        {
            ICreature trans = null;
            int xHelp = 0;
            int yHelp = 0;
            if (y+1 != Game.MapHeight)
            {
                var objDownSack = Game.Map[x, y + 1];

                if (objDownSack is null || ((objDownSack is Monster || objDownSack is Player) && counter > 2))
                {
                    counter++;
                    yHelp++;
                    var objDownSackY2 = Game.Map[x, y + 2];
                    if (counter >= 2 && (y == Game.MapHeight-3 || objDownSackY2 is Terrain || objDownSackY2 is Gold || objDownSackY2 is Sack))
                    {
                        trans = new Gold();
                    }
                }
                else { counter = 0; }
            }
            return new CreatureCommand() { DeltaX = xHelp, DeltaY = yHelp, TransformTo = trans };
        }
        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public string GetImageFileName()
        {
            return "Sack.png";
        }
    }
}

