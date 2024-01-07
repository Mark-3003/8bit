using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public bool moveup;
    public bool movedown;
    public bool moveleft;
    public bool moveright;

    public Room currentRoom;
    // Update is called once per frame
    void Update()
    {
        if (moveup)
        {
            moveup = false;
            if(currentRoom.north != null && currentRoom.lockedNorth == false)
            {
                currentRoom = currentRoom.north;
                transform.position = currentRoom.transform.position;
            }
        }
        if (movedown)
        {
            movedown = false;
            if (currentRoom.south != null && currentRoom.lockedSouth == false)
            {
                currentRoom = currentRoom.south;
                transform.position = currentRoom.transform.position;
            }
        }
        if (moveleft)
        {
            moveleft = false;
            if (currentRoom.east != null && currentRoom.lockedEast == false)
            {
                currentRoom = currentRoom.east;
                transform.position = currentRoom.transform.position;
            }
        }
        if (moveright)
        {
            moveright = false;
            if (currentRoom.west != null && currentRoom.lockedWest == false)
            {
                currentRoom = currentRoom.west;
                transform.position = currentRoom.transform.position;
            }
        }
    }
}
