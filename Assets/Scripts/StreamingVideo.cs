using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Observer;
using ExitGames.Client.Photon.StructWrapping;
using System;

[RequireComponent(typeof(AudioSource), typeof(VideoPlayer))]
public class StreamingVideo : MonoBehaviour
{

    Camera cam;
    UnityEngine.Video.VideoPlayer _videoPlayer;
    AudioSource _audioSource;
    [SerializeField] bool isTesting;

    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        cam = Camera.main;

        SetUpVideoPlayer();
        SetUpAudioSource();



        if (isTesting)
        {
            PlayStreaming("https://data.globalvision.ch/APP/GV/Exauce/D%c3%a9couverte/Kh%c3%a1m%20ph%c3%a1%2015%20b%e1%ba%a5t%20ng%e1%bb%9d%20c%c3%b9ng%20LOL%20Hairgoals.mp4");
        }
        else
        {
            var DTO = DataLogger.instance.DataLogged as ParseHTML_To_DTO;
            PlayStreaming(DTO.url);
        }

        //this.RegisterListener(EventID.OnStreamingVideo, (o) => {
        //     var DTO = o as ParseHTML_To_DTO;
        //    PlayStreaming(DTO.url);

        //});



    }

    private void SetUpAudioSource()
    {
        _audioSource.playOnAwake = false;
        _audioSource.loop = false;
    }

    void SetUpVideoPlayer()
    {
        _videoPlayer.playOnAwake = false;
        _videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        _videoPlayer.targetCamera = cam;
        _videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        _videoPlayer.EnableAudioTrack(0, true);
        _videoPlayer.SetTargetAudioSource(0, _audioSource);
        _videoPlayer.Prepare();
    }

    //void Update()
    //{
    //    Debug.Log(vid.isPlaying);
    //}

    private void PlayStreaming(string link)
    {
        _videoPlayer.url = link;
        _videoPlayer.Prepare();
        _videoPlayer.Play();
        SendEvent.SendOnlyEvent(MasterClientEventCode.OnMasterPlayVideo);
    }

    public void PlayButton()
    {
        SendEvent.SendOnlyEvent(MasterClientEventCode.OnMasterPlayVideo);
        _videoPlayer.Play();

    }
    public void StopButton()
    {
        SendEvent.SendOnlyEvent(MasterClientEventCode.OnMasterStopVideo);
        _videoPlayer.Stop();
    }
    public void PauseButton()
    {
        SendEvent.SendOnlyEvent(MasterClientEventCode.OnMasterPauseVideo);
        _videoPlayer.Pause();
    }
    public void BackButton()
    {
        _videoPlayer.Stop();
        SendEvent.SendOnlyEvent(MasterClientEventCode.OnMasterExitVideo);
        SceneController.instance.LoadVideoScene(SceneType.MainScene);
    }
}