namespace YouTubeVideoParser
{
    public class Video
    {
        public string Id { get; set; }
        public string Lang { get; set; }

        public Video(string id, string lang)
        {
            Id = id;
            Lang = lang;
        }
    }
}
