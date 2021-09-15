using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class SpawnCharacter : MonoBehaviourPunCallbacks/*, IPunObservable*/
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
        /*// 플레이어들 가져오기
        m_player = GameObject.FindGameObjectsWithTag("Player");

        // 플레이어들 색상 파악
        foreach (GameObject player in m_player)
        {
            for (int i = 0; i < checkColor.Length; i++)
            {
                if ((EPlayerColor)i == player.GetComponent<CharacterMove>().playerColor)
                {
                    checkColor[i] = true;
                }
            }
            Debug.Log(player.GetComponent<CharacterMove>().playerColor);
        }*/

        int index = PhotonNetwork.CurrentRoom.PlayerCount - 1;

        myPlayer = PhotonNetwork.Instantiate(this.PlayerPrefab.name, spawnpoz[index].transform.position, Quaternion.identity);

        myPlayer.transform.localScale = index < 5 ? new Vector3(0.5f, 0.5f, 1f) : new Vector3(-0.5f, 0.5f, 1f);
        myPlayer.GetComponent<PhotonView>().RPC("setColor", RpcTarget.AllBuffered, (EPlayerColor)Random.Range(0,12));

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

    // 변수 동기화
    /*public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(SpawnColor);
        }
        else
        {
            SpawnColor = (int)stream.ReceiveNext();
        }
    }*/
}
