using UnityEngine;
using UnityEngine.UI;

public class LockedPopup : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Button background;
    [SerializeField]
    private Button closeButton;
#pragma warning restore 0649

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        background.onClick.AddListener(ClosePanel);
        closeButton.onClick.AddListener(ClosePanel);
    }
}
