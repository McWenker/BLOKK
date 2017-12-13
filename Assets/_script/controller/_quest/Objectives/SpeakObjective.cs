using UnityEngine;

namespace QuestSystem
{
    public class SpeakObjective : IQuestObjective
    {
        private string title;
        private string description;
        private string objectiveType = "speak";
        private string npcToSpeak;
        private bool isComplete;
        private bool isBonus;

        public string Title
        {
            get
            {
                return title;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
        }

        public string ObjectiveType
        {
            get
            {
                return objectiveType;
            }
        }

        public string NPCToSpeak
        {
            get
            {
                return npcToSpeak;
            }
        }

        public bool IsComplete
        {
            get
            {
                return isComplete;
            }
        }

        public bool IsBonus
        {
            get
            {
                return isBonus;
            }
        }

        /// <summary>
        /// This constructor builds a SpeakObjective
        /// </summary>
        /// <param name="npc">NPC to speak to.</param>
        /// <param name="locale">Name of general NPC area.</param>
        /// <param name="descrip">Describe the objective</param>
        /// <param name="bonus">If true, it is a bonus objective.</param>
        public SpeakObjective(GameObject npc, string locale, string descrip, bool bonus)
        {
            title = "Speak to " + npc.name + " in "+ locale;
            description = descrip;
            npcToSpeak = npc.name;
            isBonus = bonus;
        }

        public void CheckProgress()
        {
        }

        public void UpdateProgress()
        {
            isComplete = true;
        }

        /*public override string ToString()
        {
            return collectionCurrent + "/" + collectionTotal + " " + itemToCollect.name + " " + verb + "ed.";
        }*/
    }
}