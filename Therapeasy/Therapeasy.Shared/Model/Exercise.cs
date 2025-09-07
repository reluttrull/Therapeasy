namespace Therapeasy.Model
{
    public class Exercise
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string CreatedByUser { get; set; } = "";

        public DateTime DateCreated { get; set; }

        public string Notes { get; set; } = "";

        public int SetsPerDay { get; set; }

        public int RepsPerSet { get; set; }

        public int ExerciseImageId { get; set; }

        public string GetFormattedDateAdded()
        {
            return DateCreated.ToShortDateString();
        }

        public string GetFormattedSetsAndReps()
        {
            return RepsPerSet + " repetitions, " + SetsPerDay + " times per day";
        }
    }
}
