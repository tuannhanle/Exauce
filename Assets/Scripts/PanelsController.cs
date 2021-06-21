using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Observer;
using ExitGames.Client.Photon.StructWrapping;

[System.Serializable]
public class PanelContainer
{
    public Transform panel = default;
    public EventID eventID;
}
public class PanelsController : MonoBehaviour
{
    [SerializeField] private GameObject _caterogyPrefab, _videoPrefab;
    [SerializeField] private PanelContainer _CatogoryPanel, _PersonalLibraryPanel, _VideoPanel;

    private PersonalLibraryController personalLibraryController;

    List<GameObject> videoList = new List<GameObject>();
    List<GameObject> panelList = new List<GameObject>();
    List<GameObject> personalList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

        //PERSONAL CASE
        personalLibraryController = _PersonalLibraryPanel.panel.GetComponent<PersonalLibraryController>();

        // public caterogy
        _CatogoryPanel.panel = transform.Find(_CatogoryPanel.panel.name + "/Content/List");
        this.RegisterListener(_CatogoryPanel.eventID, (o) =>
        {
            RecycleList(panelList);
            panelList = GenerateList(o.Get<List<ParseHTML_To_DTO>>(), VideoComponentType.Caterogy, _caterogyPrefab, _CatogoryPanel.panel);

        });

        // personal Library
        _PersonalLibraryPanel.panel = transform.Find(_PersonalLibraryPanel.panel.name + "/Content/List");
        this.RegisterListener(_PersonalLibraryPanel.eventID, (o) =>
        {
            RecycleList(personalList);
            personalList = GenerateList(o.Get<List<ParseHTML_To_DTO>>(), VideoComponentType.Video, _videoPrefab, _PersonalLibraryPanel.panel);

            personalLibraryController.videoComponentDTOs = o.Get<List<ParseHTML_To_DTO>>();

        });

        //hidden video panel
        _VideoPanel.panel = transform.Find(_VideoPanel.panel.name + "/Content/List");
        this.RegisterListener(_VideoPanel.eventID, (o) =>
        {
            RecycleList(videoList);
            videoList = GenerateList(o.Get<List<ParseHTML_To_DTO>>(), VideoComponentType.Video, _videoPrefab, _VideoPanel.panel);

        });
    }

    void RecycleList(List<GameObject> list)
    {
        foreach (var item in list)
        {
            Destroy(item.gameObject);
        }
        list.Clear();
    }

    List<GameObject> GenerateList(List<ParseHTML_To_DTO> videoComponentDataList, VideoComponentType buttonBoxType, GameObject prefab, Transform locateTransform)
    {
        var gameObjectList = new List<GameObject>();
        foreach (var videoComponentData in videoComponentDataList)
        {
            var videoComponent = Instantiate<GameObject>(prefab, locateTransform);
            videoComponent.GetComponent<VideoComponentUI>().Import(videoComponentData);
            videoComponent.GetComponent<VideoComponentUI>().InitType(buttonBoxType);
            videoComponent.GetComponent<VideoComponentUI>().BindingInternalData();
            videoComponent.GetComponent<VideoComponentUI>().CheckPersonalLibraryThenSelfDestroy();


            gameObjectList.Add(videoComponent);
        }


        return gameObjectList;

    }

    //void CopyValues<T>(T from, T to)
    //{
    //    var json = JsonUtility.ToJson(from);
    //    JsonUtility.FromJsonOverwrite(json, to);
    //}
}
