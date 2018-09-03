using System;
using System.Linq;
using YoutubeExplode;
using YouTubeVideoParser;

namespace ToeflLessonsParser
{
    class Program
    {
#if DEBUG
        private static void Main(string[] args)
        {
            var videoCode = "UCZqh6VE-OYFz2RCWDbZQOqw";

            Console.ReadLine();
        }
#else
        private static void Main(string[] args)
        {

            Console.ReadLine();
        }
#endif
        private static async void Parse(string videoCode)
        {
            var parser = new Parser();
            parser.Settings = new Settings(){ScreenShotsPath = "someTestPath", VideosPath = "someTestPath2"};
            parser.Videos.Add(new Video("TEST_VIDEO_ID", "en"));
            var trigger = new Trigger()
                .Credential(x => ((string)x).Contains(""))
                .UntilNext()
                .Write(y => y.ToString());
            parser.Triggers.Add(trigger);
            await parser.Run();

            /*
             * === Example 1 ===
             * var parser = Parser();
             * parser.Settings = new ParserSettings() { ScreenShotsPath = "PathForScreenShots", VideoPath = "VideoPath"}
             * parser.Videos = new Video("SOME_VIDEO_ID")
             * parser.Triggers.Add(new Trigger()
             *              .Credential(x => x.Text.Contains("Some caption text"))
             *              .While(x => x.Delay < 6f))
             *              .Write(x => x.Text)
             * parser.Run();
             */

            /*
            * === Example 2 ===
            * var parser = Parser();
            * parser.Settings = new ParserSettings() { ScreenShotsPath = "PathForScreenShots", VideoPath = "VideoPath"}
            * parser.Videos = new Video("SOME_VIDEO_ID")
            * parser.Triggers.Add(new Trigger()
            *              .Credential(x => x.Text.Contains("Some caption text"))
            *              .UntilNext()
            *              .Write(x => x.Text)
            * parser.Run();
            */

            /*
            * === Example 3 ===
            * var parser = Parser();
            * parser.Settings = new ParserSettings() { ScreenShotsPath = "PathForScreenShots", VideoPath = "VideoPath"}
            * parser.Videos = new Video("SOME_VIDEO_ID")
            * parser.Triggers.Add(new Trigger()
            *              .Credential(x => x.Text.Contains("Some caption text"))
            *              .UntilNext()
            *              .// Some while every 5 seconds action.
            * parser.Run();
            */

            /*
             * var parser = new YouTubeParser();
             * parser.Settings = new TouTubeParserSettings() {ScreenShotsPath="SomePath", VideoPath="VideoPath"};
             * 
             */
        }

        /*public bool GetVideoThumbnail(string path, string saveThumbnailTo, int seconds)
        {
            string parameters = $"-ss {seconds} -i {path} -f image2 -vframes 1 -y {saveThumbnailTo}";

            var processInfo = new ProcessStartInfo();
            processInfo.FileName = pathToConvertor;
            processInfo.Arguments = parameters;
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;

            File.Delete(saveThumbnailTo);

            using (var process = new Process())
            {
                process.StartInfo = processInfo;
                process.Start();
                process.WaitForExit();
            }

            return File.Exists(saveThumbnailTo);
        }*/

        private static void Test()
        {
            var client = new YoutubeClient();
            client.GetChannelUploadsAsync("UCZqh6VE-OYFz2RCWDbZQOqw").ContinueWith(task =>
            {
                var video = task.Result.FirstOrDefault(v => v.Title.Contains("listening practice test"));

                client.GetVideoAsync("").ContinueWith(t =>
                {
                    //t.Result.
                    //t.Result.
                });

                client.GetVideoClosedCaptionTrackInfosAsync(video.Id).ContinueWith(task1 =>
                {
                    //task1.Result[0].
                    var captionTrackInfo = task1.Result.FirstOrDefault(info => info.Language.Code == "en");
                    client.GetClosedCaptionTrackAsync(captionTrackInfo).ContinueWith(task2 =>
                    {
                        var captionTrack = task2.Result;
                        foreach (var captionTrackCaption in captionTrack.Captions)
                        {
                            Console.WriteLine($"{captionTrackCaption.Offset}: {captionTrackCaption.Text}");
                        }
                    });
                });
            });
        }

    }
}
