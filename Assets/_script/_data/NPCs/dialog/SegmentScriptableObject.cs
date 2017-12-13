using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Segment", menuName = "NPC/Dialog/Segment", order = 1)]
public class SegmentScriptableObject : ScriptableObject
{
    [SerializeField]
    MessageScriptableObject[] messages;

    [SerializeField]
    int assignedQuestID;

    [SerializeField]
    int[] questReqs;

    [SerializeField]
    int[] triggerReqs;

    public MessageScriptableObject[] Messages
    {
        get
        {
            return messages;
        }
    }

    public int AssignedQuestID
    {
        get
        {
            return assignedQuestID;
        }
    }

    public int[] QuestReqs
    {
        get
        {
            return questReqs;
        }
    }

    public int[] TriggerReqs
    {
        get
        {
            return triggerReqs;
        }
    }
}
