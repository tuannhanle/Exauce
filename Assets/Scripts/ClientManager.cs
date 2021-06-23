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

        this.RegisterListener(EventID.OnMasterPlayVideo, o => {
            videoController.Play();
            watingRoom.SetActive(false);
            SetGyroscope(video360Room.transform, Camera.main.transform);
        });

        this.RegisterListener(EventID.OnMasterPauseVideo, o => videoController.Pause());

        this.RegisterListener(EventID.OnMasterStopVideo, o => videoController.Stop());

        this.RegisterListener(EventID.OnMasterExitVideo, o => { 
            videoController.Exit();
            watingRoom.SetActive(true);
        });


    }

    // obsolete
    private void ResetGyroscope(Transform target)
    {
        target.rotation = new Quaternion();
    }

    private void SetGyroscope(Transform target, Transform refTransform)
    {
        target.rotation = refTransform.rotation;
    }
}
