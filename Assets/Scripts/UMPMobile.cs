using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UMPMobile : MonoBehaviour
{

    [SerializeField] UniversalMediaPlayer universalMediaPlayer;

    private void Awake()
    {
        var DTO = DataLogger.instance.DataLogged as ParseHTML_To_DTO;
        universalMediaPlayer.Path=DTO.url;
        PlayButton();

    }

    public void PlayButton()
    {
        SendEvent.SendOnlyEvent(MasterClientEventCode.OnMasterPlayVideo);
        universalMediaPlayer.Play();

    }
    public void StopButton()
    {
        SendEvent.SendOnlyEvent(MasterClientEventCode.OnMasterStopVideo);
        universalMediaPlayer.Stop();
    }
    public void PauseButton()
    {
        SendEvent.SendOnlyEvent(MasterClientEventCode.OnMasterPauseVideo);
        universalMediaPlayer.Pause();
    }
    public void BackButton()
    {
        universalMediaPlayer.Stop();
        SendEvent.SendOnlyEvent(MasterClientEventCode.OnMasterExitVideo);
        SceneController.instance.LoadVideoScene(SceneType.MainScene);
    }
}
