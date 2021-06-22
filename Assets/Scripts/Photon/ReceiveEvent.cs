using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using WebSocketSharp;
using Core.Utilities;
using Observer;
public class ReceiveEvent : Singleton<ReceiveEvent>, IOnEventCallback
{
    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
        if (eventCode == (byte)MasterClientEventCode.OnMasterGoIntoVideo)
        {
            var url = photonEvent.CustomData as string;

            Debug.Log("OnMasterGoIntoVideo");
            Debug.Log(url);

            this.PostEvent(EventID.OnMasterGoIntoVideo, url);


        }
        if (eventCode == (byte)MasterClientEventCode.OnMasterPlayVideo)
        {
            Debug.Log("OnMasterPlayVideo");
            this.PostEvent(EventID.OnMasterPlayVideo);

        }
        if (eventCode == (byte)MasterClientEventCode.OnMasterPauseVideo)
        {
            Debug.Log("OnMasterPauseVideo");
            this.PostEvent(EventID.OnMasterPauseVideo);

        }
        if (eventCode == (byte)MasterClientEventCode.OnMasterStopVideo)
        {
            Debug.Log("OnMasterStopVideo");
            this.PostEvent(EventID.OnMasterStopVideo);

        }
        if (eventCode == (byte)MasterClientEventCode.OnMasterExitVideo)
        {
            Debug.Log("OnMasterExitVideo");
            this.PostEvent(EventID.OnMasterExitVideo);

        }
    }
}
