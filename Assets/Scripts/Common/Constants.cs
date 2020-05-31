using System;
using System.Linq;

public static class Constants
{
    public static readonly GameMode[] GameModes = Enum.GetValues(typeof(GameMode)).Cast<GameMode>().ToArray();
    public static readonly Difficulty[] Difficulties = Enum.GetValues(typeof(Difficulty)).Cast<Difficulty>().ToArray();

    public static class SceneNames
    {
        public const string MAIN_MENU = "Main";
        public const string DIFFICULTY_MENU = "SelectStage";
        public const string GAME_MENU = "Game";
    }
}
