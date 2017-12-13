using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestSystem;

[CreateAssetMenu(fileName = "Trigger", menuName = "NPC/Quest/Trigger", order = 2)]
public class TriggerScriptableObject : ScriptableObject
{
    protected string triggerType;

    [SerializeField]
    string triggerName;

    [SerializeField]
    string triggerText;

    [SerializeField]
    int triggerID;

    [SerializeField]
    ObjectiveScriptableObject[] objectives;

    public string TriggerName
    {
        get
        {
            return triggerName;
        }
    }

    public string TriggerText
    {
        get
        {
            return triggerText;
        }
    }

    public int TriggerID
    {
        get
        {
            return triggerID;
        }
    }

    public ObjectiveScriptableObject[] Objectives
    {
        get
        {
            return objectives;
        }
    }
}
