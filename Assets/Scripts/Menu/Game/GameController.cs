using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Random = System.Random;
using TMPro;

public class GameController : MonoBehaviour
{
    private const float TIMER_WAIT_FOR_SECONDS = 1f;
    private const int MIN_DIFFERENCE_MARGIN = 1;
    private const int MAX_DIFFERENCE_MARGIN = 15;
    private const int TOTAL_ROUNDS = 10;
    private const string GAME_DETAILS_TEXT_KEY = "GameMode: {0}, Difficulty: {1}";
    private const string ROUND_TEXT_KEY = "Round: {0}/{1}";
    private const string ADDITION_EQUATION_TEXT_KEY = "Equation: {0} + {1}";
    private const string TIMER_TEXT_KEY = "Time Left: {0}";

#pragma warning disable 0649
    [Header("Prefabs")]
    [SerializeField]
    private GameChoicesButton gameChoicesButtonPrefab;
    [Header("UI")]
    [SerializeField]
    private EndPanelController endPanelController;
    [SerializeField]
    private Transform choicesParent;
    [SerializeField]
    private TextMeshProUGUI gameDetailsText;
    [SerializeField]
    private TextMeshProUGUI roundNumberText;
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private TextMeshProUGUI equationText;
    [Header("DifficultyData")]
    [SerializeField]
    private DifficultyScriptableObject easyDifficultyData;
    [SerializeField]
    private DifficultyScriptableObject normalDifficultyData;
    [SerializeField]
    private DifficultyScriptableObject hardDifficultyData;
#pragma warning restore 0649

    private int minValue = 0;
    private int maxValue = 0;
    private int timerMaxValue = 0;
    private int numberOfChoices = 0;

    private int currentRound = 1;
    private int correctAnswers = 0;

    private int firstNumberInCurrentRound = 0;
    private int secondNumberInCurrentRound = 0;

    private Difficulty selectedDifficulty;
    private Coroutine countdownTimerCoroutine;
    private int currentTimerValue = 0;

    private List<GameChoicesButton> gameChoicesButtons = new List<GameChoicesButton>();

    private string GetGameDetails()
    {
        var gameMode = GameManager.Instance.SelectedGameMode;
        return string.Format(GAME_DETAILS_TEXT_KEY, gameMode, selectedDifficulty);
    }

    private void SetGameDetails()
    {
        gameDetailsText.text = GetGameDetails();
    }

    private void PreloadChoicesButton()
    {
        for (int ctr = 0; ctr < numberOfChoices; ctr++)
        {
            var button = Instantiate(gameChoicesButtonPrefab, choicesParent);
            button.Clicked += HandleChoicesClicked;
            gameChoicesButtons.Add(button);
        }
    }

    private void StartRound()
    {
        if (currentRound > TOTAL_ROUNDS)
        {
            EndGame();
            return;
        }

        roundNumberText.text = string.Format(ROUND_TEXT_KEY, currentRound, TOTAL_ROUNDS);
        StartTimer();
        CreateEquation();
        CreateChoices();
    }

    private void StartTimer()
    {
        if (countdownTimerCoroutine != null)
        {
            StopCoroutine(countdownTimerCoroutine);
            countdownTimerCoroutine = null;
        }

        currentTimerValue = timerMaxValue;
        timerText.text = string.Format(TIMER_TEXT_KEY, currentTimerValue);
        countdownTimerCoroutine = StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        while (currentTimerValue > 0)
        {
            yield return new WaitForSeconds(TIMER_WAIT_FOR_SECONDS);
            currentTimerValue--;
            timerText.text = string.Format(TIMER_TEXT_KEY, currentTimerValue);
        }
        timerText.text = string.Format(TIMER_TEXT_KEY, timerMaxValue);

        NextRound();
    }

    private void CreateEquation()
    {
        var rand = new Random();
        firstNumberInCurrentRound = GetRandomNumberForEquation(rand);
        secondNumberInCurrentRound = GetRandomNumberForEquation(rand);

        equationText.text = string.Format(ADDITION_EQUATION_TEXT_KEY, firstNumberInCurrentRound, secondNumberInCurrentRound);
    }

    private void CreateChoices()
    {
        Random rng = new Random();
        var correctAnswer = GetCorrectAnswer();
        var listOfAnswers = new List<int>();
        listOfAnswers.Add(correctAnswer);
        for (int ctr = 0; ctr < numberOfChoices - 1; ctr++)
        {
            var newAnswer = GetRandomAnswer(listOfAnswers);
            listOfAnswers.Add(newAnswer);
        }
        var shuffledAnswer = listOfAnswers.OrderBy(a => rng.Next());

        int answerCtr = 0;
        foreach (var answer in shuffledAnswer)
        {
            var button = gameChoicesButtons[answerCtr];
            button.Init(answer);
            answerCtr++;
        }
    }

    private int GetRandomAnswer(List<int> listOfAnswers)
    {
        Random rng = new Random();
        var correctAnswer = GetCorrectAnswer();
        var newAnswer = 0;
        var randomMargin = rng.Next(MIN_DIFFERENCE_MARGIN, MAX_DIFFERENCE_MARGIN);
        if (rng.Next(0, 2) == 0)
        {
            newAnswer = correctAnswer + randomMargin;
        }
        else
        {
            newAnswer = correctAnswer - randomMargin;
        }

        if (listOfAnswers.Contains(newAnswer))
        {
            newAnswer = GetRandomAnswer(listOfAnswers);
        }

        return newAnswer;
    }

    private int GetCorrectAnswer()
    {
        switch (GameManager.Instance.SelectedGameMode)
        {
            case GameMode.ADDITION:
                return firstNumberInCurrentRound + secondNumberInCurrentRound;
            default:
                Debug.LogError("Not supported game mode: " + GameManager.Instance.SelectedGameMode);
                return 0;
        }
    }

    private void HandleChoicesClicked(int value)
    {
        if (GetCorrectAnswer() == value)
        {
            correctAnswers++;
        }
        NextRound();
    }

    private void NextRound()
    {
        currentRound++;
        StartRound();
    }

    private int GetRandomNumberForEquation(Random rand)
    {
        return rand.Next(minValue, maxValue);
    }

    private void EndGame()
    {
        if (countdownTimerCoroutine != null)
        {
            StopCoroutine(countdownTimerCoroutine);
        }

        endPanelController.Init(GetGameDetails(), correctAnswers, TOTAL_ROUNDS);
    }

    private void LoadConfig()
    {
        switch (selectedDifficulty)
        {
            case Difficulty.EASY:
                SetConfig(easyDifficultyData);
                break;
            case Difficulty.NORMAL:
                SetConfig(normalDifficultyData);
                break;
            case Difficulty.HARD:
                SetConfig(hardDifficultyData);
                break;
            default:
                Debug.LogError("Selected difficulty not supported, using easy as default");
                SetConfig(easyDifficultyData);
                break;
        }
    }

    private void SetConfig(DifficultyScriptableObject difficultyData)
    {
        minValue = difficultyData.MinValue;
        maxValue = difficultyData.MaxValue;
        timerMaxValue = difficultyData.Time;
        numberOfChoices = difficultyData.NumberOfChoices;
    }

    private void Awake()
    {
        selectedDifficulty = GameManager.Instance.SelectedDifficulty;
        LoadConfig();
        SetGameDetails();
        PreloadChoicesButton();
        StartRound();
    }

    private void OnDestroy()
    {
        foreach (var button in gameChoicesButtons)
        {
            button.Clicked -= HandleChoicesClicked;
        }
    }
}
