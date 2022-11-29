using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml.Linq;

namespace Digger
{
    static class HelperingFunctions
    {
        static public int RandomXY(int x, int mapSize, Random randomer)
        {
            int xyHelp = 0;
            if (x == 0) { xyHelp = randomer.Next(0, 2); }
            else if (x == mapSize - 1) { xyHelp = randomer.Next(-1, 0); }
            else { xyHelp = randomer.Next(-1, 2); }
            return xyHelp;
        }
        static public int summer(int numb, int Helper)
        { return numb + Helper; }
    }

    public struct  UpObject {
        public int x;
        public int y;
    };


    class Player : ICreature
    {
        public static int XPosition; // Используетсы! VS не видит связи
        public static int YPosition; // Используетсы! VS не видит связи
        public CreatureCommand Act(int x, int y)
        {
            XPosition = x;
            YPosition = y;
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
            if (conflictedObject is Monster || conflictedObject is Sack) { return true; }
            return false;
        }
        public int GetDrawingPriority()
        { return 0; }
        public string GetImageFileName()
        { return "Digger.png"; }
    }
    class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        { return new CreatureCommand(); }
        public bool DeadInConflict(ICreature conflictedObject)
        { return true; }
        public int GetDrawingPriority()
        { return 2; }
        public string GetImageFileName()
        { return "Terrain.png"; }
    }
    class Monster : ICreature
    {
        UpObject upObject = new UpObject();
        public int RandomXY(int x, int mapSize, Random randomer, int flag)
        {
            int xyHelp = 0;
            if (flag == 0)
            {
                if (x == 0) { xyHelp = randomer.Next(0, 2); }
                else if (x == mapSize - 1) { xyHelp = randomer.Next(-1, 0); }
                else { xyHelp = randomer.Next(-1, 2); }
            }
            return xyHelp;
        }
        public CreatureCommand Act(int x, int y)
        {
            upObject.x = x;
            upObject.y = y - 1;
            int xHelper = 0;
            int yHelper = 0;
            int PlayerPositionX = Player.XPosition;
            int PlayerPositionY = Player.YPosition;
            object meetObj;
            var randomer = new Random();

            if (x == PlayerPositionX && y < PlayerPositionY) { yHelper = 1; } else if (x == PlayerPositionX && y > PlayerPositionY) { yHelper = -1; } else { yHelper = RandomXY(y, Game.MapHeight, randomer, 0); }
            if (y == PlayerPositionY && x < PlayerPositionX) { xHelper = 1; } else if (y == PlayerPositionY && x > PlayerPositionY) { xHelper = -1; } else { xHelper = RandomXY(x, Game.MapWidth, randomer, yHelper); }
            if (Game.Map[x + xHelper, y + yHelper] is Terrain) { xHelper = 0; yHelper = 0; }
            meetObj = Game.Map[x + xHelper, y +yHelper];
            if (meetObj is Terrain || meetObj is Monster || meetObj is Sack) { xHelper = 0; yHelper = 0; }
            return new CreatureCommand() { DeltaX = xHelper, DeltaY = yHelper, TransformTo = null };
        }
        public bool DeadInConflict(ICreature conflictedObject)
        {
            var obj = Game.Map[upObject.x, upObject.y];
            if ( obj is Sack) { obj.DeadInConflict(this);}
            if (conflictedObject is Gold) { return false; }
            
            return true; }

        public int GetDrawingPriority()
        { return 2; }

        public string GetImageFileName()
        { return "Monster.png"; }
    }
    class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        { return new CreatureCommand(); }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Player) { Game.Scores += 10; }
            return true;
        }

        public int GetDrawingPriority()
        { return 2; }

        public string GetImageFileName()
        { return "Gold.png"; }
    }
    class Sack : ICreature
    {
        public int counter = 0;
        public CreatureCommand Act(int x, int y)
        {
            ICreature trans = null;
            int xHelp = 0;
            int yHelp = 0;
            if (y != Game.MapHeight - 2)
            {
                if (Game.Map[x, y + 1] is null & (Game.MapHeight - 1 != y))
                {
                    counter++;
                    yHelp++;
                    var ObjDownTerrain = Game.Map[x, y + 2];
                    if (counter >= 1 
                        & (ObjDownTerrain is Gold || ObjDownTerrain is Terrain || y >= Game.MapHeight - 1 || ObjDownTerrain is Sack))
                    {
                        counter = 0;
                        trans = new Gold();
                    }
                    else if (ObjDownTerrain is Player)
                    {
                        yHelp++;
                        counter = 0;
                        Game.IsOver = true;
                    }
                }
                else { counter = 0; }
            }
            return new CreatureCommand() { DeltaX = xHelp, DeltaY = yHelp, TransformTo = trans };
        }
        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Monster || conflictedObject is Player) {return false; }
            return true;
        }

        public int GetDrawingPriority()
        { return 2; }

        public string GetImageFileName()
        { return "Sack.png"; }
    }


}
