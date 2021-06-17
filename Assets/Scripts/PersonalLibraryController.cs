using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Observer;
public class PersonalLibraryController : MonoBehaviour
{
    [SerializeField] TMP_InputField _InputField;
    [SerializeField] Button _confirmButton;
    [SerializeField] TextMeshProUGUI _notificationText;

    [HideInInspector]
    public List<VideoComponentDTO> videoComponentDTOs = new List<VideoComponentDTO>();
    // Start is called before the first frame update
    void Start()
    {
        _confirmButton.onClick.AddListener(() =>
        {
            Validator(_InputField.text);
        });
    }

    void Validator(string input)
    {
        foreach (var videoComponentDTO in videoComponentDTOs)
        {
            if (input.Equals(videoComponentDTO.fileName))
            {
                Debug.Log(videoComponentDTO.fileName);
                this.PostEvent(EventID.OnStreamVideo, videoComponentDTO.url);
            }
            else
            {
                Debug.Log(videoComponentDTO.fileName);

                Debug.Log(input);

            }
        }
    }
}
