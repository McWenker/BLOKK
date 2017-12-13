using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{
    public class Quest
    {
        private bool begun = false;
        private bool completed = false;
        private int ID;

        public bool Begun
        {
            get
            {
                return begun;
            }
        }

        public bool Completed
        {
            get
            {
                return completed;
            }
        }

        public int Identifier
        {
            get
            {
                return ID;
            }
        }
        // Name
        // DescriptionSummary
        // QuestHint
        // QuestDialog
        // SourceID
        // QuestID
        // IsChain
        // NextQuestID
        private IQuestInformation information;
        public IQuestInformation Information
        {
            get
            {
                return information;
            }
        }

        // Objectives
        private List<IQuestObjective> objectives;
        // BonusObjectives
        // Rewards
        // Events
            // on completion
            // on failure
            // on update

        public Quest(QuestScriptableObject questData)
        {
            information = new QuestInformation(questData);
            objectives = new List<IQuestObjective>();
            ID = information.QuestID;
            foreach(ObjectiveScriptableObject obj in questData.Objectives)
            {
                if(obj.ObjectiveType == "collection")
                {
                    CollectionObjectiveScriptableObject colObj = (CollectionObjectiveScriptableObject)obj;
                    CollectionObjective objectiveInstance = new CollectionObjective(colObj.Verb, colObj.CollectionTotal, colObj.ItemToCollect, colObj.Description, colObj.IsBonus);
                    objectives.Add(objectiveInstance);
                }
                
                if(obj.ObjectiveType == "location")
                {
                    LocationObjectiveScriptableObject locObj = (LocationObjectiveScriptableObject)obj;
                    LocationObjective objectiveInstance = new LocationObjective(locObj.Location, locObj.Description, locObj.IsBonus);
                    objectives.Add(objectiveInstance);
                }

                if (obj.ObjectiveType == "speak")
                {
                    SpeakObjectiveScriptableObject spkObj = (SpeakObjectiveScriptableObject)obj;
                    SpeakObjective objectiveInstance = new SpeakObjective(spkObj.NPCToSpeak, spkObj.Locale, spkObj.Description, spkObj.IsBonus);
                    objectives.Add(objectiveInstance);
                }
            }
        }

        public void BeginQuest()
        {
            foreach (IQuestObjective o in objectives)
            {
                if (o.ObjectiveType == "collection")
                {
                    CollectionObjective col = (CollectionObjective)o;
                    col.BeginCheck();
                }
            }
            Debug.Log(Information.Title + " begun.");
            begun = true;
            IsComplete();
        }

        public void CheckItem(string itemToCheck)
        {
            foreach(IQuestObjective o in objectives)
            {
                if(o.ObjectiveType == "collection")
                {
                    CollectionObjective col = (CollectionObjective)o;
                    if (itemToCheck == col.ItemToCollect.name)
                        o.UpdateProgress();
                    IsComplete();
                }
            }
        }

        public void CheckLoc(int locID)
        {
            foreach (IQuestObjective o in objectives)
            {
                if (o.ObjectiveType == "location")
                {
                    LocationObjective loc = (LocationObjective)o;
                    if (loc.Locale.LocID == locID)
                        o.UpdateProgress();
                    IsComplete();
                }
            }
        }

        public void CheckNPC(string NPC)
        {
            foreach(IQuestObjective o in objectives)
            {
                if(o.ObjectiveType == "speak")
                {
                    SpeakObjective spk = (SpeakObjective)o;
                    if (spk.NPCToSpeak == NPC)
                        o.UpdateProgress();
                    IsComplete();
                }
            }
        }

        private bool IsComplete()
        {
            if (begun)
            {
                for(int i = 0; i < objectives.Count; i++)
                {
                    if (objectives[i].IsComplete != true && objectives[i].IsBonus == false)
                    {
                        Debug.Log(Information.Title + " is not yet complete, waiting on objective " + objectives[i].Title);
                        return false;
                    }
                }

                Debug.Log(Information.Title + " is complete!");
                completed = true;
                return true; // quest completed, can return to questgiver
            }

            else
            {
                return false;
            }
        }
    }
}


