using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Button playButton;
#pragma warning restore 0649

    private void HandlePlayButton()
    {
        SceneManager.LoadScene(Constants.SceneNames.DIFFICULTY_MENU);
    }

    private void Awake()
    {
        playButton.onClick.AddListener(HandlePlayButton);
    }
}
