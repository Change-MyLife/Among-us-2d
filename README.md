# Among Us 모작
포톤 네트워크를 사용하여 멀티플레이 기능을 구현한 어몽어스 게임의 모작입니다.  
// 스프라이트 소스들은 하단의 링크를 참조해주세요.  
# 코드 미리보기
포톤 콜백 함수들 사용방법  
```
    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            RoomOptions roomOption = new RoomOptions();
            roomOption.MaxPlayers = (byte)roomdata.playerCount;
            roomOption.CustomRoomProperties = new Hashtable() { { "imposter", roomdata.imposterCount } };
            ...
    }
```
PunRPC를 이용한 동기화
```
[PunRPC]
    public void setColor(EPlayerColor color)
    {
        playerColor = color;
        if (spriteRender == null)
        {
            spriteRender = GetComponent<SpriteRenderer>();
        }
        spriteRender.material.SetColor("_PlayerColor", PlayerColor.GetColor(playerColor));
    }
    ...
```
OnPhotonSerializeView를 이용한 동기화
```
public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerColor);
            stream.SendNext(playerType);
            ...
    }
```
# 스크린샷
__메인화면__  
![image](https://user-images.githubusercontent.com/65800890/150623548-4ce90283-9f42-48ac-95c6-4e9513ddb2d4.png)  
__로비 화면__  
![image](https://user-images.githubusercontent.com/65800890/150623561-323ab474-bfff-4383-bd2d-eb7e154bbfd7.png)  
# 링크
https://www.spriters-resource.com/pc_computer/amongus/
