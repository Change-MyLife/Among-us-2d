using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameManager : MonoBehaviour
{
    // 싱글톤
    public static GameManager Instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }

            return m_instance;
        }
    }

    public static GameManager m_instance;

    // 임포스터 수
    public int imposters;
    public int playerCount;    // 현재 플레이어 수

    public List<GameObject> PlayerList = new List<GameObject>();

    private void Awake()
    {
        // 싱글톤
        if (Instance != this)
        {
            Destroy(gameObject);
        }
        //instance = this;

        // 현재 room의 커스텀프로퍼티를 가져오기
        Hashtable CP = PhotonNetwork.CurrentRoom.CustomProperties;

        // room에 저장되어 있는 임포스터 수
        imposters = (int)CP["imposter"];
        playerCount = PhotonNetwork.CurrentRoom.PlayerCount;    // 현재 플레이어 수 값 가져오기

        // 모든 플레이어 씬 동시 이동 = true
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        StartCoroutine("GameReady");
    }

    public void AddPlayers(GameObject crew)
    {
        // 플레이어 추가
        PlayerList.Add(crew);
    }

    public IEnumerator GameReady()
    {
        // 플레이어 수가 같아질 때 까지 update 한다.
        while (PlayerList.Count != playerCount)
        {
            yield return null;
        }

        ChooseImposters();      // 임포스터 정하는 함수

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(LoadingUI.instance.Loading());      // 로딩 중
    }

    public void ChooseImposters()
    {
        // 임포스터 정하기
        int imp = imposters;
        while (imp > 0)
        {
            int ran = Random.Range(0, playerCount);
            if (PlayerList[ran].GetComponent<LobbyChar>().playerType == PlayerType.Crew)
            {
                PlayerList[ran].GetComponent<LobbyChar>().playerType = PlayerType.imposter;
                imp--;
            }
        }
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
