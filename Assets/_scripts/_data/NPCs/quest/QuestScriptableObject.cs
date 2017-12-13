using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestSystem;

[CreateAssetMenu(fileName = "Simple Quest", menuName = "NPC/Quest/Simple Quest", order = 1)]
public class QuestScriptableObject : ScriptableObject
{
    protected string questType;

    [SerializeField]
    string questName;

    [SerializeField]
    string questText;

    [SerializeField]
    string sourceID;

    [SerializeField]
    int questID;

    [SerializeField]
    bool isChain;

    [SerializeField]
    int nextQuestID;

    [SerializeField]
    ObjectiveScriptableObject[] objectives;

	public string QuestName
    {
        get
        {
            return questName;
        }
    }

    public string QuestText
    {
        get
        {
            return questText;
        }
    }

    public string SourceID
    {
        get
        {
            return sourceID;
        }
    }

    public int QuestID
    {
        get
        {
            return questID;
        }
    }

    public bool IsChain
    {
        get
        {
            return isChain;
        }
    }

    public int NextQuestID
    {
        get
        {
            return nextQuestID;
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
