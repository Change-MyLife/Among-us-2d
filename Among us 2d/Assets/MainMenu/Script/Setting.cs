using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EcontrolType
{
    Mouse,
    KeyboardMouse
}

public class Setting : MonoBehaviour
{
    public static EcontrolType controlType;
    public static string nickname;
}