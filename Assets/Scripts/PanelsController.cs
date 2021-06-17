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

    private List<PanelContainer> panelContainers = new List<PanelContainer>();

    // Start is called before the first frame update
    void Start()
    {
        panelContainers.AddRange(new List<PanelContainer> { _DecouverteContainer, _SportContainer, _DetenteContainer, _ArtContainer, _CultureContainer, _PersonalLibraryContainer });
        foreach (var panelContainer in panelContainers)
        {
            panelContainer.panel = transform.Find(panelContainer.panel.name + "/Content/List");
            this.RegisterListener(panelContainer.eventID,
                 (o) => GenerateList((List<VideoComponentDTO>)o, _videoPrefab, panelContainer.panel));
        }
    }

    void GenerateList(List<VideoComponentDTO> videoComponentDataList, GameObject prefab, Transform locateTransform)
    {
        foreach (var videoComponentData in videoComponentDataList)
        {
            var videoComponent = Instantiate<GameObject>(prefab, locateTransform);
            videoComponent.GetComponent<VideoComponentUI>().videoComponentDTO = videoComponentData;
            videoComponent.GetComponent<VideoComponentUI>().BindingInternalData();
        }

    }

    void CopyValues<T>(T from, T to)
    {
        var json = JsonUtility.ToJson(from);
        JsonUtility.FromJsonOverwrite(json, to);
    }
}
