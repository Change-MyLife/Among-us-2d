using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameManager : MonoBehaviour
{
    // 싱글톤
    public static GameManager Instance;

    // 임포스터 수
    [SerializeField]
    private int imposters;
    private int playerCount;    // 현재 플레이어 수

    public List<GameObject> PlayerList = new List<GameObject>();

    private void Awake()
    {
        // 현재 room의 커스텀프로퍼티를 가져오기
        Hashtable CP = PhotonNetwork.CurrentRoom.CustomProperties;

        // 싱글톤
        Instance = this;

        // room에 저장되어 있는 임포스터 수
        imposters = (int)CP["imposter"];
        playerCount = PhotonNetwork.CurrentRoom.PlayerCount;    // 현재 플레이어 수 값 가져오기

        // 모든 플레이어 씬 동시 이동 = true
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void ChooseImposters(GameObject crew)
    {
        // 플레이어 추가
        PlayerList.Add(crew);

        // 임포스터 정하기
        if (PlayerList.Count == playerCount)
        {
            while (imposters > 0)
            {
                int ran = Random.Range(0, playerCount);
                if(PlayerList[ran].GetComponent<LobbyChar>().playerType == PlayerType.Crew)
                {
                    PlayerList[ran].GetComponent<LobbyChar>().playerType = PlayerType.imposter;
                    imposters--;
                }
            }
        }

        LoadingUI.instance.StartCoroutine("Loading");
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
