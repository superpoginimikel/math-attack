using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DifficultyScriptableObject")]
public class DifficultyScriptableObject : ScriptableObject
{
    public int MinValue;
    public int MaxValue;
    public int Time;
    public int NumberOfChoices;
}
