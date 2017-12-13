using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour
{
	[SerializeField]
	string speakerName;

    [SerializeField]
    Sprite speakerSprite;

    [SerializeField]
    bool isTrigger;

    bool triggered = false;
    int segNum;
    int holdSegNum;

    [SerializeField]
    SegmentScriptableObject[] segment;

    private int messageCount = 0;
    private bool isSpeaking;

    bool IsSpeaking
    {
        set
        {
            isSpeaking = value;
        }
    }

	public void Speak() // eventually needs to look at QuestManager and evaluate Segment
	{
        if (isTrigger && !triggered)
        {
            triggered = true;
            QuestController.instance.CheckNPC(gameObject.name);
        }

        segNum = FindSeg();
        if (holdSegNum != segNum)
            messageCount = 0;
        holdSegNum = segNum;

        IsSpeaking = true;
        if (!DialogManager.showingDialog)
        {
            if (!segment[segNum].Messages[messageCount].IsQuestion && !segment[segNum].Messages[messageCount].IsQuestTurn)
                DialogManager.PopUpDialog(speakerSprite, speakerName, segment[segNum].Messages[messageCount].MessageText, DialogManager.DialogType.OkDialog, YesAnswer);
            else if (segment[segNum].Messages[messageCount].IsQuestion && !segment[segNum].Messages[messageCount].IsRepeatQuestion)
                DialogManager.PopUpDialog(speakerSprite, speakerName, segment[segNum].Messages[messageCount].MessageText, DialogManager.DialogType.YesNoDialog, YesAnswer, NoAnswer);
            else if (segment[segNum].Messages[messageCount].IsQuestion && segment[segNum].Messages[messageCount].IsRepeatQuestion)
                DialogManager.PopUpDialog(speakerSprite, speakerName, segment[segNum].Messages[messageCount].MessageText, DialogManager.DialogType.YesNoDialog, YesAnswer);
            else if (segment[segNum].Messages[messageCount].IsQuestTurn)
                DialogManager.PopUpDialog(speakerSprite, speakerName, segment[segNum].Messages[messageCount].MessageText, DialogManager.DialogType.QuestDialog, QuestFinish);
        }
	}

    int FindSeg()
    {
        bool questsComplete;
        bool trigsComplete;

        for(int i = (segment.Length-1); i >= 0; i--)
        {
            questsComplete = false;
            trigsComplete = false;
            if (segment[i].QuestReqs.Length > 0)
            {
                if (QuestController.instance.CheckComplete(segment[i].QuestReqs, "quests"))
                {
                    questsComplete = true;
                }
            }

            if (segment[i].TriggerReqs.Length > 0)
            {
                if (QuestController.instance.CheckComplete(segment[i].TriggerReqs, "trigs"))
                {
                    trigsComplete = true;
                }
            }
            
            if (((segment[i].QuestReqs.Length > 0 && questsComplete == true) && (segment[i].TriggerReqs.Length > 0 && trigsComplete == true)) ||
                ((segment[i].QuestReqs.Length < 1) && (segment[i].TriggerReqs.Length > 0 && trigsComplete == true)) ||
                ((segment[i].QuestReqs.Length > 0 && questsComplete == true) && (segment[i].TriggerReqs.Length < 1)))
            {
                return i;
            }
        }
        return 0;
    }

    void NoAnswer()
    {
        int _temp = segment[segNum].Messages[messageCount].NoMessage;
        if (_temp < segment[segNum].Messages.Length)
        {
            messageCount = _temp;
            Speak();
        }
    }

    void YesAnswer()
    {
        if(segment[segNum].Messages[messageCount].IsQuestion && segment[segNum].AssignedQuestID != 99)
        {
            Debug.Log("Beginning quest with ID: " + segment[segNum].AssignedQuestID);
            QuestController.instance.BeginQuest(segment[segNum].AssignedQuestID);
        }
        int _temp = segment[segNum].Messages[messageCount].YesMessage;
        if (_temp < segment[segNum].Messages.Length)
        {
            messageCount = _temp;
            Speak();
        }
    }

    void QuestFinish()
    {
        QuestController.instance.FinishQuest(segment[segNum].AssignedQuestID);
        int _temp = segment[segNum].Messages[messageCount].YesMessage;
        if (_temp < segment[segNum].Messages.Length)
        {
            messageCount = _temp;
            Speak();
        }
    }

}
