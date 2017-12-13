using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestSystem;

public class ObjectiveScriptableObject : ScriptableObject
{
    [SerializeField]
    string objectiveType;
    
    [SerializeField]
    string title;

    [SerializeField]
    bool isBonus;

    public string ObjectiveType
    {
        get
        {
            return objectiveType;
        }
    }

    public string Title
    {
        get
        {
            return title;
        }
    }

    public bool IsBonus
    {
        get
        {
            return isBonus;
        }
    }
}
