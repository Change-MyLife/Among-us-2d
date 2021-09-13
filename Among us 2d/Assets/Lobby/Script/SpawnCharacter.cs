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

    // 플레이어 색상
    [SerializeField]
    private EPlayerColor color;
    
    // 플레이어
    static public GameObject myPlayer;

    static public GameObject[] m_player;

    public bool[] checkColor;

    void Start()
    {
        Spawn();
        GetPlayer();
    }

    // 캐릭터 생성
    public void Spawn()
    {
        int index = Random.Range(0, 5);
        myPlayer = PhotonNetwork.Instantiate(this.PlayerPrefab.name, spawnpoz[index].transform.position, Quaternion.identity);
        myPlayer.GetComponent<PhotonView>().RPC("setColor", RpcTarget.AllBuffered, color);
    }

    // 방에 들어와 있는 플레이어들 가져오기
    void GetPlayer()
    {
        m_player = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in m_player)
        {
            Debug.Log(player.GetComponent<CharacterMove>().playerColor);
        }
    }

    [ContextMenu("정보")]
    void Info()
    {
        if (PhotonNetwork.InRoom)
        {
            GetPlayer();
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
            stream.SendNext(checkColor);
        }
        else
        {
            checkColor = (bool[])stream.ReceiveNext();
        }
    }*/
}
