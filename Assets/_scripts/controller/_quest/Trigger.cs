using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{
    public class Trigger
    {
        private bool completed = false;

        public bool Completed
        {
            get
            {
                return completed;
            }
        }
        // Name
        // DescriptionSummary
        // TriggerID
        private ITriggerInformation information;
        public ITriggerInformation Information
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

        public Trigger(TriggerScriptableObject triggerData)
        {
            information = new TriggerInformation(triggerData);
            objectives = new List<IQuestObjective>();
            foreach (ObjectiveScriptableObject obj in triggerData.Objectives)
            {
                if (obj.ObjectiveType == "collection")
                {
                    CollectionObjectiveScriptableObject colObj = (CollectionObjectiveScriptableObject)obj;
                    CollectionObjective objectiveInstance = new CollectionObjective(colObj.Verb, colObj.CollectionTotal, colObj.ItemToCollect, colObj.Description, colObj.IsBonus);
                    objectives.Add(objectiveInstance);
                }

                if (obj.ObjectiveType == "location")
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
            foreach (IQuestObjective o in objectives)
            {
                if (o.ObjectiveType == "speak")
                {
                    SpeakObjective spk = (SpeakObjective)o;
                    if (spk.NPCToSpeak == NPC)
                    {
                        o.UpdateProgress();
                        IsComplete();
                        break;
                    }
                }
            }
        }

        private bool IsComplete()
        {
            for (int i = 0; i < objectives.Count; i++)
            {
                if (objectives[i].IsComplete != true && objectives[i].IsBonus == false)
                {
                    return false;
                }
            }

            Debug.Log(information.Title + " complete.");
            completed = true;
            return true; // trigger completed, can return to questgiver
        }
    }
}


