using Xabe.FFmpeg;
using ConvertWithMe.Core.Definitions;
using Xabe.FFmpeg.Events;
using System.IO;

namespace ConvertWithMe.Core
{
    public class FileConversion
    {

        public FileConversion()
        {
            FFmpeg.SetExecutablesPath(ApplicationPaths.FFmpeg);
        }

        public async Task ConvertToAudio(
            string src, string dest, 
            Definitions.Format format, 
            Definitions.AudioCodec aCodec, 
            int bitrate, int sampleRate, 
            //SampleFormat sFormat, 
            ConversionProgressEventHandler progressCallback,
            CancellationToken cancellationToken
            )
        {
            if (!format.AudioCodecs.Contains(aCodec))
            {
                throw new ArgumentException("Format doesnt support the provided audio codec.");
            }

            if (!aCodec.ValidSampleRates.Contains(sampleRate) && !aCodec.HasVariableSampleRate)
            {
                throw new ArgumentException($"Audio codec only supports the following sample rates: {aCodec.ValidSampleRates}.");
            }

            //if (!aCodec.validSampleFormats.Contains(sFormat))
            //{
            //    throw new ArgumentException("Audio codec doesnt support the provided sample Format.");
            //}

            IMediaInfo mediaInfo = await FFmpeg.GetMediaInfo(src);
            IAudioStream? audioStream = mediaInfo.AudioStreams.FirstOrDefault();

            if (audioStream == null)
            {
                throw new ArgumentNullException($"No audiostreams could be found in {src}.");
            }

            audioStream = audioStream
                .SetCodec(aCodec.Codec)
                .SetSampleRate(sampleRate)
                .SetBitrate(bitrate * 1000); // add the factor again which we previously removed 

            IConversion conversion = FFmpeg.Conversions.New()
                .AddParameter("-hide_banner")
                .AddStream(audioStream)
                .SetOutput($"{dest}.{format.Extension}");

            await startConversionAsync(conversion, dest, progressCallback, cancellationToken);
        }

        public async Task ConvertToVideo(
            string src, string dest, 
            Definitions.Format format, 
            Definitions.VideoCodec vCodec, 
            Definitions.AudioCodec aCodec, 
            int vBitrate, int aBitrate, 
            float frameRate, int sampleRate,
            int width, int height,
            EncodingMode encodingMode, 
            PixelFormat pFormat, 
            //SampleFormat sFormat,
            ConversionPreset quailityPreset,
            ConversionProgressEventHandler progressCallback,
            CancellationToken cancellationToken
            )
        {
            if (!format.AudioCodecs.Contains(aCodec))
            {
                throw new ArgumentException("Format doesnt support the provided audio codec.");
            }

            if (!format.VideoCodecs.Contains(vCodec))
            {
                throw new ArgumentException("Format doesnt support the provided video codec.");
            }

            if (!aCodec.ValidSampleRates.Contains(sampleRate) && !aCodec.HasVariableSampleRate)
            {
                throw new ArgumentException($"Audio codec only supports the following sample rates: {aCodec.ValidSampleRates}.");
            }

            //if (!aCodec.validSampleFormats.Contains(sFormat))
            //{
            //    throw new ArgumentException("Audio codec doesnt support the provided sample Format.");
            //}

            if (encodingMode == EncodingMode.VBR && !vCodec.HasVBRSupport)
            {
                throw new ArgumentException("Video codec doesnt support VBR.");
            }

            if (encodingMode == EncodingMode.VBR && (vBitrate < 0 || vBitrate > 50))
            {
                throw new ArgumentException($"The CRF has to be between 0 and 50. It is {vBitrate}");
            }

            if (!vCodec.ValidPixelFormats.Contains(pFormat))
            {
                throw new ArgumentException("Video Codec doesnt support the provided pixel Format.");
            }

            IMediaInfo mediaInfo = await FFmpeg.GetMediaInfo(src);
            IAudioStream? audioStream = mediaInfo.AudioStreams.FirstOrDefault();
            IVideoStream? videoStream = mediaInfo.VideoStreams.FirstOrDefault();

            if (audioStream == null)
            {
                // Some videos dont have an audio.
                // TODO: Log status and send to UI (somehow lol)
            }
            if (videoStream == null)
            {
                throw new ArgumentNullException($"No videostreams could be found in {src}.");
            }

            audioStream = audioStream?
                .SetCodec(aCodec.Codec)
                .SetSampleRate(sampleRate)
                .SetBitrate(aBitrate * 1000); // add the factor again which we previously removed 

            videoStream = videoStream
                .SetCodec(vCodec.Codec)
                .SetFramerate(frameRate)
                .SetSize(width, height);


            IConversion conversion = FFmpeg.Conversions.New()
                .AddParameter("-hide_banner")
                .AddStream(videoStream)
                .SetPixelFormat(pFormat)
                .SetOutput(dest);

            if (audioStream != null)
            {
                conversion = conversion.AddStream(audioStream);
            }

            switch (encodingMode)
            {
                case EncodingMode.VBR:
                    conversion = conversion.AddParameter($"-crf {vBitrate}", ParameterPosition.PostInput);
                    break;
                case EncodingMode.CBR:
                    conversion = conversion.SetVideoBitrate(vBitrate * 1000000); // add the factor again which we previously removed 
                    break;
            }

            if (vCodec.HasQualitySupport)
            {
                conversion = conversion.SetPreset(quailityPreset);
            }

            await startConversionAsync(conversion, dest, progressCallback, cancellationToken);
        }

        private async Task startConversionAsync(IConversion conversion, string dest, ConversionProgressEventHandler progressCallback, CancellationToken cancellationToken)
        {
            conversion.OnProgress += progressCallback;
            try
            {
                IConversionResult result = await conversion.Start(cancellationToken);
            }
            catch (Xabe.FFmpeg.Exceptions.ConversionException e)
            {
                //TODO
            }
            catch (OperationCanceledException)
            {
                if (File.Exists(dest))
                {
                    // File is still being used by ffmpeg
                    // Wait for a while and then delete it without interrupting the current thread.
                    await Task.Run(() =>
                    {
                        Thread.Sleep(3000);
                        File.Delete(dest);
                    });
                }
            }
            finally
            {
                conversion.OnProgress -= progressCallback;
            }
        }
    }
}
