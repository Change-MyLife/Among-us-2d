using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class SpawnCharacter : MonoBehaviourPunCallbacks /*, IPunObservable*/
{
    public GameObject PlayerPrefab;
    public GameObject[] spawnpoz;

    private int index;

    PhotonView PV;

    //public GameObject myPlayer;
    static public List<GameObject> myPlayer = new List<GameObject>();

    void Awake()
    {
        Spawn();
        PV = GetComponent<PhotonView>();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
    }

    public void Spawn()
    {
        index = Random.Range(0, 5);
        myPlayer.Add(PhotonNetwork.Instantiate(this.PlayerPrefab.name, spawnpoz[index].transform.position, Quaternion.identity));
        
        myPlayer[0].GetComponent<SpriteRenderer>().material.SetColor("_PlayerColor", PlayerColor.GetColor((EPlayerColor)0));
        //myPlayer.GetComponent<SpriteRenderer>().material.SetColor("_PlayerColor", PlayerColor.GetColor((EPlayerColor)PhotonNetwork.CurrentRoom.PlayerCount - 1));
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

    /*public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(myPlayer);
        }
        else
        {
            myPlayer = (GameObject)stream.ReceiveNext();
        }
    }*/
}
