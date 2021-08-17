using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class OnlineUI : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private InputField nicknameinputField;
    [SerializeField]
    private GameObject createRoomUI;

    public void onClickCreateRoomButton()
    {
        if(nicknameinputField.text != "")
        {
            Setting.nickname = nicknameinputField.text;

            createRoomUI.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            nicknameinputField.GetComponent<Animator>().SetTrigger("on");
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected.");
        PhotonNetwork.LocalPlayer.NickName = Setting.nickname;
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Now this client is in a room");
        PhotonNetwork.LoadLevel("LobbyScene");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed JoinRandom Room is nothing");
    }

    public void FindGameButton()
    {
        if (nicknameinputField.text != "")
        {
            Setting.nickname = nicknameinputField.text;

            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.LocalPlayer.NickName = Setting.nickname;
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                Debug.Log("Connecting to server");
                PhotonNetwork.ConnectUsingSettings();
            }
        }
        else
        {
            nicknameinputField.GetComponent<Animator>().SetTrigger("on");
        }
    }
}
