using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectStageParentButton : MonoBehaviour
{
    protected static Color32 originalColor = new Color32(255, 255, 255, 255);
    protected static Color32 lockedColor = new Color32(255, 255, 255, 150);

#pragma warning disable 0649
    [SerializeField]
    protected Sprite selectedSprite;
    [SerializeField]
    protected Sprite unselectedSprite;
    [SerializeField]
    protected Image buttonImage;
    [SerializeField]
    protected GameObject lockedGameObject;
    [SerializeField]
    protected TextMeshProUGUI textMeshPro;
    [SerializeField]
    protected Button button;
#pragma warning restore 0649

    public virtual void Select()
    {
        buttonImage.sprite = selectedSprite;
    }

    public virtual void UnSelect()
    {
        buttonImage.sprite = unselectedSprite;
    }

    public virtual void SetLockedState()
    {
        buttonImage.color = lockedColor;
        textMeshPro.color = lockedColor;
        lockedGameObject.SetActive(true);
    }
}
