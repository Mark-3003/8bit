using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float scrollSpeed;
    public Vector2 scrollLimit;
    public bool canScroll;

    public GameObject outerMask;
    public GameObject innerMask;
    void Update()
    {
        if (canScroll)
        {
            Camera.main.orthographicSize += Input.mouseScrollDelta.y * scrollSpeed;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, scrollLimit.y, scrollLimit.x);
        }
    }
}
