using Xabe.FFmpeg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertWithMe.Core
{
    public class FileConversion
    {

        public FileConversion()
        {
            FFmpeg.SetExecutablesPath(ApplicationPaths.FFmpeg);
        }

        private async Task ConvertWithOptions(string fileSrc, string fileDest)
        {
            string args = $"-i \"{fileSrc}\" \"{fileDest}\"";
            IConversion conversion = FFmpeg.Conversions.New();
            conversion.OnProgress += Conversion_OnProgress;
            //conversion.SetPixelFormat()
            await conversion.Start(args);
        }

        private void Conversion_OnProgress(object sender, Xabe.FFmpeg.Events.ConversionProgressEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
