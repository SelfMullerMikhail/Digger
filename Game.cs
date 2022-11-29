using System.Windows.Forms;

namespace Digger
{
    public static class Game
    {
        private const string mapWithPlayerTerrain = @"
 SSSS 
TTTTTP
    S T
MM   T
TTTTTT";
        private const string mapWithPlayerTerrain2 = @"
SSSSSSS   S  T
TTTTTTTT     T
    M  T     T
SSSSSSST     T
   M   T     T
TTTTTTTT  P  T
             T";

        private const string mapWithPlayerTerrainSackGold = @"
TMT T    
T T T    
T T T  M 
T T T    
T TMTTTTT
TTTTTTTTT
M        
TTTTTTTTT
        M";

        private const string mapWithPlayerTerrainSackGoldMonster = @"
P   TTTTTTTTT   M  TTM  GTTTTT
TTTT TTTGTTGTTTGTTTTTT GTTTTTT
TTTT TTTTG    M GTTTTT GTTTSTT
TM   TTTTT TTTTGTTTGTT GTTTSTT
TTTT TTTTT      GTTTTTT TTTSTT
TM   TTTS   TTTTTTSTTT TTTTTTT
TTTT TTTTTT   M   GSTT TT MTTT
TM   TTTTTT TTTTTTSSTT TTTTTTT
TTTT  M     STTTSTSTTT TTGTTTT
TM   TTTT  TTTTTTTTTTS TTTGTTT
SSSSS TTT  TTTSGTTTM   STTTTTT
TTTTT TTT  TTTSGTTT TTSTTTGTTT
SSSMM TTT TTTTTTTTT TTTMTTGTTT
MMMMM TTT  TTTTTTTT TTT MTTTTT
TTTTTGTT          M TTT    M  
TTGT    M TTTTTTTTTTTTT M TTTT
TTG  TTTTTT   S           TGGG
TTTTTTTTTTT   MTTTTTTTT   TGGT";

        private const string mapWithPlayerTerrainSackGoldMonster2 = @"
M                            M
 TTTTTTTTTTTTTTTTTSSSSSSSSSTTT
 TTPTTSSSSSSSSSSTTTGG    MMTTT
 TTTTTTTTTTTTTTTTTTTTG   TTTTT
 TTTTTT         TTTTTTS  TTTTT
 TTTTTT         TTTTTTT  TTTTT
 TTTTTTMMMMMMMGSTTTTTTT  TTTTT
 TTTTTTTTTTTTTTTTTTTTTT  TTTTT
 TTTTTTTTTTTTTTTTTTTTTT  TTTTT
 TTTTTTTTTTTTTTTTTTTTTTMMTTTTT
 TTTTTTTTTTTTTTTTTTTTTTTTTTTTT
 TTTTTTTTTTTTTTTT TMTM      MT
 TTTTTTTTTTTTTTTT T T        T
 TTTTTTTT      MT T T        T
 TTTTTTTTTTTTTTTT T T        T
 TTTTTTTM       TMT TM     M T
MTTTTTTTTTTTTTTTTTTTTTTTTTTTTT";

        public static ICreature[,] Map;
        public static int Scores;
        public static bool IsOver;
        public static ICreature[] persons;

        public static Keys KeyPressed;
        public static int MapWidth => Map.GetLength(0);
        public static int MapHeight => Map.GetLength(1);

        public static void CreateMap()
        {
            Map = CreatureMapCreator.CreateMap(mapWithPlayerTerrainSackGoldMonster2);
        }
    }
}