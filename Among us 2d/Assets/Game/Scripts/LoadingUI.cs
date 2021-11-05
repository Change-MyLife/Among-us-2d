using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class LoadingUI : MonoBehaviourPunCallbacks
{
    // 싱글톤
    public static LoadingUI instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<LoadingUI>();
            }

            return m_instance;
        }
    }

    public static LoadingUI m_instance;

    [SerializeField]
    private GameObject shhhhUI;

    [SerializeField]
    private GameObject crewUI;

    [SerializeField]
    private GameObject impostersUI;

    [SerializeField]
    private GameObject playerUI;

    [SerializeField]
    private Text crewUiText;
    [SerializeField]
    private Text imposterUiText;

    [SerializeField]
    private GameObject[] crews;
    [SerializeField]
    private GameObject[] imposters;

    void Awake()
    {
        // 싱글톤
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator Loading()
    {
        shhhhUI.SetActive(true);
        yield return new WaitForSeconds(2f);
        shhhhUI.SetActive(false);

        SetLoadingUI();
        yield return new WaitForSeconds(1f);
        PlayerTypeLoadingUI();      // 각 플레이어의 타입에 따른 로딩UI
        yield return new WaitForSeconds(2f);

        // LoadingUI를 닫는다.
        gameObject.SetActive(false);

        // 플레이어 타입에 따라 UI를 다르게 한다
        playerUI.SetActive(true);
    }

    public void SetLoadingUI()
    {
        int CountImp = GameManager.Instance.imposters;       // 임포스터 수
        int Countcrew = GameManager.Instance.playerCount;

        int i = 0;
        while (CountImp > 0)
        {
            imposters[i].SetActive(true);
            i++;
            CountImp--;
        }

        i = 0;
        while (Countcrew > 0)
        {
            crews[i].SetActive(true);
            i++;
            Countcrew--;
        }
    }

    public void PlayerTypeLoadingUI()
    {
        foreach (var v in GameManager.Instance.PlayerList)
        {
            if (v.GetComponent<PhotonView>().IsMine)
            {
                if (v.transform.GetComponent<LobbyChar>().playerType == PlayerType.Crew)
                {
                    crewUiText.text = "임포스터가 " + GameManager.Instance.imposters + "명 남았습니다.";
                    crewUI.SetActive(true);
                }
                else
                {
                    imposterUiText.text = v.GetComponent<LobbyChar>().nickname + "님은 임포스터 입니다.";
                    impostersUI.SetActive(true);
                }
            }
        }
    }
}
