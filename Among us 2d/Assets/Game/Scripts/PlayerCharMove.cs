using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class PlayerCharMove : MonoBehaviourPunCallbacks
{
    private Animator animator;

    static public bool isMoveable;
    public float speed = 2f;

    // 캐릭터 색상 변수
    private SpriteRenderer spriteRender;
    public EPlayerColor playerColor;

    [SerializeField]
    private Text nicknameText;
    private string nickname;

    void Start()
    {
        // 캐릭터 색상 초기화
        spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.material.SetColor("_PlayerColor", PlayerColor.GetColor(playerColor));

        isMoveable = true;
        animator = GetComponent<Animator>();

        if (photonView.IsMine)
        {
            Camera cam = Camera.main;
            cam.transform.SetParent(transform);
            cam.transform.localPosition = new Vector3(0f, 0f, -10f);
            cam.orthographicSize = 1.8f;
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
                if (dir.x < 0f) transform.localScale = new Vector3(-0.35f, 0.35f, 1f);
                else if (dir.x > 0f) transform.localScale = new Vector3(0.35f, 0.35f, 1f);
                transform.position += dir * speed * Time.deltaTime;
                isMove = dir.magnitude != 0f;
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f)).normalized;
                    if (dir.x < 0f) transform.localScale = new Vector3(-0.35f, 0.35f, 1f);
                    else if (dir.x > 0f) transform.localScale = new Vector3(0.35f, 0.35f, 1f);
                    transform.position += dir * speed * Time.deltaTime;
                    isMove = dir.magnitude != 0f;
                }
            }
            // 애니메이터 파라미터
            animator.SetBool("IsMove", isMove);
        }

        if (transform.localScale.x < 0)
        {
            nicknameText.transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
        }
        else if (transform.localScale.x > 0)
        {
            nicknameText.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
    }

    // 캐릭터 닉네임 변경
    [PunRPC]
    void SetNickName()
    {
        nickname = photonView.Owner.NickName;
        nicknameText.text = nickname;
    }


    // 캐릭터 색상 변경
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
}
