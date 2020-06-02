using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SelectStageController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private LockedPopup lockedGameModePopup;
    [SerializeField]
    private SelectGameModeButton selectGameModeButtonPrefab;
    [SerializeField]
    private Transform selectGameModeButtonParent;
    [SerializeField]
    private SelectDifficultyButton selectDifficultyButtonPrefab;
    [SerializeField]
    private Transform selectDifficultyButtonParent;
    [SerializeField]
    private Button playButton;
#pragma warning restore 0649

    private List<SelectGameModeButton> selectGameModeButtons = new List<SelectGameModeButton>();
    private List<SelectDifficultyButton> selectDifficultyButtons = new List<SelectDifficultyButton>();

    private void Init()
    {
        DrawGameModeButtons();
        DrawDifficultyButtons();
    }

    private void DrawGameModeButtons()
    {
        // Always set SelectedGameMode into addition as others are not implemented yet
        GameManager.Instance.SelectedGameMode = GameMode.ADDITION;

        foreach (var gameMode in Constants.GameModes)
        {
            var button = Instantiate(selectGameModeButtonPrefab, selectGameModeButtonParent);
            button.Init(gameMode, gameMode.ToString());
            button.Clicked += HandleGameModeButtonClicked;
            if (gameMode != GameMode.ADDITION)
            {
                button.SetLockedState();
            }
            selectGameModeButtons.Add(button);
        }
    }

    private void DrawDifficultyButtons()
    {
        int index = 0;
        foreach (var difficulty in Constants.Difficulties)
        {
            var button = Instantiate(selectDifficultyButtonPrefab, selectDifficultyButtonParent);
            button.Init(difficulty);
            button.Clicked += HandleDifficultyButtonClicked;
            selectDifficultyButtons.Add(button);

            if (index == 0)
            {
                button.Select();
            }
            index++;
        }
    }

    private void HandleGameModeButtonClicked(GameMode gameMode, bool isLocked)
    {
        // Set button state

        if (isLocked)
        {
            lockedGameModePopup.Show();
        }
    }

    private void HandleDifficultyButtonClicked(Difficulty difficulty)
    {
        foreach (var difficultyButton in selectDifficultyButtons)
        {
            if (difficultyButton.GetDifficulty == difficulty)
            {
                difficultyButton.Select();
            }
            else
            {
                difficultyButton.UnSelect();
            }
        }
    }

    private void HandlePlayButton()
    {
        SceneManager.LoadScene(Constants.SceneNames.GAME_MENU);
    }

    private void Awake()
    {
        playButton.onClick.AddListener(HandlePlayButton);

        Init();
    }

    private void OnDestroy()
    {
        foreach (var gameModeButton in selectGameModeButtons)
        {
            gameModeButton.Clicked -= HandleGameModeButtonClicked;
        }

        foreach (var difficultyButton in selectDifficultyButtons)
        {
            difficultyButton.Clicked -= HandleDifficultyButtonClicked;
        }
    }
}
