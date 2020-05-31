using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndPanelController : MonoBehaviour
{
    private const string CORRECT_ANSWER_TEXT_KEY = "Correct Answer: {0}/{1}";

#pragma warning disable 0649
    [SerializeField]
    private TextMeshProUGUI gameDetailsText;
    [SerializeField]
    private TextMeshProUGUI correctAnswerText;
    [SerializeField]
    private Button button;
#pragma warning restore 0649

    public void Init(string gameDetailsString, int correctAnswer, int totalQuestions)
    {
        gameDetailsText.text = gameDetailsString;
        correctAnswerText.text = string.Format(CORRECT_ANSWER_TEXT_KEY, correctAnswer, totalQuestions);
        gameObject.SetActive(true);
    }

    private void HandleButtonClick()
    {
        SceneManager.LoadScene(Constants.SceneNames.DIFFICULTY_MENU);
    }

    private void Awake()
    {
        button.onClick.AddListener(HandleButtonClick);
    }
}
