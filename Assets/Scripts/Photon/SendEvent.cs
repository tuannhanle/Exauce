using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
public enum MasterClientEventCode : byte { OnMasterGoIntoVideo, OnMasterPlayVideo, OnMasterStopVideo, OnMasterPauseVideo, OnMasterExitVideo }

public class SendEvent
{
    public static void SendMessageEvent(MasterClientEventCode eventCode,object content )
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; 
        PhotonNetwork.RaiseEvent((byte)eventCode, content, raiseEventOptions, SendOptions.SendReliable);
    }
    public static void SendOnlyEvent(MasterClientEventCode eventCode)
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        PhotonNetwork.RaiseEvent((byte)eventCode,"" , raiseEventOptions, SendOptions.SendReliable);
    }
}
