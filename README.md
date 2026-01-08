# yt-dlp GUI (Windows)

A minimal Windows Forms GUI for yt-dlp. Download the best quality video or selected subtitles from YouTube using an easy interface.
<img width="802" height="482" alt="image" src="https://github.com/user-attachments/assets/2c8e9ac3-3fe9-4ae1-b445-149d6a717b2d" />

## Features
- Download best video + audio merged to MP4
- Download subtitles only (SRT) for a selected language
- Show live progress and logs
- Remember last used yt-dlp folder, download folder, and URL
- Browse for folders easily

## Requirements
- Windows 10/11
- .NET 8 Desktop Runtime
- yt-dlp
- ffmpeg

## Installation
1. Download and install the .NET 8 Desktop Runtime from Microsoft.
2. Download yt-dlp and ffmpeg:
   - yt-dlp: https://github.com/yt-dlp/yt-dlp/releases
   - ffmpeg (Windows builds): https://www.gyan.dev/ffmpeg/builds/ or https://www.ffmpeg.org/download.html
3. Place `yt-dlp.exe` in the same folder as `ffmpeg.exe`.
   - Example: `C:\Tools\yt-dlp` containing `yt-dlp.exe`, `ffmpeg.exe`, `ffprobe.exe`.
4. Build the app or download the `Simple yt-dlp GUI.exe` from the releases section and run it.

## Usage
1. Set the `yt-dlp` directory (use Browse) to the folder containing `yt-dlp.exe` and `ffmpeg.exe`.
2. Paste a video URL.
3. Optionally set a download folder.
4. Click `Get available SRTs` to list subtitle languages.
5. Choose one of:
   - `Download video` to fetch best video+audio merged to MP4
   - `Download subtitle` to fetch only SRT for the selected language

## Notes
- If downloads fail with 403 or missing URLs, update yt-dlp (`yt-dlp -U`).
- For region/auth restricted videos, consider passing browser cookies (future UI option) or run yt-dlp directly with `--cookies-from-browser chrome`.
- The app runs yt-dlp via `cmd.exe` for compatibility and streams logs to the UI.

## License
This project is provided as-is. Check yt-dlp and ffmpeg licenses on their respective sites.
