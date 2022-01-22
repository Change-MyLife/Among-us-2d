# Among Us 모작
포톤 네트워크를 사용하여 멀티플레이 기능을 구현한 어몽어스 게임의 모작입니다.  
// 스프라이트 소스들은 하단의 링크를 참조해주세요.  
# 코드 미리보기
```
public class OnlineUI : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private InputField nicknameinputField;
    [SerializeField]
    private GameObject createRoomUI;

    public void onClickCreateRoomButton()
    {
        if(nicknameinputField.text != "")
        {
            Setting.nickname = nicknameinputField.text;

            createRoomUI.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            nicknameinputField.GetComponent<Animator>().SetTrigger("on");
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected.");
        PhotonNetwork.LocalPlayer.NickName = Setting.nickname;
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Now this client is in a room");
        PhotonNetwork.LoadLevel("LobbyScene");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed JoinRandom Room is nothing");
    }

    public void FindGameButton()
    {
        if (nicknameinputField.text != "")
        {
            Setting.nickname = nicknameinputField.text;

            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.LocalPlayer.NickName = Setting.nickname;
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                Debug.Log("Connecting to server");
                PhotonNetwork.ConnectUsingSettings();
            }
        }
        else
        {
            nicknameinputField.GetComponent<Animator>().SetTrigger("on");
        }
    }
}
```
# 스크린샷
__메인화면__  
![image](https://user-images.githubusercontent.com/65800890/150623548-4ce90283-9f42-48ac-95c6-4e9513ddb2d4.png)  
__로비 화면__  
![image](https://user-images.githubusercontent.com/65800890/150623561-323ab474-bfff-4383-bd2d-eb7e154bbfd7.png)  
# 링크
https://www.spriters-resource.com/pc_computer/amongus/
