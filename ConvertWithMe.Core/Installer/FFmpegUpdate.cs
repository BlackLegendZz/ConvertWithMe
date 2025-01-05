using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;

namespace ConvertWithMe.Core.Installer
{
    public static class FFmpegUpdate
    {
        /// <summary>
        /// Downloads the latest precompiled Version of ffmpeg for Windows x64 and unzips it in the Application folder.
        /// </summary>
        /// <param name="callback">Callback function to update on the download Progress</param>
        /// <returns></returns>
        public static async Task DownloadAndInstallNewestVersion(Action<float, string> callback)
        {
            if (Directory.GetFiles(ApplicationPaths.FFmpeg).Length > 0)
            {
                return;
            }
            float progress = 0f;
            callback?.Invoke(progress, "Fetching ffmpeg from github...");

            string fn = "ffmpeg-7.1-full_build";
            string downloadUrl = $"https://github.com/GyanD/codexffmpeg/releases/download/7.1/{fn}.zip";

            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; VersionChecker/1.0)");
            using HttpResponseMessage response = await client.GetAsync(downloadUrl);
            response.EnsureSuccessStatusCode();

            long totalBytes = response.Content.Headers.ContentLength ?? -1L;
            long totalBytesRead = 0L;
            byte[] buffer = new byte[1024 * 8];
            int bytesRead = 0;
            bool canReportProgress = totalBytes != -1;
            string filePath = Path.Combine(ApplicationPaths.FFmpeg, $"{fn}.zip");

            using (Stream downloadStream = await response.Content.ReadAsStreamAsync())
            {
                using FileStream fs = new FileStream(filePath, FileMode.Create);
                while ((bytesRead = await downloadStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    await fs.WriteAsync(buffer, 0, bytesRead);
                    totalBytesRead += bytesRead;

                    if (canReportProgress)
                    {
                        // Use 90 so that the remaining 10% are for the extraction.
                        progress = 90.0f * totalBytesRead / totalBytes;
                        callback?.Invoke(progress, "Downloading...");
                    }
                }
                callback?.Invoke(progress, "Extracting files...");
            }

            // Unzip only the 3 executables
            using (ZipArchive zArch = ZipFile.OpenRead(filePath))
            {
                string[] executables = ["ffmpeg.exe", "ffprobe.exe", "ffplay.exe"];
                foreach (ZipArchiveEntry entry in zArch.Entries)
                {
                    progress += 10f / zArch.Entries.Count;
                    callback?.Invoke(progress, $"Extracting {entry.Name}...");
                    if (!executables.Contains(entry.Name)) { continue; }

                    entry.ExtractToFile(Path.Combine(ApplicationPaths.FFmpeg, entry.Name), true);
                }
            }

            File.Delete(filePath); //zip is not needed anymore
            callback?.Invoke(100f, "Done!");
        }
    }
}
