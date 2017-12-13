namespace QuestSystem
{
    public class TriggerInformation : ITriggerInformation
    {
        private string name;
        private string descriptionSummary;
        private int triggerID;

        public string Title
        {
            get
            {
                return name;
            }
        }

        public string DescriptionSummary
        {
            get
            {
                return descriptionSummary;
            }
        }

        public int TriggerID
        {
            get
            {
                return triggerID;
            }
        }

        public TriggerInformation(TriggerScriptableObject triggerData)
        {
            name = triggerData.TriggerName;
            descriptionSummary = triggerData.TriggerText;
            triggerID = triggerData.TriggerID;
        }
    }
}
