namespace QuestSystem
{
    public class QuestInformation : IQuestInformation
    {
        private string name;
        private string descriptionSummary;
        private string sourceID;
        private int questID;
        private bool isChain;
        private int nextQuestID;

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

        public QuestInformation(QuestScriptableObject questData)
        {
            name = questData.QuestName;
            descriptionSummary = questData.QuestText;
            sourceID = questData.SourceID;
            questID = questData.QuestID;
            isChain = questData.IsChain;
            nextQuestID = questData.NextQuestID;
        }
    }
}
