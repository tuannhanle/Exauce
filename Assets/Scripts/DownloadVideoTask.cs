using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Networking;
using UnityEngine;

public class DownloadVideoTask : MonoBehaviour
{
    public void DownloadTask(Uri url, string fileName, Action<bool> callback)
    {
        var lastDownloads = BackgroundDownload.backgroundDownloads;
        if (lastDownloads.Length > 0)
        {
            Debug.Log("Dispose previous DOWNLOAD");
            foreach (var download in lastDownloads)
            {

                download.Dispose();
                Debug.Log("Last DOWNLOAD Length: " + lastDownloads.Length);

            }

        }
        StartDownload(url, fileName,(isDone) => callback.Invoke(isDone));

    }
    void StartDownload(Uri uri, string fileName,Action<bool> callback)
    {
        Debug.Log("No previous DOWNLOAD");
        var download = BackgroundDownload.Start(uri, fileName);
        StartCoroutine(Coro_StartDownload(download));
        StartCoroutine(GetProgress(download,(isDone) => callback.Invoke(isDone)));
    }
    IEnumerator GetProgress(BackgroundDownload download, Action<bool> callback)
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log(download.status + " : " + download.progress * 100 + " %");
            if (download.status == BackgroundDownloadStatus.Done)
            {
                Debug.Log("File successfully downloaded: ");
                callback.Invoke(true);
                download.Dispose();
                break;
            }
            if (download.status == BackgroundDownloadStatus.Failed)
            {
                callback.Invoke(false);

                Debug.Log("File download failed with error: " + download.error);
                break;
            }
            else
            {
                callback.Invoke(true);

            }

        }
    }
    IEnumerator Coro_StartDownload(BackgroundDownload download)
    {
        Debug.Log("Start download...");
        yield return download;

    }
}
