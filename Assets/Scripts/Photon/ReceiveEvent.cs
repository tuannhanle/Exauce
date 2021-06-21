using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using WebSocketSharp;
using Core.Utilities;

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
        //byte eventCode = photonEvent.Code;
        //if (eventCode == (byte)MasterClientEventCode.OnMasterGotVideo)
        //{
        //    var msg = photonEvent.CustomData as string;
        //    Debug.Log(msg);

        //}
    }
}
