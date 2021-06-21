using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;
using Core.Utilities;


public class PhotonNetworkManager : MonoBehaviourPunCallbacks
{

    private void Start()
    {
        ConnectToServer();
    }

    private void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connecting to server...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Has connected to server");
        base.OnConnectedToMaster();

        RoomOptions roomOptions = new RoomOptions()
        {
            MaxPlayers = 10,
            IsVisible = true,
            IsOpen = true
        };
        PhotonNetwork.JoinOrCreateRoom("Room 1", roomOptions, TypedLobby.Default);

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A newplayer joined the room");
        base.OnPlayerEnteredRoom(newPlayer);
    }



}
