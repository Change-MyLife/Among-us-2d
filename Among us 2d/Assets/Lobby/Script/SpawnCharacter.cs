using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class SpawnCharacter : MonoBehaviourPunCallbacks
{
    public GameObject PlayerPrefab;
    public GameObject[] spawnpoz;

    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn()
    {
        PhotonNetwork.Instantiate(this.PlayerPrefab.name, spawnpoz[index++].transform.position, Quaternion.identity);
    }
}
