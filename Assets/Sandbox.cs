using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking;
using System;
using System.IO;
using UnityEngine.Video;

public class Sandbox : MonoBehaviour
{
    [SerializeField] UnityEngine.Video.VideoPlayer _videoPlayer;
    [SerializeField] AudioSource _audioSource;

    void Start()
    {
        _videoPlayer.playOnAwake = false;
        _videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        _videoPlayer.EnableAudioTrack(0, true);
        _videoPlayer.SetTargetAudioSource(0, _audioSource);

        string fileName = "success.mp4";
        string destinationFile = Path.Combine(Application.persistentDataPath, fileName);
        Debug.Log("SAVE HERE: " + destinationFile);
        if (File.Exists(destinationFile))
        {
            Debug.Log("File already downloaded");
            return;
        }

        var downloads = BackgroundDownload.backgroundDownloads;
        if (downloads.Length > 0)
            StartCoroutine(WaitForDownload(downloads[0], 
                onDownloaded:() => { Debug.Log("Start download"); }));
        else
        {
            Uri url = new Uri("https://data.globalvision.ch/APP/GV/Exauce/D%c3%a9couverte/Kh%c3%a1m%20ph%c3%a1%2015%20b%e1%ba%a5t%20ng%e1%bb%9d%20c%c3%b9ng%20LOL%20Hairgoals.mp4");
            StartCoroutine(WaitForDownload(BackgroundDownload.Start(url, fileName),
                onDownloaded: () => SaveExistVideoToGallery(destinationFile, "Exauce", fileName)));
        }
    }
    private IEnumerator SaveExistVideoToGallery(string existingMediaPath, string album, string filename)
    {
        yield return new WaitForEndOfFrame();

        NativeGallery.Permission permission = NativeGallery.SaveVideoToGallery(existingMediaPath, album, filename, (success, path) => Debug.Log("Media save result: " + success + " " + path));
        Debug.Log("Permission result: " + permission);

    }


    IEnumerator WaitForDownload(BackgroundDownload download, Action onDownloaded)
    {
        yield return download;
        if (download.status == BackgroundDownloadStatus.Done)
        {
            Debug.Log("File successfully downloaded");
            onDownloaded?.Invoke();
        }
        else
            Debug.Log("File download failed with error: " + download.error);
    }

    IEnumerator StartDownload()
    {
        using (var download = BackgroundDownload.Start(new Uri("https://mysite.com/file"), "files/file.data"))
        {
            yield return download;
            if (download.status == BackgroundDownloadStatus.Failed)
                Debug.Log(download.error);
            else
                Debug.Log("DONE downloading file");
        }
    }
    IEnumerator ResumeDownload()
    {
        if (BackgroundDownload.backgroundDownloads.Length == 0)
            yield break;
        var download = BackgroundDownload.backgroundDownloads[0];
        yield return download;
        // deal with results here
        // dispose download
        download.Dispose();
    }
}
