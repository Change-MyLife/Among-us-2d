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

    public EPlayerColor playerColor;

    //static public List<GameObject> myPlayer = new List<GameObject>();
    static public GameObject myPlayer;

    void Awake()
    {
        Spawn();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
    }

    public void Spawn()
    {
        int index = Random.Range(0, 5);
        //myPlayer.Add(PhotonNetwork.Instantiate(this.PlayerPrefab.name, spawnpoz[index].transform.position, Quaternion.identity));
        myPlayer = PhotonNetwork.Instantiate(this.PlayerPrefab.name, spawnpoz[index].transform.position, Quaternion.identity);
        myPlayer.GetComponent<SpriteRenderer>().material.SetColor("_PlayerColor", PlayerColor.GetColor((EPlayerColor)Random.Range(0,10)));

        //myPlayer[0].GetComponent<SpriteRenderer>().material.SetColor("_PlayerColor", PlayerColor.GetColor((EPlayerColor)0));
    }

    public void PlayerColorCheck()
    {
        // 현재 방에 들어와있는 플레이어의 색상 체크
        /*for(int i = 0; i < myPlayer.Count; i++)
        {
            //Debug.Log(myPlayer[i].GetComponent<SpriteRenderer>().material.GetColor("_PlayerColor"));
            Debug.Log(i);
        }*/
    }

    [ContextMenu("정보")]
    void Info()
    {
        if (PhotonNetwork.InRoom)
        {
            //print("플레이어들의 색상 : ");
            //PlayerColorCheck();
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
