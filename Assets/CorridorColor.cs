using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorColor : MonoBehaviour
{
    public SpriteRenderer one;
    public SpriteRenderer two;
    public SpriteRenderer three;

    public void SetColor(Color _color)
    {
        one.color = _color;
        two.color = _color;
        three.color = _color;
    }
}
