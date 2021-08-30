using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterColorUI : MonoBehaviour
{
    private void OnEnable()
    {
        // 캐릭터 정지
        CharacterMove.isMoveable = false;
    }

    public void Close()
    {
        // 캐릭터 이동
        CharacterMove.isMoveable = true;
        gameObject.SetActive(false);
    }
}
