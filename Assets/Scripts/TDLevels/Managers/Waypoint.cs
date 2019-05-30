using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public SpriteRenderer spr;
    public Sprite leftCornerSprite;
    public Sprite rightCornerSprite;

    public void SetCorner(bool right)
    {
        if(right)
            spr.sprite = rightCornerSprite;
        else
            spr.sprite = leftCornerSprite;
    }
}
