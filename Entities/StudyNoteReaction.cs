namespace DevStudyNotes.API.Entities
{
    public class StudyNotesReaction
    {
        public int Id { get; private set; }
        public bool IsPositive { get; private set; }
        public int StudyNoteId { get; private set; }

        public StudyNotesReaction(bool isPositive)
        {
            IsPositive = IsPositive;
        }
    }
}
