using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LoadingUI : MonoBehaviour
{
    public static LoadingUI instance;

    [SerializeField]
    private GameObject shhhhUI;

    [SerializeField]
    private GameObject crewUI;

    [SerializeField]
    private GameObject impostersUI;

    [SerializeField]
    private GameObject playerUI;

    public IEnumerator Loading()
    {
        shhhhUI.SetActive(true);
        yield return new WaitForSeconds(3f);
        shhhhUI.SetActive(false);

        PlayerTypeLoadingUI();
        yield return new WaitForSeconds(3f);
        crewUI.SetActive(false);
        impostersUI.SetActive(false);
        playerUI.SetActive(true);
    }

    public void PlayerTypeLoadingUI()
    {
        foreach(var v in GameManager.Instance.PlayerList)
        {
            if(v.transform.GetComponent<LobbyChar>().playerType == PlayerType.Crew && v.GetComponent<PhotonView>().IsMine)
            {
                crewUI.SetActive(true);
            }
            else if(v.transform.GetComponent<LobbyChar>().playerType == PlayerType.imposter && v.GetComponent<PhotonView>().IsMine)
            {
                impostersUI.SetActive(true);
            }
        }
    }

    void Awake()
    {
        instance = this;
    }
}
