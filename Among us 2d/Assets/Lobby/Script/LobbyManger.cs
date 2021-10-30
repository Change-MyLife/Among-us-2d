using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LobbyManger : MonoBehaviourPunCallbacks
{
    public static LobbyManger Instance;

    [SerializeField]
    private Text PlayerCountText;

    [SerializeField]
    private int imposters;

    [SerializeField]
    private GameObject startbutton;

    private void Awake()
    {
        // 방 임포스터 설정 값 가져오기
        Hashtable CP = PhotonNetwork.CurrentRoom.CustomProperties;

        Instance = this;
        imposters = (int)CP["imposter"];

        // 모든 플레이어 씬 동시 이동 = true
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    [PunRPC]
    public void UpdatePlayerCount()
    {
        var maxplayer = PhotonNetwork.CurrentRoom.MaxPlayers;           // 최대 인원수
        var currentplayer = PhotonNetwork.CurrentRoom.PlayerCount;      // 현재 인원수
        var minplayer = imposters == 1 ? 4 : imposters == 2 ? 7 : 9;

        if(currentplayer < minplayer)
        {
            PlayerCountText.color = Color.red;
        }
        else
        {
            PlayerCountText.color = Color.white;
        }

        PlayerCountText.text = currentplayer.ToString() + " / " + maxplayer.ToString();
    }

    [PunRPC]
    void SetStartButton()
    {
        var maxplayer = PhotonNetwork.CurrentRoom.MaxPlayers;           // 최대 인원수
        var currentplayer = PhotonNetwork.CurrentRoom.PlayerCount;      // 현재 인원수
        var minplayer = imposters == 1 ? 4 : imposters == 2 ? 7 : 9;

        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            startbutton.SetActive(true);

            startbutton.GetComponent<Image>().raycastTarget = true;
            startbutton.GetComponent<Button>().interactable = true;
            /*if(currentplayer >= minplayer)
            {
                startbutton.GetComponent<Image>().raycastTarget = true;
                startbutton.GetComponent<Button>().interactable = true;
            }
            else
            {
                startbutton.GetComponent<Image>().raycastTarget = false;
                startbutton.GetComponent<Button>().interactable = false;
            }*/
        }
        else
        {
            startbutton.SetActive(false);
        }
    }

    public void OnClickStartButton()
    {
        Debug.Log("Game Start");
        //MoveToGameScene();
        PhotonNetwork.IsMessageQueueRunning = false;
        PhotonNetwork.LoadLevel(2);
    }
}
