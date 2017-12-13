using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Speak Objective", menuName = "NPC/Quest/Objective/Speak", order = 3)]
public class SpeakObjectiveScriptableObject : ObjectiveScriptableObject
{
    [SerializeField]
    private string description;

    [SerializeField]
    private GameObject npcToSpeak;

    [SerializeField]
    private string locale;

    public string Description
    {
        get
        {
            return description;
        }
    }

    public GameObject NPCToSpeak
    {
        get
        {
            return npcToSpeak;
        }
    }

    public string Locale
    {
        get
        {
            return locale;
        }
    }
}
