using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class SpawnGameChar : MonoBehaviourPunCallbacks
{
    public GameObject PlayerPrefab;
    public GameObject[] spawnpoz;

    // 생성하려는 플레이어 색상
    public EPlayerColor SpawnColor;

    // 플레이어
    static public GameObject myPlayer;

    static public GameObject[] m_player;

    public bool[] checkColor = new bool[12];

    void Start()
    {
        Spawn();
    }

    // 캐릭터 생성
    public void Spawn()
    {
        int index = PhotonNetwork.CurrentRoom.PlayerCount - 1;

        myPlayer = PhotonNetwork.Instantiate(this.PlayerPrefab.name, transform.position, Quaternion.identity);

        myPlayer.transform.localScale = index < 5 ? new Vector3(0.35f, 0.35f, 1f) : new Vector3(-0.35f, 0.35f, 1f);
        myPlayer.GetComponent<PhotonView>().RPC("setColor", RpcTarget.AllBuffered, (EPlayerColor)Random.Range(0, 12));
    }

    [ContextMenu("정보")]
    void Info()
    {
        if (PhotonNetwork.InRoom)
        {

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
