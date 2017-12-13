using UnityEngine;

namespace QuestSystem
{
    public class LocationObjective : IQuestObjective
    {
        private string title;
        private string description;
        private string objectiveType = "location";
        private bool isComplete;
        private bool isBonus;

        [SerializeField]
        private Location locale;

        public LocationObjective(Location loc, string descrip, bool bonus)
        {
            title = "Go to " + loc.Title;
            locale = loc;
            description = descrip;
            isBonus = bonus;
            CheckProgress();
        }

        public string Title
        {
            get
            {
                return title;
            }
        }

        public Location Locale
        {
            get
            {
                return locale;
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

        public bool IsComplete
        {
            get
            {
                Debug.Log(title + ".isComplete = " + isComplete);
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

        public void CheckProgress()
        {
        }

        public void UpdateProgress()
        {
            isComplete = true;
        }
    }
}