using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Observer;
using ExitGames.Client.Photon.StructWrapping;

public class StreamingVideo : MonoBehaviour
{

    Camera cam;
    UnityEngine.Video.VideoPlayer vid;
    AudioSource aud;

    void Start()
    {
        this.RegisterListener(EventID.OnStreamingVideo, (o) => {
            vid.url = o.Get<ParseHTML_To_DTO>().url;
            vid.Play();
        });

        cam = Camera.main;
        aud = cam.GetComponent<AudioSource>();
        vid = cam.GetComponent<UnityEngine.Video.VideoPlayer>();
        vid.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        vid.audioOutputMode = VideoAudioOutputMode.AudioSource;
        vid.EnableAudioTrack(0, true);
        vid.SetTargetAudioSource(0, aud);
        vid.Prepare();
    }

    //void Update()
    //{
    //    Debug.Log(vid.isPlaying);
    //}

    public void PlayMovie()
    {
        vid.Play();
    }
}