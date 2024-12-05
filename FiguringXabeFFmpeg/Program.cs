using Xabe.FFmpeg;

FFmpeg.SetExecutablesPath(@"E:\ffmpeg\bin");

string videoIN = @"D:\Jan\Videos\Polarlicht.mkv";
string videoOUT = @"D:\Jan\Videos\testScript.mp4";


IMediaInfo mediaInfo = await FFmpeg.GetMediaInfo(videoIN);
IVideoStream? videoStream = mediaInfo.VideoStreams.FirstOrDefault();
IAudioStream? audioStream = mediaInfo.AudioStreams.FirstOrDefault();

if (videoStream == null || audioStream == null)
{
    throw new Exception("Yeet!");
}

//videoStream = videoStream.CopyStream();
videoStream = videoStream.SetCodec(VideoCodec.hevc);

IConversion conversion = FFmpeg.Conversions.New()
    .AddStream((IStream)videoStream, (IStream)audioStream)
    .SetOutput(videoOUT);

IConversionResult result = await conversion.Start();

Console.WriteLine(result.Arguments);