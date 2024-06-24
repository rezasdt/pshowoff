using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Tutorial : ScriptableObject
{
    [field: SerializeField] public List<TutorialData> tutorialList { get; private set; } = new();
}

[System.Serializable]
public class TutorialData
{
    [field: TextArea]
    [field: SerializeField] public string Description { get; private set; }

    [field: SerializeField] public string VideoFileName { get; private set; }
}