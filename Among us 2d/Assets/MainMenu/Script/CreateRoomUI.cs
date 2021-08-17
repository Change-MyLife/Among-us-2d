using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class CreateRoomUI : MonoBehaviourPunCallbacks
{
    public enum maps { THESKELD, POLUS, MIRAHQ }

    [SerializeField]
    private List<Image> bannercrew;

    [SerializeField]
    private List<Button> impostors;

    [SerializeField]
    private List<Button> maxplayers;

    public CreateRoomData roomdata;

    public class CreateRoomData
    {
        public int imposterCount;   // 임포스터 수
        public int playerCount;     // 최대 플레이어
        public maps map;            // 맵
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < bannercrew.Count; i++)
        {
            /* material 복사본 */
            Material materialInstance = Instantiate(bannercrew[i].material);
            bannercrew[i].material = materialInstance;
        }

        // roomdata의 초기값
        roomdata = new CreateRoomData() { imposterCount = 1, playerCount = 9 , map = maps.THESKELD};
        UpdateCrewImpostors();
    }

    // 배너 크루원 임포스터 색깔 바꾸기 함수
    void UpdateCrewImpostors()
    {
        int imposterCount = roomdata.imposterCount;
        int playerCount = roomdata.playerCount - 1;

        for (int i = 0; i < bannercrew.Count; i++)
        {
            bannercrew[i].material.SetColor("_PlayerColor", Color.white);
        }

        while (imposterCount != 0)
        {
            int num = Random.Range(0, playerCount);
            if (bannercrew[num].material.GetColor("_PlayerColor") != Color.red)
            {
                bannercrew[num].material.SetColor("_PlayerColor", Color.red);
                imposterCount--;
            }
        }

        for(int i = 0; i < bannercrew.Count; i++){
            if(i < roomdata.playerCount)
            {
                bannercrew[i].gameObject.SetActive(true);
            }
            else
            {
                bannercrew[i].gameObject.SetActive(false);
            }
        }
    }

    // 최대 플레이어 수
    public void UpdateMaxPlayerCount(int playerCount)
    {
        roomdata.playerCount = playerCount;
        ColorBlock cb;

        for(int i = 0; i < maxplayers.Count; i++)
        {
            if (i == playerCount - 4)
            {
                cb = maxplayers[i].colors;
                cb.normalColor = new Color(1f, 1f, 1f, 1f);
                maxplayers[i].colors = cb;
            }
            else
            {
                cb = maxplayers[i].colors;
                cb.normalColor = new Color(1f, 1f, 1f, 0f);
                maxplayers[i].colors = cb;
            }
        }

        UpdateCrewImpostors();
    }

    // 임포스터 수
    public void UpdateImposterCount(int imposterCount)
    {
        roomdata.imposterCount = imposterCount;
        ColorBlock cb;

        for(int i = 0; i < impostors.Count; i++)
        {
            if(i == imposterCount - 1)
            {
                cb = impostors[i].colors;
                cb.normalColor = new Color(1f, 1f, 1f, 1f);
                impostors[i].colors = cb;
            }
            else
            {
                cb = impostors[i].colors;
                cb.normalColor = new Color(1f, 1f, 1f, 0f);
                impostors[i].colors = cb;
            }
        }

        // 제약을 주는 코드 입력!! (미정)

        UpdateCrewImpostors();
    }

    // 포톤 콜백 함수들
    #region MonoBehaviourPunCallbacks CallBacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected.");
        Debug.Log("roomMaxPlayers" + (byte)roomdata.playerCount);
        PhotonNetwork.LocalPlayer.NickName = Setting.nickname;
        PhotonNetwork.CreateRoom("Room", new RoomOptions { MaxPlayers = (byte)roomdata.playerCount });
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("Disconnected reason {0}", cause);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Now this client is in a room");
        PhotonNetwork.LoadLevel("LobbyScene");
    }

    #endregion

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("Connected.");
            Debug.Log("roomMaxPlayers" + (byte)roomdata.playerCount);
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = (byte)roomdata.playerCount });
        }
        else
        {
            Debug.Log("Connecting to server");
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}
