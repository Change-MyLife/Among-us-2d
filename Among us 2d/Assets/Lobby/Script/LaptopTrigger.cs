using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LaptopTrigger : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Button UseButton;

    [SerializeField]
    private Sprite[] sprites;

    [SerializeField]
    private GameObject UI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PhotonView>().IsMine)
        {
            UseButton.image.overrideSprite = sprites[1];
            // 커스텀 마이즈 버튼의 활성화
            UseButton.interactable = true;
            UseButton.GetComponent<Image>().raycastTarget = true;

            // 버튼에 이벤트 연결
            UseButton.onClick.AddListener(CustomizeOnClick);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PhotonView>().IsMine)
        {
            UseButton.image.overrideSprite = sprites[0];
            // 커스텀 마이즈 버튼의 비활성화
            UseButton.interactable = false;
            UseButton.GetComponent<Image>().raycastTarget = false;

            // 연결된 이벤트 연결해제
            UseButton.onClick.RemoveListener(CustomizeOnClick);
        }
    }

    public void CustomizeOnClick()
    {
        CharacterMove.isMoveable = false;
        UI.SetActive(true);
    }
}
