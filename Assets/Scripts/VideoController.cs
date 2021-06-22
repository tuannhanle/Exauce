using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(AudioSource), typeof(VideoPlayer))]
public class VideoController : MonoBehaviour
{
    Camera cam;
    UnityEngine.Video.VideoPlayer _videoPlayer;
    AudioSource _audioSource;
    MeshRenderer _meshRenderer;
    [SerializeField] bool isTesting;
    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _audioSource = GetComponent<AudioSource>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        
        cam = Camera.main;

        SetUpVideoPlayer();
        SetUpAudioSource();

        //var DTO = DataLogger.instance.DataLogged as ParseHTML_To_DTO;
        //PlayStreaming(DTO.url);

        if (isTesting)
        {
            PlayStreaming("https://data.globalvision.ch/APP/GV/Exauce/D%c3%a9tente/Lama%20Tanz.mp4");
        }
    }
    private void Update()
    {
        this.transform.position = cam.transform.position;
    }
    private void SetUpAudioSource()
    {
        _audioSource.playOnAwake = false;
        _audioSource.loop = false;
    }

    void SetUpVideoPlayer()
    {
        _videoPlayer.playOnAwake = false;
        _videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.RenderTexture;
        _videoPlayer.targetCamera = cam;
        _videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        _videoPlayer.EnableAudioTrack(0, true);
        _videoPlayer.SetTargetAudioSource(0, _audioSource);
        _videoPlayer.Prepare();
    }
    public void PlayStreaming(string link)
    {
        _audioSource.volume = 0;
        _meshRenderer.enabled=false;
        _videoPlayer.url = link;
        _videoPlayer.Play();
        _videoPlayer.Stop();
        //SendEvent.SendOnlyEvent(MasterClientEventCode.OnMasterPlayVideo);
    }
    public void Play()
    {
        _audioSource.volume = 1;
        _meshRenderer.enabled = true;
        _videoPlayer.Play();
    }
    public void Stop()
    {
        _videoPlayer.Stop();
    }
    public void Pause()
    {
        _videoPlayer.Pause();
    }
    public void Exit()
    {
        _audioSource.volume = 0;
        _meshRenderer.enabled = false;
        _videoPlayer.Stop();

    }
}
