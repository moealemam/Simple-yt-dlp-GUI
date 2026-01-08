using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace yt_dlp_GUI
{
    public partial class MainWindow : Form
    {
        private CancellationTokenSource? _cts;
        private static readonly Regex ProgressRegex = new(@"\b(\d{1,3}(?:\.\d+)?)%\b", RegexOptions.Compiled);

        public MainWindow()
        {
            InitializeComponent();
            comboSubtitle.SelectedIndex = 0; // default to None

            // Load saved values
            try
            {
                textBoxYtDlpDir.Text = Properties.Settings.Default.YtDlpDir ?? string.Empty;
                textBoxDownloadDir.Text = Properties.Settings.Default.DownloadDir ?? string.Empty;
                textBoxUrl.Text = Properties.Settings.Default.LastUrl ?? string.Empty;
            }
            catch { }

            this.FormClosing += (_, __) => SaveSettings();
        }

        private void SaveSettings()
        {
            try
            {
                Properties.Settings.Default.YtDlpDir = textBoxYtDlpDir.Text;
                Properties.Settings.Default.DownloadDir = textBoxDownloadDir.Text;
                Properties.Settings.Default.LastUrl = textBoxUrl.Text;
                Properties.Settings.Default.Save();
            }
            catch { }
        }

        private string GetYtDlpExePath()
        {
            var dir = textBoxYtDlpDir.Text.Trim().Trim('"');
            if (string.IsNullOrWhiteSpace(dir)) return string.Empty;
            var candidate = Path.Combine(dir, "yt-dlp.exe");
            return candidate;
        }

        private string GetDownloadDirectory()
        {
            var dir = textBoxDownloadDir.Text.Trim().Trim('"');
            return dir;
        }

        private static ProcessStartInfo CreateCmdProcessStartInfo(string exePath, string arguments)
        {
            // Build: cmd /C ""C:\\path\\yt-dlp.exe" <arguments>"
            var cmd = Environment.GetEnvironmentVariable("COMSPEC");
            if (string.IsNullOrWhiteSpace(cmd)) cmd = "cmd.exe";

            var exeQuoted = '"' + exePath + '"';
            var argsPart = string.IsNullOrWhiteSpace(arguments) ? string.Empty : " " + arguments;
            var full = exeQuoted + argsPart;
            var wrapped = '"' + full + '"';

            return new ProcessStartInfo
            {
                FileName = cmd!,
                Arguments = "/C " + wrapped,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                CreateNoWindow = true,
                WorkingDirectory = Path.GetDirectoryName(exePath) ?? Environment.CurrentDirectory
            };
        }

        private static string? FindFfmpegDir(string ytDlpExePath)
        {
            try
            {
                var baseDir = Path.GetDirectoryName(ytDlpExePath);
                if (!string.IsNullOrEmpty(baseDir))
                {
                    if (File.Exists(Path.Combine(baseDir, "ffmpeg.exe"))) return baseDir;
                }
                var path = Environment.GetEnvironmentVariable("PATH") ?? string.Empty;
                foreach (var p in path.Split(Path.PathSeparator, StringSplitOptions.RemoveEmptyEntries))
                {
                    var d = p.Trim();
                    try { if (File.Exists(Path.Combine(d, "ffmpeg.exe"))) return d; } catch { }
                }
            }
            catch { }
            return null;
        }

        private string BuildCommonArgs(string exePath)
        {
            var sb = new StringBuilder();
            sb.Append("--newline ");
            var ffmpegDir = FindFfmpegDir(exePath);
            if (!string.IsNullOrEmpty(ffmpegDir))
            {
                sb.Append($"--ffmpeg-location \"{ffmpegDir}\" ");
            }
            else
            {
                Log("ffmpeg.exe not found on PATH or next to yt-dlp.exe. For merged MP4, add ffmpeg.exe next to yt-dlp.exe or to PATH.");
            }
            return sb.ToString();
        }

        private void Log(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(() => Log(message));
                return;
            }
            richTextLog.AppendText(message + Environment.NewLine);
            richTextLog.ScrollToCaret();
        }

        private void buttonBrowseYtDlpDir_Click(object? sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog
            {
                Description = "Select folder containing yt-dlp.exe",
                UseDescriptionForTitle = true,
                ShowNewFolderButton = false
            };
            if (Directory.Exists(textBoxYtDlpDir.Text))
            {
                fbd.SelectedPath = textBoxYtDlpDir.Text;
            }
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                textBoxYtDlpDir.Text = fbd.SelectedPath;
                SaveSettings();
            }
        }

        private async void buttonGetSRTs_Click(object sender, EventArgs e)
        {
            var exePath = GetYtDlpExePath();
            var url = textBoxUrl.Text.Trim();

            if (!File.Exists(exePath))
            {
                MessageBox.Show("yt-dlp.exe not found. Please provide a valid directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("Please enter a video URL.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                SaveSettings();
                SetUiEnabled(false);
                Log($"Listing subtitles for: {url}");
                var args = BuildCommonArgs(exePath) + $"--list-subs \"{url}\"";
                var output = await RunProcessCaptureOutputAsync(exePath, args, default);
                var languages = ParseSubtitleLanguages(output);

                comboSubtitle.BeginUpdate();
                comboSubtitle.Items.Clear();
                comboSubtitle.Items.Add("None");
                foreach (var lang in languages)
                {
                    comboSubtitle.Items.Add(lang);
                }
                comboSubtitle.SelectedIndex = 0;
                comboSubtitle.EndUpdate();

                Log(languages.Count == 0 ? "No subtitles available." : $"Found {languages.Count} subtitle languages.");
            }
            catch (Exception ex)
            {
                Log("Error listing subtitles: " + ex.Message);
            }
            finally
            {
                SetUiEnabled(true);
            }
        }

        private static List<string> ParseSubtitleLanguages(string output)
        {
            var set = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            using var reader = new StringReader(output);
            string? line;
            bool tableStarted = false;
            while ((line = reader.ReadLine()) != null)
            {
                if (!tableStarted)
                {
                    if (line.Contains("Available subtitles for", StringComparison.OrdinalIgnoreCase))
                    {
                        tableStarted = true;
                    }
                    continue;
                }

                if (string.IsNullOrWhiteSpace(line)) continue;
                if (line.StartsWith("Language", StringComparison.OrdinalIgnoreCase)) continue;
                if (line.StartsWith("ID", StringComparison.OrdinalIgnoreCase)) continue;
                if (line.TrimStart().StartsWith("-")) continue;

                var firstToken = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(firstToken))
                {
                    set.Add(firstToken.Trim());
                }
            }
            return set.ToList();
        }

        private async void buttonDownloadSubtitle_Click(object sender, EventArgs e)
        {
            var exePath = GetYtDlpExePath();
            var url = textBoxUrl.Text.Trim();
            var outDir = GetDownloadDirectory();

            if (!File.Exists(exePath))
            {
                MessageBox.Show("yt-dlp.exe not found. Please provide a valid directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("Please enter a video URL.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var selectedSub = comboSubtitle.SelectedItem as string;
            if (string.IsNullOrWhiteSpace(selectedSub) || string.Equals(selectedSub, "None", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Please select a subtitle language.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!string.IsNullOrWhiteSpace(outDir) && !Directory.Exists(outDir))
            {
                MessageBox.Show("The selected download directory does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var common = BuildCommonArgs(exePath);
            var sb = new StringBuilder();
            sb.Append(common);
            sb.Append("--skip-download ");
            sb.Append("--write-sub ");
            sb.Append($"--sub-lang {selectedSub} ");
            sb.Append("--convert-subs srt ");
            if (!string.IsNullOrWhiteSpace(outDir))
            {
                sb.Append($"-o \"{Path.Combine(outDir, "%(title)s.%(ext)s")}\" ");
            }
            sb.Append($"\"{url}\"");
            var args = sb.ToString();

            try
            {
                SetUiEnabled(false);
                progressBar.Value = 0;
                richTextLog.Clear();
                Log("Starting subtitle download...");

                _cts = new CancellationTokenSource();
                var exit1 = await RunProcessStreamOutputAsync(exePath, args, _cts.Token);
                if (exit1 == 0) Log("Subtitle download finished.");
                else Log($"Subtitle download finished with exit code {exit1}.");
            }
            catch (OperationCanceledException)
            {
                Log("Subtitle download canceled.");
            }
            catch (Exception ex)
            {
                Log("Error during subtitle download: " + ex.Message);
            }
            finally
            {
                SetUiEnabled(true);
                _cts?.Dispose();
                _cts = null;
            }
        }

        private async void buttonDownload_Click(object sender, EventArgs e)
        {
            var exePath = GetYtDlpExePath();
            var url = textBoxUrl.Text.Trim();
            var outDir = GetDownloadDirectory();

            if (!File.Exists(exePath))
            {
                MessageBox.Show("yt-dlp.exe not found. Please provide a valid directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("Please enter a video URL.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!string.IsNullOrWhiteSpace(outDir) && !Directory.Exists(outDir))
            {
                MessageBox.Show("The selected download directory does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var common = BuildCommonArgs(exePath);
            var sb = new StringBuilder();
            sb.Append(common);
            sb.Append("-f \"bestvideo+bestaudio\" ");
            sb.Append("--merge-output-format mp4 ");
            if (!string.IsNullOrWhiteSpace(outDir))
            {
                sb.Append($"-o \"{Path.Combine(outDir, "%(title)s.%(ext)s")}\" ");
            }
            sb.Append($"\"{url}\"");
            var args = sb.ToString();

            try
            {
                SetUiEnabled(false);
                progressBar.Value = 0;
                richTextLog.Clear();
                Log("Starting download...");

                _cts = new CancellationTokenSource();
                var exit = await RunProcessStreamOutputAsync(exePath, args, _cts.Token);
                Log(exit == 0 ? "Download finished." : $"Download failed with exit code {exit}.");
            }
            catch (OperationCanceledException)
            {
                Log("Download canceled.");
            }
            catch (Exception ex)
            {
                Log("Error during download: " + ex.Message);
            }
            finally
            {
                SetUiEnabled(true);
                _cts?.Dispose();
                _cts = null;
            }
        }

        private void buttonBrowseDownloadDir_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog
            {
                Description = "Select download folder",
                UseDescriptionForTitle = true,
                ShowNewFolderButton = true
            };
            if (Directory.Exists(textBoxDownloadDir.Text))
            {
                fbd.SelectedPath = textBoxDownloadDir.Text;
            }
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                textBoxDownloadDir.Text = fbd.SelectedPath;
            }
        }

        private void SetUiEnabled(bool enabled)
        {
            if (InvokeRequired)
            {
                BeginInvoke(() => SetUiEnabled(enabled));
                return;
            }
            textBoxYtDlpDir.Enabled = enabled;
            textBoxUrl.Enabled = enabled;
            textBoxDownloadDir.Enabled = enabled;
            buttonBrowseDownloadDir.Enabled = enabled;
            buttonGetSRTs.Enabled = enabled;
            comboSubtitle.Enabled = enabled;
            buttonDownload.Enabled = enabled;
            buttonDownloadSubtitle.Enabled = enabled;
        }

        private async Task<string> RunProcessCaptureOutputAsync(string exePath, string arguments, CancellationToken ct)
        {
            var psi = CreateCmdProcessStartInfo(exePath, arguments);

            var outputBuilder = new StringBuilder();

            using var process = new Process { StartInfo = psi, EnableRaisingEvents = true };
            var tcs = new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously);

            process.OutputDataReceived += (_, e) =>
            {
                if (e.Data != null)
                {
                    lock (outputBuilder)
                    {
                        outputBuilder.AppendLine(e.Data);
                    }
                    Log(e.Data);
                }
            };
            process.ErrorDataReceived += (_, e) =>
            {
                if (e.Data != null)
                {
                    lock (outputBuilder)
                    {
                        outputBuilder.AppendLine(e.Data);
                    }
                    Log(e.Data);
                }
            };

            process.Exited += (_, __) => tcs.TrySetResult(process.ExitCode);

            if (!process.Start())
                throw new InvalidOperationException("Failed to start process.");

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            using var reg = ct.Register(() =>
            {
                try { if (!process.HasExited) process.Kill(true); } catch { }
            });

            await tcs.Task.ConfigureAwait(false);

            return outputBuilder.ToString();
        }

        private async Task<int> RunProcessStreamOutputAsync(string exePath, string arguments, CancellationToken ct)
        {
            var psi = CreateCmdProcessStartInfo(exePath, arguments);

            using var process = new Process { StartInfo = psi, EnableRaisingEvents = true };
            var tcs = new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously);

            process.OutputDataReceived += (_, e) =>
            {
                if (e.Data != null)
                {
                    HandleProcessLine(e.Data);
                }
            };
            process.ErrorDataReceived += (_, e) =>
            {
                if (e.Data != null)
                {
                    HandleProcessLine(e.Data);
                }
            };

            process.Exited += (_, __) => tcs.TrySetResult(process.ExitCode);

            if (!process.Start())
            {
                throw new InvalidOperationException("Failed to start process.");
            }

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            using var reg = ct.Register(() =>
            {
                try { if (!process.HasExited) process.Kill(true); } catch { }
            });

            var exitCode = await tcs.Task.ConfigureAwait(false);

            if (exitCode == 0)
            {
                UpdateProgress(100);
            }

            return exitCode;
        }

        private void HandleProcessLine(string line)
        {
            Log(line);
            var m = ProgressRegex.Match(line);
            if (m.Success)
            {
                if (double.TryParse(m.Groups[1].Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var percent))
                {
                    UpdateProgress((int)Math.Max(0, Math.Min(100, Math.Round(percent))));
                }
            }
        }

        private void UpdateProgress(int value)
        {
            if (InvokeRequired)
            {
                BeginInvoke(() => UpdateProgress(value));
                return;
            }
            progressBar.Value = Math.Max(progressBar.Minimum, Math.Min(progressBar.Maximum, value));
        }
    }
}
