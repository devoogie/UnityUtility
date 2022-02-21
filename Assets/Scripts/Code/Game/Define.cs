namespace Define
{
    public static class Time
    {
        public const float Game_Wait = 2f;
        public const int Turn_Prepare = 10;
        public const float Merge_Effect = 0.5f;
        public const float Close_Toast = 2f;
    }
    public static class Hex
    {
        public static float Tile_Scale = 2.5f;
        public static float Tile_Pivot_Even = 0.75f;
        public static float Tile_Pivot_Odd = 0.25f;
    }
    public partial class Game
    {
        public static int Amount_Deck_Unit = 6;
        public static int Amount_Slot_Max = 6;
        public static int Amount_Slot_Start = 3;

        public static int Require_Win = 5;

        // public static int Cost_Action_Reroll = 3;
        public static int Cost_Action_Reroll = 1;
        public static int Cost_Action_Switch = 1;
        public static int Cost_Action_Slot = 2;

        public static int Amount_Action_Add_Turn = 1;
        public static int Amount_Action_Start = 0;
        public static int Amount_Action_Max = 5;
    }
    public static class Block
    {
        public static int Require_Amount_Merge = 2;
    }
}