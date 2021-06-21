using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Observer;
using System.IO;
using System.Text.RegularExpressions;
public class PersonalLibraryController : MonoBehaviour
{
    [SerializeField] TMP_InputField _InputField;
    [SerializeField] Button _confirmButton;
    [SerializeField] TextMeshProUGUI _notificationText;

    [HideInInspector]
    public List<ParseHTML_To_DTO> videoComponentDTOs = new List<ParseHTML_To_DTO>();
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

        var filename = Path.GetFileNameWithoutExtension(input);
        var inputHashCode = filename.GetHashCode();
        Debug.Log("INPUT: " + filename);
        foreach (var videoComponentDTO in videoComponentDTOs)
        {
            var filenameDTO = Path.GetFileNameWithoutExtension(videoComponentDTO.fileName);
            var videoHashCodeDTO = filenameDTO.GetHashCode();

            if (inputHashCode.Equals(videoHashCodeDTO))
            {
                Debug.Log("TRUE : "+videoComponentDTO.fileName);

                DataLogger.instance.DataLogged = videoComponentDTO;
                SendEvent.SendMessageEvent(MasterClientEventCode.OnMasterGoIntoVideo, videoComponentDTO.url);
                SceneController.instance.LoadVideoScene(SceneType.VideoPlayerScene);
            }
        }
    }
}
