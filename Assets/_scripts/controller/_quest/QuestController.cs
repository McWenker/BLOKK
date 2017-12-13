using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using QuestSystem;

public class QuestController : MonoBehaviour
{
    // Singleton reference
    private static QuestController _instance;

    // Public getter for the singleton reference
    public static QuestController instance
    {
        get
        {
            // If there is no instance for DialogManager yet and we're not shutting down at the moment
            if (_instance == null)
            {
                //Try finding and instance in the scene
                _instance = GameObject.FindObjectOfType<QuestController>();
                //If no instance was found, let's create one
                if (!_instance)
                {
                    GameObject singleton = (GameObject)Instantiate(Resources.Load("QuestController"));
                    singleton.name = "DialogManager";
                    _instance = singleton.GetComponent<QuestController>();
                }
                //Set the instance to persist between levels.
                DontDestroyOnLoad(_instance.gameObject);
            }
            //Return an instance, either that we found or that we created.
            return _instance;
        }
    }

    [SerializeField]
    QuestScriptableObject[] questsData;

    [SerializeField]
    TriggerScriptableObject[] triggersData;

    [SerializeField]
    Quest[] quests;

    [SerializeField]
    Trigger[] triggers;

    private List<Quest> allQuests;
    private List<Quest> activeQuests;

    private void Awake()
    {
        quests = new Quest[questsData.Length];
        activeQuests = new List<Quest>();

        for (int i = 0; i < quests.Length; i++)
        {
            Quest instanceOfQuest = new Quest(questsData[i]);
            quests[i] = instanceOfQuest;
        }

        triggers = new Trigger[triggersData.Length];
        for (int j = 0; j < triggers.Length; j++)
        {
            Trigger instanceOfTrig = new Trigger(triggersData[j]);
            triggers[j] = instanceOfTrig;
        }
    }

    public void BeginQuest(int questID)
    {
        for(int i = 0; i < quests.Length; i++)
        {
            if (questID == _instance.quests[i].Information.QuestID)
            {
                _instance.quests[i].BeginQuest();
                _instance.activeQuests.Add(quests[i]);
            }
        }
    }

    public bool CheckComplete(int[] reqIDs, string checkType)
    {
        if(checkType == "quests")
        {
            for(int j = 0; j < quests.Length; j++)
            {
                for(int i = 0; i < reqIDs.Length; i++)
                {
                    if (quests[j].Information.QuestID == reqIDs[i])
                    {
                        if (!quests[j].Completed)
                        {
                            return false;
                        }
                    }
                }
            }
        }

        else if(checkType == "trigs")
        {
            for(int i = 0; i < triggers.Length; i++)
            {
                for(int j = 0; j < reqIDs.Length; j++)
                {
                    if(_instance.triggers[i].Information.TriggerID == reqIDs[j])
                    {
                        if(!_instance.triggers[i].Completed)
                        {
                            return false;
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("checkType fail");
        }
        return true;
    }

    public void CheckItem(int questID, string itemToCheck)
    {
        foreach(Quest q in _instance.activeQuests)
        {
            if (questID == q.Information.QuestID && !q.Completed)
                q.CheckItem(itemToCheck);
        }
    }

    public void CheckLocation(int locCheck)
    {
        foreach (Quest q in _instance.activeQuests)
        {
            if(!q.Completed)
                q.CheckLoc(locCheck);
        }

        for (int i = 0; i < _instance.triggers.Length; i++)
        {
            if(!triggers[i].Completed)
                triggers[i].CheckLoc(locCheck);
        }
    }

    public void CheckNPC(string NPCtoCheck)
    {
        foreach(Quest q in _instance.activeQuests)
        {
            if (!q.Completed)
                q.CheckNPC(NPCtoCheck);
        }
        
        for(int i = 0; i < _instance.triggers.Length; i++)
        {
            if (!triggers[i].Completed)
                _instance.triggers[i].CheckNPC(NPCtoCheck);
        }
    }

    public void FinishQuest(int questID)
    {
        _instance.activeQuests = _instance.activeQuests.Where(x => questID != x.Information.QuestID).ToList();
    }

    public void CompleteTrigger(int trigID)
    {
        for(int i = 0; i < triggers.Length; i++)
        {
            if(trigID == triggers[i].Information.TriggerID)
            {
                // trigger is complete
            }
        }
    }

    public void FailQuest(int questID)
    {
        foreach (Quest q in _instance.activeQuests)
        {
            if (questID == q.Information.QuestID)
            {
                // quest was failed
                _instance.activeQuests.Remove(q);
            }
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Quests: ");
            foreach (Quest q in _instance.quests)
            {
                Debug.Log(q.Information.Title);
            }

            Debug.Log("Active Quests: ");
            foreach (Quest aq in _instance.activeQuests)
            {
                Debug.Log(aq.Information.Title);
            }
        }
    }
}
