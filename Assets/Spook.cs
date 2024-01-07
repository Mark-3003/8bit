using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spook : MonoBehaviour
{
    public bool freezePlayer;
    public bool maskMostRooms;
    public bool maskCurrentRoom;
    public bool changeRoomColor;
    public Color roomColorChange;

    public float spookTime;
    public float maskActivationOffset;

    PlayerManager pm;
    CameraManager cm;
    float timer;
    bool active;
    private void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerManager>();
        cm = Camera.main.GetComponent<CameraManager>();
    }
    public void Scare()
    {
        active = true;
        if (freezePlayer)
        {
            pm.canMove = false;
            cm.canScroll = false;
        }

    }
    private void Update()
    {
        if (active)
        {
            timer += Time.deltaTime;
        }
    }
}
