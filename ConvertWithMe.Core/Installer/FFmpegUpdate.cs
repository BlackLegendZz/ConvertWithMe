using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConvertWithMe.Core.Installer
{
    public static class FFmpegUpdate
    {
        public static async Task<bool> IsNewestVersionAsync()
        {
            return DateTime.Compare(await GetCurrentPublishDateAsync(), await GetNewestPublishDateAsync()) >= 0;
        }

        private static async Task<DateTime> GetNewestPublishDateAsync()
        {
            string url = "https://api.github.com/repos/BtbN/FFmpeg-Builds/releases/latest";
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; VersionChecker/1.0)");

            string response = await client.GetStringAsync(url);

            JsonDocument responseJson = JsonDocument.Parse(response);
            DateTime dtRelease = responseJson.RootElement.GetProperty("published_at").GetDateTime();
            return dtRelease;
        }

        private static async Task<DateTime> GetCurrentPublishDateAsync()
        {
            string versionFile = Path.Combine(ApplicationPaths.FFmpeg, "version.txt");
            DateTime dt = DateTime.MinValue;

            if (!File.Exists(versionFile))
            {
                return dt;
            }

            string f = await File.ReadAllTextAsync(versionFile);
            bool success = DateTime.TryParse(f, out dt);

            if (!success)
            {
                //TODO logging
            }

            return dt;
        }

        /// <summary>
        /// Downloads the latest precompiled Version of ffmpeg for Windows x64 and unzips it in the Application folder.
        /// </summary>
        /// <param name="callback">Callback function to update on the download Progress</param>
        /// <returns></returns>
        public static async Task DownloadAndInstallNewestVersion(Action<float> callback)
        {
            string fn = "ffmpeg-master-latest-win64-gpl";
            string downloadUrl = $"https://github.com/BtbN/FFmpeg-Builds/releases/download/latest/{fn}.zip";

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
                        float progress = 100.0f * totalBytesRead / totalBytes;
                        callback?.Invoke(progress);
                    }
                }
            }

            // Unzip only the 3 executables
            using (ZipArchive zArch = ZipFile.OpenRead(filePath))
            {
                string[] executables = ["ffmpeg.exe", "ffprobe.exe", "ffplay.exe"];
                foreach (ZipArchiveEntry entry in zArch.Entries)
                {
                    if (!executables.Contains(entry.Name)) { continue; }

                    entry.ExtractToFile(Path.Combine(ApplicationPaths.FFmpeg, entry.Name), true);
                }
            }

            await UpdateVersion();
            File.Delete(filePath); //zip is not needed anymore
        }

        private static async Task UpdateVersion()
        {
            DateTime dtNew = await GetNewestPublishDateAsync();
            await File.WriteAllTextAsync(Path.Combine(ApplicationPaths.FFmpeg, "version.txt"), dtNew.ToString());
        }
    }
}
