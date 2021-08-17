using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class LobbyScript : MonoBehaviourPunCallbacks
{
    #region Private Serializable Fields
    [SerializeField]
    private byte maxPlayersPerRoom = 6;

    #endregion

    #region Private Fields

    string gameVersion = "1";


    #endregion

    #region MonoBehaviourPunCallbacks CallBacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected.");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("Disconnected reason {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed JoinRandom so i create room");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Now this client is in a room");

        GotoLobbyScene();
    }

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    #endregion

    #region Public Methods
    public void Connect()
    {
        Debug.Log("Connecting to server");
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }


    #endregion

    void GotoLobbyScene()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
