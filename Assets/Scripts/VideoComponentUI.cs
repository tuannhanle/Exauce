﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Observer;
using System;
using System.Text;

[System.Serializable]
public class ParseHTML_To_DTO
{
    public string fileName { get; private set; }
    public string url { get; private set; }
    public string dateCreated { get; private set; }
    public string size { get; private set; }

    private ParseHTML_To_DTO() { }

    public ParseHTML_To_DTO(string fileName,string url, string dateCreated, string size)
    {
        this.fileName = Encode(fileName.Trim());
        //this.fileName = fileName.Trim();
        this.url = url.Trim();
        this.dateCreated = dateCreated.Trim();
        this.size = size.Trim();
    }
    public static string Encode(string path)
    {
        byte[] bytes = Encoding.GetEncoding(1252).GetBytes(path);
        return Encoding.UTF8.GetString(bytes);
    }
    public static string Decode(string path)
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
public enum VideoComponentType { Video, Caterogy}
public class VideoComponentUI : MonoBehaviour
{
    public ParseHTML_To_DTO videoComponentDTO { get; set; }
    [SerializeField] private TextMeshProUGUI _fileNameUI;
    [SerializeField] private TextMeshProUGUI _dateCreatedUI;
    [SerializeField] private TextMeshProUGUI _sizeUI;
    [SerializeField] private Button _button;
    private VideoComponentType _buttonBoxType;

    private void OnEnable()
    {

        _button.onClick.AddListener(() => {
            switch (_buttonBoxType)
            {
                case VideoComponentType.Video:
                    //this.PostEvent(EventID.OnStreamingVideo, videoComponentDTO);
                    DataLogger.instance.DataLogged = videoComponentDTO;
                    SendEvent.SendMessageEvent(MasterClientEventCode.OnMasterGoIntoVideo, videoComponentDTO.url);
                    SceneController.instance.LoadVideoScene(SceneType.VideoPlayerScene);
                    //DataLogger.instance.TestCastData<ParseHTML_To_DTO>();
                    break;
                case VideoComponentType.Caterogy:
                    this.PostEvent(EventID.OnDirCaterogy, videoComponentDTO);

                    break;
                default:
                    break;
            }

        });
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }
    public void BindingInternalData()
    {
        _fileNameUI.text = videoComponentDTO.fileName;
        _dateCreatedUI.text = videoComponentDTO.dateCreated;
        _sizeUI.text = videoComponentDTO.size;
    }
    
    internal void CheckPersonalLibraryThenSelfDestroy()
    {
        if (videoComponentDTO.fileName== "PersonalLibrary /")
        {
            Destroy(this.gameObject);
        }
    }

    internal void InitType(VideoComponentType buttonBoxType)
    {
        _buttonBoxType = buttonBoxType;
    }

    internal void Import(ParseHTML_To_DTO videoComponentData)
    {
        videoComponentDTO = videoComponentData;
    }
}
