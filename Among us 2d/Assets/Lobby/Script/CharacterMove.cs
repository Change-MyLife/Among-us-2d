using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class CharacterMove : MonoBehaviourPunCallbacks, IPunObservable
{
    private Animator animator;

    static public bool isMoveable;
    public float speed = 2f;

    private SpriteRenderer spriteRender;

    public EPlayerColor playerColor;

    void Start()
    {
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
    }

    void Update()
    {
        // UI 버튼을 클릭시 캐릭터 움직임 x
        if((Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject()))
        {
            isMoveable = false;
        }

        if (photonView.IsMine && isMoveable)
        {
            bool isMove = false;
            if (Setting.controlType == EcontrolType.KeyboardMouse)
            {
                Vector3 dir = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f), 1f);
                if (dir.x < 0f) transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
                else if (dir.x > 0f) transform.localScale = new Vector3(0.5f, 0.5f, 1f);
                transform.position += dir * speed * Time.deltaTime;
                isMove = dir.magnitude != 0f;
            }
            else
            {
                if(Input.GetMouseButton(0))
                {
                    Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f)).normalized;
                    if (dir.x < 0f) transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
                    else if (dir.x > 0f) transform.localScale = new Vector3(0.5f, 0.5f, 1f);
                    transform.position += dir * speed * Time.deltaTime;
                    isMove = dir.magnitude != 0f;
                }
                
            }

            // 애니메이터 파라미터
            animator.SetBool("IsMove", isMove);
        }
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerColor);
        }
        else
        {
            playerColor = (EPlayerColor)stream.ReceiveNext();
        }
    }
}
