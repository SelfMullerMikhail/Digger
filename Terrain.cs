using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digger
{
    class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }
        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }
        public int GetDrawingPriority()
        {
            return 2;
        }
        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }
}
