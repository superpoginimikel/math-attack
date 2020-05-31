using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class GameChoicesButton : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private TextMeshProUGUI valueText;
    [SerializeField]
    private Button button;
#pragma warning restore 0649

    private int savedValue;

    public Action<int> Clicked;

    public void Init(int value)
    {
        savedValue = value;
        valueText.text = value.ToString();
    }

    private void HandleButtonClicked()
    {
        Clicked(savedValue);
    }

    private void Awake()
    {
        button.onClick.AddListener(HandleButtonClicked);
    }
}
