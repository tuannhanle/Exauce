using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Observer;
using System;
using System.Text;
using Unity.Networking;
using System.Text.RegularExpressions;

[System.Serializable]
public class ParseHTML_To_DTO
{
    public string fileName { get; private set; }
    public string url { get; private set; }
    public string dateCreated { get; private set; }
    public string size { get; private set; }

    private ParseHTML_To_DTO() { }

    public ParseHTML_To_DTO(string fileName, string url, string dateCreated, string size)
    {
        this.fileName = RemoveInvalidChars(Encode(fileName.Trim()));
        //this.fileName = fileName.Trim();
        this.url = url.Trim();
        this.dateCreated = dateCreated.Trim();
        this.size = size.Trim();
    }
    public string RemoveInvalidChars(string filename)
    {
        return string.Concat(filename.Split(Path.GetInvalidFileNameChars()));
    }

    private static string Encode(string path)
    {
        byte[] bytes = Encoding.GetEncoding(1252).GetBytes(path);
        return Encoding.UTF8.GetString(bytes);
    }
    private static string Decode(string path)
    {
        Char[] chars;
        Byte[] bytes = Encoding.UTF8.GetBytes(path);

        Decoder utf8Decoder = Encoding.UTF8.GetDecoder();

        int charCount = utf8Decoder.GetCharCount(bytes, 0, bytes.Length);
        chars = new Char[charCount];
        int charsDecodedCount = utf8Decoder.GetChars(bytes, 0, bytes.Length, chars, 0);
        return new string(chars);
    }
}
public enum VideoComponentType { Video, Caterogy }

public class VideoComponentUI : MonoBehaviour
{
    private DownloadVideoTask downloadVideoTask;
    public ParseHTML_To_DTO videoComponentDTO { get; set; }
    [SerializeField] private TextMeshProUGUI _fileNameUI;
    [SerializeField] private TextMeshProUGUI _dateCreatedUI;
    [SerializeField] private TextMeshProUGUI _sizeUI;
    [SerializeField] private Button _watchStreamButton,_watchLocalButton, _downloadMasterButton, _downloadClientButton;

    [SerializeField] private VideoComponentType _buttonBoxType;

    private Uri url;
    private string fileName;
    private string destinationFile;

    private void Awake()
    {
        if (_buttonBoxType == VideoComponentType.Video)

            downloadVideoTask = this.gameObject.AddComponent<DownloadVideoTask>();
    }
    private void OnEnable()
    {
        if (_buttonBoxType == VideoComponentType.Video)
        {
            _downloadMasterButton?.onClick?.AddListener(() =>
            {
                _downloadMasterButton.interactable = false;
                downloadVideoTask.DownloadTask(url, fileName, (isDone) => _downloadMasterButton.interactable = !isDone);

            });
            _watchStreamButton?.onClick?.AddListener(() =>
            {
                DataLogger.instance.DataLogged = videoComponentDTO;
                DataLogger.instance.videoPath = ""; //
                SendEvent.SendMessageEvent(MasterClientEventCode.OnMasterGoIntoVideo, videoComponentDTO.url);
                SceneController.instance.LoadVideoScene(SceneType.VideoPlayerScene);
            });

        }
        else
        {
            _watchStreamButton?.onClick?.AddListener(() => this.PostEvent(EventID.OnDirCaterogy, videoComponentDTO));

        }

    }

    private void OnDisable()
    {
        if (_buttonBoxType == VideoComponentType.Video)
        {
            _downloadMasterButton.onClick.RemoveAllListeners();
            _watchStreamButton.onClick.RemoveAllListeners();

        }

    }
    internal void CheckExist()
    {
        if (_buttonBoxType == VideoComponentType.Video)

            if (File.Exists(this.destinationFile))
            {
                Debug.Log("File already downloaded");
                Debug.Log(destinationFile);

                _downloadMasterButton.interactable = false;
                return;
            }
            else
            {
                _downloadMasterButton.interactable = true;

            }





    }

    internal void BindingInternalData()
    {
        _fileNameUI.text = videoComponentDTO.fileName;
        _dateCreatedUI.text = videoComponentDTO.dateCreated;
        _sizeUI.text = videoComponentDTO.size;
    }

    internal void CheckPersonalLibraryThenSelfDestroy()
    {
        if (videoComponentDTO.fileName == "PersonalLibrary ")
        {
            Destroy(this.gameObject);
        }
    }

    //internal void InitType(VideoComponentType buttonBoxType)
    //{
    //    _buttonBoxType = buttonBoxType;
    //}

    internal void Import(ParseHTML_To_DTO videoComponentData)
    {
        videoComponentDTO = videoComponentData;
        fileName = videoComponentDTO.fileName + ".mp4"; //check file name chua mp4
        url = new Uri(videoComponentDTO.url);
        destinationFile = Path.Combine(Application.persistentDataPath, fileName);
    }
}
