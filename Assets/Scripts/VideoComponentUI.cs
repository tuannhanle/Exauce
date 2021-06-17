using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Observer;

[System.Serializable]
public class VideoComponentDTO
{
    public string fileName { get; private set; }
    public string url { get; private set; }
    public string dateCreated { get; private set; }
    public string size { get; private set; }

    private VideoComponentDTO() { }

    public VideoComponentDTO(string fileName,string url, string dateCreated, string size)
    {
        this.fileName = fileName.Trim();
        this.url = url.Trim();
        this.dateCreated = dateCreated.Trim();
        this.size = size.Trim();
    }

}
public class VideoComponentUI : MonoBehaviour
{
    public VideoComponentDTO videoComponentDTO { get; set; }
    [SerializeField] private TextMeshProUGUI _fileNameUI;
    [SerializeField] private TextMeshProUGUI _dateCreatedUI;
    [SerializeField] private TextMeshProUGUI _sizeUI;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(() => {
            this.PostEvent(EventID.OnStreamVideo, videoComponentDTO.url);
            
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



}
