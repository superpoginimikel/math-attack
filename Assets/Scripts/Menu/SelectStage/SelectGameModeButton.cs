using System;

public class SelectGameModeButton : SelectStageParentButton
{
    private GameMode gameMode;
    public Action<GameMode, bool> Clicked;

    public void Init(GameMode gameMode, string gameModeText)
    {
        this.gameMode = gameMode;
        textMeshPro.text = gameModeText.ToUpper();
    }

    private void HandleButtonClick()
    {
        Clicked(gameMode, isStageLocked);
    }

    private void Awake()
    {
        button.onClick.AddListener(HandleButtonClick);
    }
}
