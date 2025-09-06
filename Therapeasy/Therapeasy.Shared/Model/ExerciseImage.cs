namespace Therapeasy.Model
{
    public class ExerciseImage
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string CreatedByUser { get; set; } = "";
        public DateTime DateCreated { get; set; }
        public byte[] ImageData { get; set; } = [];
        public string ContentType { get; set; } = ""; // e.g., "image/png", "image/jpg", "image/gif"

        public string GetImageForDisplay()
        {
            var base64 = Convert.ToBase64String(ImageData);
            return String.Format("data:image/gif;base64,{0}", base64);
        }
    }
}