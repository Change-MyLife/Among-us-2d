using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class LobbySettingUI : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Button MouseControlButton;
    [SerializeField]
    private Button KeyboardMouseControlButton;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void OnEnable()
    {
        // 캐릭터 정지
        CharacterMove.isMoveable = false;

        switch (Setting.controlType)
        {
            case EcontrolType.Mouse:
                MouseControlButton.image.color = Color.green;
                KeyboardMouseControlButton.image.color = Color.white;
                break;
            case EcontrolType.KeyboardMouse:
                MouseControlButton.image.color = Color.white;
                KeyboardMouseControlButton.image.color = Color.green;
                break;
        }
        base.OnEnable();
    }

    public void SetControlMode(int controlType)
    {
        Setting.controlType = (EcontrolType)controlType;
        switch (Setting.controlType)
        {
            case EcontrolType.Mouse:
                MouseControlButton.image.color = Color.green;
                KeyboardMouseControlButton.image.color = Color.white;
                break;
            case EcontrolType.KeyboardMouse:
                MouseControlButton.image.color = Color.white;
                KeyboardMouseControlButton.image.color = Color.green;
                break;
        }
    }

    public void Close()
    {
        // 캐릭터 이동
        CharacterMove.isMoveable = true;

        StartCoroutine(CloseAfterDelay());
    }

    private IEnumerator CloseAfterDelay()
    {
        animator.SetTrigger("close");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        animator.ResetTrigger("close");
    }

    public void OutGameButton()
    {
        if (PhotonNetwork.CurrentRoom == null)
        {
            SceneManager.LoadScene("MainMenuScene");
        }
        else
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    public override void OnLeftRoom()
    {
        //PhotonNetwork.LoadLevel(0);
        SceneManager.LoadScene("MainMenuScene");

        base.OnLeftRoom();
    }

    public void ReturnGameButton()
    {
        Close();
    }
}
