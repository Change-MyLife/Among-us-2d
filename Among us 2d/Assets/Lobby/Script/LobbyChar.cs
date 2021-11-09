using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public enum PlayerType
{
    Crew,
    imposter
}

public class LobbyChar : MonoBehaviourPunCallbacks, IPunObservable
{
    private Animator animator;

    static public bool isMoveable;
    public float speed = 2f;

    private SpriteRenderer spriteRender;

    // 플레이어 색상
    public EPlayerColor playerColor;

    [SerializeField]
    private Text nicknameText;
    // 플레이어 닉네임
    public string nickname;
    // 플레이어 사이즈
    private float playerSize = 0.5f;

    // 플레이어의 타입 (임포스터 or 크루원)
    public PlayerType playerType = PlayerType.Crew;

    [SerializeField] private Button KillButton;

    public void Start()
    {
        // 씬 이동 후에도 캐릭터 보존
        DontDestroyOnLoad(this.gameObject);

        // 플레이어 색상 설정
        spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.material.SetColor("_PlayerColor", PlayerColor.GetColor(playerColor));

        isMoveable = true;
        animator = GetComponent<Animator>();

        if (photonView.IsMine)
        {
            Camera cam = Camera.main;
            cam.transform.SetParent(transform);
            cam.transform.localPosition = new Vector3(0f, 0f, -10f);
            cam.orthographicSize = 2.5f;
        }

        // 플레이어 닉네임 설정
        photonView.RPC("SetNickName", RpcTarget.AllBuffered);

        // LobbyManger RPC
        if (LobbyManger.Instance != null)
        {
            LobbyManger.Instance.GetComponent<PhotonView>().RPC("UpdatePlayerCount", RpcTarget.All);
            LobbyManger.Instance.GetComponent<PhotonView>().RPC("SetStartButton", RpcTarget.All);
        }
    }

    // 씬 이동 후 호출
    private void OnLevelWasLoaded(int level)
    {
        // 게임 씬 불러오고 호출
        if(level == 2)
        {
            // 플레이어 설정 초기화
            isMoveable = true;
            playerSize = 0.3f;
            speed = 1.5f;

            transform.localScale = new Vector3(playerSize, playerSize, 1f);

            if (photonView.IsMine)
            {
                // 카메라를 삭제 후 메인카메라를 다시 설정한다.
                Destroy(transform.Find("Main Camera").gameObject);
                Camera cam = Camera.main;
                cam.transform.SetParent(transform);
                cam.transform.localPosition = new Vector3(0f, 0f, -10f);
                cam.orthographicSize = 1.3f;
            }

            // GameManager로 플레이어 추가
            GameManager.m_instance.AddPlayers(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        // LobbyManger RPC
        if (LobbyManger.Instance != null)
        {
            LobbyManger.Instance.GetComponent<PhotonView>().RPC("UpdatePlayerCount", RpcTarget.All);
            LobbyManger.Instance.GetComponent<PhotonView>().RPC("SetStartButton", RpcTarget.All);
        }
    }

    void Update()
    {
        // UI 버튼을 클릭시 캐릭터 움직임 x
        if ((Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject()))
        {
            isMoveable = false;
        }

        if (photonView.IsMine && isMoveable)
        {
            bool isMove = false;
            if (Setting.controlType == EcontrolType.KeyboardMouse)
            {
                Vector3 dir = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f), 1f);
                if (dir.x < 0f) transform.localScale = new Vector3(-playerSize, playerSize, 1f);
                else if (dir.x > 0f) transform.localScale = new Vector3(playerSize, playerSize, 1f);
                transform.position += dir * speed * Time.deltaTime;
                isMove = dir.magnitude != 0f;
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f)).normalized;
                    if (dir.x < 0f) transform.localScale = new Vector3(-playerSize, playerSize, 1f);
                    else if (dir.x > 0f) transform.localScale = new Vector3(playerSize, playerSize, 1f);
                    transform.position += dir * speed * Time.deltaTime;
                    isMove = dir.magnitude != 0f;
                }
            }
            // 애니메이터 파라미터
            animator.SetBool("IsMove", isMove);
        }

        if (transform.localScale.x < 0)
        {
            nicknameText.transform.localScale = new Vector3(-0.3f, 0.3f, 1f);
        }
        else if (transform.localScale.x > 0)
        {
            nicknameText.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
        }
    }

    [PunRPC]
    public void SetNickName()
    {
        nickname = photonView.Owner.NickName;
        nicknameText.text = nickname;
    }

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

    [PunRPC]
    public void setPoz(Vector3 poz)
    {
        transform.position = poz;
    }

    [PunRPC]
    public void SetPlayerType(PlayerType type)
    {
        playerType = type;
    }

    public void SetNickNameColor(PlayerType type)
    {
        if(playerType == PlayerType.imposter && type == PlayerType.imposter)
        {
            nicknameText.color = Color.red;
        }
    }

    [PunRPC]
    public void Death()
    {
        animator.SetTrigger("Death");
        if (photonView.IsMine)
        {
            isMoveable = false;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerColor);
            stream.SendNext(playerType);
        }
        else
        {
            playerColor = (EPlayerColor)stream.ReceiveNext();
            playerType = (PlayerType)stream.ReceiveNext();
        }
    }
}
