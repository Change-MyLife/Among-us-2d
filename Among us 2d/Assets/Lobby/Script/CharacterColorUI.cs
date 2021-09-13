using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CharacterColorUI : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Image CrewImage;
    [SerializeField]
    private Button[] ColorButtons;

    int PrevNumber = -1;

    /*private void OnEnable()
    {
        // 캐릭터 정지
        CharacterMove.isMoveable = false;
    }*/

    void Start()
    {
        var inst = Instantiate(CrewImage.material);
        CrewImage.material = inst;
    }

    public void Close()
    {
        // 캐릭터 이동
        CharacterMove.isMoveable = true;
        gameObject.SetActive(false);
    }

    public void OnColorButton(int Number)
    {
        // 선택가능한 색상?
        if (!ColorButtons[Number].transform.GetChild(0).gameObject.activeSelf && SpawnCharacter.myPlayer.GetComponent<PhotonView>().IsMine)
        {
            // 자신의 캐릭터 색깔 변경
            if (SpawnCharacter.myPlayer.GetComponent<PhotonView>().IsMine)
            {
                SpawnCharacter.myPlayer.GetComponent<PhotonView>().RPC("setColor", RpcTarget.AllBuffered, (EPlayerColor)Number);
            }

            // UI 캐릭터의 색상 변경
            CrewImage.GetComponent<Image>().material.SetColor("_PlayerColor", PlayerColor.GetColor((EPlayerColor)Number));

            // 다른사람은 선택 못하게 객체 활성화
            ColorButtons[Number].transform.GetChild(0).gameObject.SetActive(true);

            // 이전의 선택했던 색상 버튼 비활성화
            if (!(PrevNumber == -1))
            {
                ColorButtons[PrevNumber].transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        PrevNumber = Number;
    }
}
