using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Observer;


public class ClientManager : MonoBehaviour
{
    [SerializeField] GameObject watingRoom, video360Room, waitingLabel;

    UMPVRDevice uMPVRDevice;

    private void Awake()
    {
        uMPVRDevice = video360Room.GetComponent<UMPVRDevice>();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.RegisterListener(EventID.OnMasterGoIntoVideo, o => uMPVRDevice.Prepare(o as string));

        this.RegisterListener(EventID.OnMasterPlayVideo, o => {
            uMPVRDevice.Play();
            watingRoom.SetActive(false);
            waitingLabel.SetActive(false);
            SetGyroscope(video360Room.transform, Camera.main.transform);
        });

        this.RegisterListener(EventID.OnMasterPauseVideo, o => uMPVRDevice.Pause());

        this.RegisterListener(EventID.OnMasterStopVideo, o => uMPVRDevice.Stop());

        this.RegisterListener(EventID.OnMasterExitVideo, o => {
            uMPVRDevice.Exit();
            watingRoom.SetActive(true);
            waitingLabel.SetActive(true);

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
