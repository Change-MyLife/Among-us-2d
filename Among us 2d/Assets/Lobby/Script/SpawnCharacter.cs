using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class SpawnCharacter : MonoBehaviourPunCallbacks /*IPunObservable*/
{
    public GameObject PlayerPrefab;
    public GameObject[] spawnpoz;

    private int index;

    static public GameObject myPlayer;

    void Awake()
    {
        Spawn();
    }

    void Update()
    {
        
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
    }

    public void Spawn()
    {
        index = Random.Range(0, 5);
        myPlayer = PhotonNetwork.Instantiate(this.PlayerPrefab.name, spawnpoz[index].transform.position, Quaternion.identity);
    }

    /*public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) stream.SendNext(index);
        else index = (int)stream.ReceiveNext();
    }*/
}
