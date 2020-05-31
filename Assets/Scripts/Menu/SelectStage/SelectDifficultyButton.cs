using System;

public class SelectDifficultyButton : SelectStageParentButton
{
    private Difficulty difficulty;
    public Action<Difficulty> Clicked;

    public Difficulty GetDifficulty { get { return difficulty; } }

    public void Init(Difficulty difficulty)
    {
        this.difficulty = difficulty;
        textMeshPro.text = difficulty.ToString().ToUpper();
    }

    public override void Select()
    {
        base.Select();
        GameManager.Instance.SelectedDifficulty = difficulty;
    }

    private void HandleButtonClick()
    {
        Clicked(difficulty);
    }

    private void Awake()
    {
        button.onClick.AddListener(HandleButtonClick);
    }
}
