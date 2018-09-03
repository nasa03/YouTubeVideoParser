using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Models.ClosedCaptions;

namespace YouTubeVideoParser
{
    public class Parser
    {
        public Settings Settings;
        public List<Video> Videos { get; private set; } = new List<Video>();
        public List<Trigger> Triggers { get; private set; } = new List<Trigger>();

        private IEnumerator<Trigger> triggersEnumerator;

        private bool MoveNext()
        {
            if (triggersEnumerator == null)
            {
                triggersEnumerator = Triggers.GetEnumerator();
            }
            return triggersEnumerator.MoveNext();
        }

        public async Task Run()
        {
            if (!IsValid())
                throw new Exception();
            MoveNext();
            foreach (var video in Videos)
            {
                try
                {
                    var captions = await LoadCaptions(video.Id, video.Lang);
                    // TODO: Handle it there!
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        public bool IsValid()
        {
            if (Settings == null || Videos.Count == 0 || Triggers.Count == 0)
                return false;
            foreach (var trigger in Triggers)
            {
                if (!trigger.IsValid()) return false;
            }
            return true;
        }

        private async Task<IReadOnlyList<ClosedCaption>> LoadCaptions(string videoId, string lang)
        {
            var client = new YoutubeClient();
            var track = await client.GetVideoClosedCaptionTrackInfosAsync(videoId);
            var captionTrackInfo = track.FirstOrDefault(info => info.Language.Code == lang);
            var closedCaptionsTrack = await client.GetClosedCaptionTrackAsync(captionTrackInfo);
            return closedCaptionsTrack.Captions;
        }
    }
}
