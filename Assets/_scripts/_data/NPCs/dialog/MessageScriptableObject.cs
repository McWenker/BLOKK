using UnityEngine;

[CreateAssetMenu(fileName = "Message", menuName = "NPC/Dialog/Message", order = 2)]
public class MessageScriptableObject : ScriptableObject
{
    [SerializeField]
    private string messageText;

    [SerializeField]
    private bool isQuestion;

    [SerializeField]
    private bool isRepeatQuestion;

    [SerializeField]
    private bool isQuestTurn;

    [SerializeField]
    private int yesMessage; // yes or OK

    [SerializeField]
    private int noMessage;

    public string MessageText
    {
        get
        {
            return messageText;
        }
    }

    public bool IsQuestion
    {
        get
        {
            return isQuestion;
        }
    }

    public bool IsRepeatQuestion
    {
        get
        {
            return isRepeatQuestion;
        }
    }

    public bool IsQuestTurn
    {
        get
        {
            return isQuestTurn;
        }
    }

    public int YesMessage
    {
        get
        {
            return yesMessage;
        }
    }

    public int NoMessage
    {
        get
        {
            return noMessage;
        }
    }
}
