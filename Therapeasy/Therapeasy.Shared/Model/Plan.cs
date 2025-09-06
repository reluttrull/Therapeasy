using Therapeasy.Enums;

namespace Therapeasy.Model
{
    public class Plan
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public BodyRegion BodyRegion { get; set; }

        public string CreatedByUser { get; set; } = "";

        public DateTime DateCreated { get; set; }

        public string GetFormattedDateAdded()
        {
            return DateCreated.ToShortDateString();
        }
    }
}
