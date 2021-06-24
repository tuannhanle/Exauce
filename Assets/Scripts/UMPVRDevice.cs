using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UMPVRDevice : MonoBehaviour
{
    [SerializeField] UniversalMediaPlayer universalMediaPlayer;
    [SerializeField] MeshRenderer _meshRenderer;

    public void Prepare(string url)
    {
        universalMediaPlayer.Volume = 0;
        _meshRenderer.enabled = false;
        universalMediaPlayer.Path = url;
        universalMediaPlayer.Prepare();

        //SendEvent.SendOnlyEvent(MasterClientEventCode.OnMasterPlayVideo);
    }
    public void Play()
    {
        universalMediaPlayer.Volume = 1;
        _meshRenderer.enabled = true;
        universalMediaPlayer.Play();
    }
    public void Stop()
    {
        universalMediaPlayer.Stop();
    }
    public void Pause()
    {
        universalMediaPlayer.Pause();
    }
    public void Exit()
    {
        universalMediaPlayer.Volume = 0;
        _meshRenderer.enabled = false;
        universalMediaPlayer.Stop();

    }
}
