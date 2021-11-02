using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private int imposters;

    private void Awake()
    {
        // 방 임포스터 설정 값 가져오기
        Hashtable CP = PhotonNetwork.CurrentRoom.CustomProperties;

        Instance = this;
        imposters = (int)CP["imposter"];

        // 모든 플레이어 씬 동시 이동 = true
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    [ContextMenu("정보")]
    void Info()
    {
        if (PhotonNetwork.InRoom)
        {
            Debug.Log("==============방 정보==============");
            Debug.Log("임포스터 수는 : " + imposters);
            print("현재 방 인원수 : " + PhotonNetwork.CurrentRoom.PlayerCount);
            print("현재 방 최대인원수 : " + PhotonNetwork.CurrentRoom.MaxPlayers);

            string playerStr = "방에 있는 플레이어 목록 : ";
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                playerStr += PhotonNetwork.PlayerList[i].NickName + ", ";
            }
            print(playerStr);
        }
    }
}
