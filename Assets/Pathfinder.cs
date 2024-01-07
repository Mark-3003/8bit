using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    PlayerManager pm;
    Room currentRoom;
    List<Room> starter;
    private void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerManager>();
    }
    void FindPath()
    {
        starter.Clear();
        starter.Add(currentRoom);
        currentRoom.RoomtoPlayer(starter, 1, this);
    }
}
