using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaptopTrigger : MonoBehaviour
{
    [SerializeField]
    private Button UseButton;

    [SerializeField]
    private Sprite[] sprites;

    [SerializeField]
    private GameObject UI;

    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("노트북과 상호작용");
        UseButton.image.overrideSprite = sprites[1];
        UseButton.interactable = true;

        UseButton.onClick.AddListener(UseButtonOnClick);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("노트북에서 빠져나갔습니다.");
        UseButton.image.overrideSprite = sprites[0];
        UseButton.interactable = false;
    }

    void UseButtonOnClick()
    {
        UI.SetActive(true);
    }
}
