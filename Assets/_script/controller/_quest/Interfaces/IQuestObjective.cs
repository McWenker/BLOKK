namespace QuestSystem
{
    public interface IQuestObjective
    {
        string Title { get; }
        string Description { get; }
        string ObjectiveType { get; }
        bool IsComplete { get;  }
        bool IsBonus { get; }
        void UpdateProgress();
        void CheckProgress();
    }
}
