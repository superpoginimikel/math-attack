public class GameManager : Singleton<GameManager>
{
    public GameMode SelectedGameMode = GameMode.ADDITION;
    public Difficulty SelectedDifficulty = Difficulty.EASY;
}
