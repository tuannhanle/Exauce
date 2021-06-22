using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Observer;


public class ClientManager : MonoBehaviour
{
    [SerializeField] GameObject watingRoom, video360Room;

    VideoController videoController;

    private void Awake()
    {
        videoController = video360Room.GetComponent<VideoController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.RegisterListener(EventID.OnMasterGoIntoVideo, o => videoController.PlayStreaming(o as string));

        this.RegisterListener(EventID.OnMasterPlayVideo, o => videoController.Play());
        this.RegisterListener(EventID.OnMasterPlayVideo, o => watingRoom.SetActive(false));

        this.RegisterListener(EventID.OnMasterPauseVideo, o => videoController.Pause());

        this.RegisterListener(EventID.OnMasterStopVideo, o => videoController.Stop());

        this.RegisterListener(EventID.OnMasterExitVideo, o => videoController.Exit());
        this.RegisterListener(EventID.OnMasterExitVideo, o => watingRoom.SetActive(true));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
