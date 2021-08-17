using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerColor
{
    Red, Blue, Green,
    Pink, Orange, Yellow,
    Black, White, Purple,
    Brown, Cyan, Lime
}

public class PlayerColor
{
    private static List<Color> colors = new List<Color>()
    {
        new Color(1f,0f,0f),
        new Color(0f,0.1f,1f),
        new Color(0f, 0.4f, 0f),

        new Color(1f, 0f, 0.7f),
        new Color(1f, 0.5f, 0f),
        new Color(1f, 0.9f, 0f),

        new Color(0.2f, 0.2f, 0.2f),
        new Color(0.7f, 0.7f, 0.7f),
        new Color(0.5f, 0f, 0.5f),

        new Color(0.5f, 0.3f, 0f),
        new Color(0f, 0.7f, 0.7f),
        new Color(0f, 1f, 0f)
    };

    public static Color GetColor(EPlayerColor playerColor)
    {
        return colors[(int)playerColor];
    }
}
