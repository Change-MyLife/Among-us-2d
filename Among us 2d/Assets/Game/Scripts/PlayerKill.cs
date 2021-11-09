using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerKill : MonoBehaviourPunCallbacks
{
    private CircleCollider2D circleCollider;
    public float killRange;

    [SerializeField] private List<LobbyChar> targetPlayers = new List<LobbyChar>();

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();

        circleCollider.radius = killRange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (GameManager.Instance)
        {
            var player = collision.GetComponent<LobbyChar>();
            if (player && player.playerType == PlayerType.Crew && GameManager.Instance.myChar.playerType == PlayerType.imposter)
            {
                if (!targetPlayers.Contains(player))
                {
                    targetPlayers.Add(player);          // 타겟 플레이어 리스트 추가
                }

                Button killButton = GameManager.Instance.killButton.GetComponent<Button>();
                killButton.interactable = true;
                killButton.GetComponent<Image>().raycastTarget = true;

                killButton.onClick.AddListener(OnClickKillButton);          // 킬 이벤트 활성화
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameManager.Instance)
        {
            var player = collision.GetComponent<LobbyChar>();
            if (targetPlayers.Contains(player))
            {
                targetPlayers.Remove(player);              // 타겟 플레이어 리스트에서 제거
            }

            Button killButton = GameManager.Instance.killButton.GetComponent<Button>();
            killButton.interactable = false;
            killButton.GetComponent<Image>().raycastTarget = false;

            killButton.onClick.RemoveListener(OnClickKillButton);       // 킬 이벤트 비활성화
        }
    }

    public void OnClickKillButton()
    {
        LobbyChar.isMoveable = true;
        LobbyChar nearTarget = null;
        if (photonView.IsMine)
        {
            float dist = float.MaxValue;
            foreach(var target in targetPlayers)
            {
                float newDist = Vector3.Distance(transform.position, target.transform.position);
                if(newDist < dist)
                {
                    dist = newDist;
                    nearTarget = target;
                }
            }

            targetPlayers.Remove(nearTarget);
            nearTarget.GetComponent<PhotonView>().RPC("Death",RpcTarget.All);
            GameManager.Instance.killCool = GameManager.Instance.killCoolTime;
        }
    }
}
