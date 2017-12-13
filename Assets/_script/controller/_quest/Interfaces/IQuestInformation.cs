namespace QuestSystem
{
    public interface IQuestInformation
    {
        string Title { get; }
        string DescriptionSummary { get; }
        string SourceID { get; }
        int QuestID { get; }
        bool IsChain { get; }
        int NextQuestID { get; }
    }
}

// Title
// DescriptionSummary
// QuestHint
// QuestDialog
// SourceID
// QuestID
// IsChain
// NextQuestID
