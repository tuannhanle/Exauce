using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Observer;

[System.Serializable]
public class PanelContainer
{
    public Transform panel = default;
    public EventID eventID;
}
public class PanelsController : MonoBehaviour
{
    [SerializeField] private GameObject _videoPrefab;
    [SerializeField] private PanelContainer _DecouverteContainer, _SportContainer, _DetenteContainer, _ArtContainer, _CultureContainer, _PersonalLibraryContainer;

    private PanelContainer defaultPanel;

    private PersonalLibraryController personalLibraryController;
    private List<PanelContainer> panelContainers = new List<PanelContainer>();

    // Start is called before the first frame update
    void Start()
    {
        defaultPanel = new PanelContainer() {
            panel = _DecouverteContainer.panel,
            eventID= EventID.OnFirstLoad
        };

        //PERSONAL CASE
        //personalLibraryController = _PersonalLibraryContainer.panel.GetComponent<PersonalLibraryController>();
        //panelContainers.AddRange(new List<PanelContainer> { _DecouverteContainer, _SportContainer, _DetenteContainer, _ArtContainer, _CultureContainer, _PersonalLibraryContainer });


        defaultPanel.panel = transform.Find(defaultPanel.panel.name + "/Content/List");
        this.RegisterListener(defaultPanel.eventID, (o) => GenerateList((List<ParseHTML_To_DTO>)o,ButtonBoxType.Folder , _videoPrefab, defaultPanel.panel));

        //foreach (var panelContainer in panelContainers)
        //{
        //    panelContainer.panel = transform.Find(panelContainer.panel.name + "/Content/List");
        //    this.RegisterListener(panelContainer.eventID,
        //         (o) => GenerateList((List<ParseHTML_To_DTO>)o, _videoPrefab, panelContainer.panel));

        //}
    }

    void GenerateList(List<ParseHTML_To_DTO> videoComponentDataList,ButtonBoxType buttonBoxType,GameObject prefab, Transform locateTransform)
    {
        foreach (var videoComponentData in videoComponentDataList)
        {
            var videoComponent = Instantiate<GameObject>(prefab, locateTransform);
            videoComponent.GetComponent<VideoComponentUI>().Import( videoComponentData);
            videoComponent.GetComponent<VideoComponentUI>().InitType(buttonBoxType);
            videoComponent.GetComponent<VideoComponentUI>().BindingInternalData();
        }

        PersonalListException(locateTransform, videoComponentDataList);

    }
    void PersonalListException(Transform locateTransform, List<ParseHTML_To_DTO> videoComponentDataList)
    {
        if (locateTransform.GetInstanceID().Equals(_PersonalLibraryContainer.panel.GetInstanceID()))
        {
            personalLibraryController.videoComponentDTOs = videoComponentDataList;
        }
    }

    void CopyValues<T>(T from, T to)
    {
        var json = JsonUtility.ToJson(from);
        JsonUtility.FromJsonOverwrite(json, to);
    }
}
