namespace DevStudyNotes.API.Entities
{
    public class StudyNote
    {

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public bool IsPublic { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public List<StudyNotesReaction> Reactions { get; private set; }

        public StudyNote(string title, string description, bool isPublic)
        {
            Title = title;
            Description = description;
            IsPublic = isPublic;

            Reactions= new List<StudyNotesReaction>();
            CreatedAt= DateTime.Now;
        }

        public void AddReaction(bool isPositive)
        {
            if (!IsPublic)
                throw new InvalidOperationException();

            Reactions.Add(new StudyNotesReaction(isPositive));
        }
    }
}
