using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool initialized;
    private PlayerManager pm;
    private GameManager gm;
    public bool darkRoom;
    public GameObject corridor;
    public GameObject darkCorridor;
    public Room north;
    public Room south;
    public Room east;
    public Room west;
    public Animator backgroundImage;

    public bool lockedNorth;
    public bool lockedSouth;
    public bool lockedEast;
    public bool lockedWest;
    public GameObject item;
    private void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerManager>();
        gm = Camera.main.GetComponent<GameManager>();

        if(north != null)
        {
            SpawnCorridor(north, "north");
        }
        if (south != null)
        {
            SpawnCorridor(south, "south");
        }
        if (east != null)
        {
            SpawnCorridor(east, "east");
        }
        if (west != null)
        {
            SpawnCorridor(west, "west");
        }
    }
    private void OnMouseDown()
    {
        if(north == pm.currentRoom)
        {
            pm.MoveDown();
        }
        else if (south == pm.currentRoom)
        {
            pm.MoveUp();
        }
        else if (east == pm.currentRoom)
        {
            pm.MoveRight();
        }
        else if (west == pm.currentRoom)
        {
            pm.MoveLeft();
        }

    }
    void SpawnCorridor(Room _room, string _dir)
    {
        GameObject _corr = corridor;
        if(darkRoom == true)
        {
            _corr = darkCorridor;
        }
        if(_room.darkRoom == true)
        {
            _corr = darkCorridor;
        }
        Vector2 normal = (_room.transform.position - transform.position).normalized;
        Debug.Log(normal);
        GameObject _obj = Instantiate(_corr, transform.position, Quaternion.LookRotation(Vector3.forward, normal), transform);
        _obj.transform.rotation = Quaternion.Euler(_obj.transform.rotation.eulerAngles + new Vector3(0, 0, 90f));
        _obj.name = _dir;
    }
    public Room returnRoom(string _dir)
    {
        if(_dir == "north" && north != null)
        {
            return north;
        }
        if (_dir == "south" && south != null)
        {
            return south;
        }
        if (_dir == "east" && east != null)
        {
            return east;
        }
        if (_dir == "west" && west != null)
        {
            return west;
        }
        return null;
    }
    public void RoomtoPlayer(List<Room> _rooms, int _distance, Pathfinder pf)
    {
        if (this != pm.currentRoom)
        {
            _rooms.Add(this);
            float _north = Physics2D.Linecast(north.transform.position, pm.currentRoom.transform.position).distance;
            float _south = Physics2D.Linecast(south.transform.position, pm.currentRoom.transform.position).distance;
            float _east = Physics2D.Linecast(east.transform.position, pm.currentRoom.transform.position).distance;
            float _west = Physics2D.Linecast(west.transform.position, pm.currentRoom.transform.position).distance;

            float shortest = Mathf.Min(_north, _south, _east, _west);
            if (_north == shortest)
            {
                north.RoomtoPlayer(_rooms, _distance + 1, pf);
            }
            if (_south == shortest)
            {
                south.RoomtoPlayer(_rooms, _distance + 1, pf);
            }
            if (_east == shortest)
            {
                east.RoomtoPlayer(_rooms, _distance + 1, pf);
            }
            if (_west == shortest)
            {
                west.RoomtoPlayer(_rooms, _distance + 1, pf);
            }
        }
        else
        {

        }
    }
    public void LockConnection(string _dir, bool _lock)
    {
        if(_dir == "north")
        {
            north.LockCorridor(_dir, _lock);
            lockedNorth = _lock;
            if(_lock == true)
            {
                transform.Find("north").GetComponent<CorridorColor>().SetColor(gm.disabledCorridorColor);
            }
            else
            {
                transform.Find("north").GetComponent<CorridorColor>().SetColor(Color.white);
            }
        }
        else if(_dir == "south")
        {
            south.LockCorridor(_dir, _lock);
            lockedSouth = _lock;
            if (_lock == true)
            {
                transform.Find("south").GetComponent<CorridorColor>().SetColor(gm.disabledCorridorColor);
            }
            else
            {
                transform.Find("south").GetComponent<CorridorColor>().SetColor(Color.white);
            }
        }
        else if(_dir == "east")
        {
            east.LockCorridor(_dir, _lock);
            lockedEast = _lock;
            if (_lock == true)
            {
                transform.Find("east").GetComponent<CorridorColor>().SetColor(gm.disabledCorridorColor);
            }
            else
            {
                transform.Find("east").GetComponent<CorridorColor>().SetColor(Color.white);
            }
        }
        else if(_dir == "west")
        {
            west.LockCorridor(_dir, _lock);
            lockedWest = _lock;
            if (_lock == true)
            {
                transform.Find("west").GetComponent<CorridorColor>().SetColor(gm.disabledCorridorColor);
            }
            else
            {
                transform.Find("west").GetComponent<CorridorColor>().SetColor(Color.white);
            }
        }
    }
    public void LockCorridor(string _dir, bool _lock)
    {
        if (_dir == "north")
        {
            lockedSouth = _lock;
            if (_lock == true)
            {
                transform.Find("south").GetComponent<CorridorColor>().SetColor(gm.disabledCorridorColor);
            }
            else
            {
                transform.Find("south").GetComponent<CorridorColor>().SetColor(Color.white);
            }
        }
        else if (_dir == "south")
        {
            lockedNorth = _lock;
            if (_lock == true)
            {
                transform.Find("north").GetComponent<CorridorColor>().SetColor(gm.disabledCorridorColor);
            }
            else
            {
                transform.Find("north").GetComponent<CorridorColor>().SetColor(Color.white);
            }
        }
        else if (_dir == "east")
        {
            lockedWest = _lock;
            if (_lock == true)
            {
                transform.Find("west").GetComponent<CorridorColor>().SetColor(gm.disabledCorridorColor);
            }
            else
            {
                transform.Find("west").GetComponent<CorridorColor>().SetColor(Color.white);
            }
        }
        else if (_dir == "west")
        {
            lockedEast = _lock;
            if (_lock == true)
            {
                transform.Find("east").GetComponent<CorridorColor>().SetColor(gm.disabledCorridorColor);
            }
            else
            {
                transform.Find("east").GetComponent<CorridorColor>().SetColor(Color.white);
            }
        }
    }
    private void OnDrawGizmos()
    {
        if(north != null)
            Gizmos.DrawLine(transform.position + new Vector3(-0.1f, 0f, 0f), north.transform.position + new Vector3(-0.1f, 0f, 0f));
        if (south != null)
            Gizmos.DrawLine(transform.position + new Vector3(0.1f, 0f, 0f), south.transform.position + new Vector3(0.1f, 0f, 0f));
        if (east != null)
            Gizmos.DrawLine(transform.position + new Vector3(0f, 0.1f, 0f), east.transform.position + new Vector3(0f, 0.1f, 0f));
        if (west != null)
            Gizmos.DrawLine(transform.position + new Vector3(0f, -0.1f, 0f), west.transform.position + new Vector3(0f, -0.1f, 0f));
    }
    public GameObject SwapItem(GameObject _obj)
    {
        GameObject _hold = item;
        item = _obj;
        if (_obj != null)
        {
            _obj.transform.SetParent(transform);
            _obj.transform.localPosition = new Vector3(0, -0.025f, 0);
            _obj.transform.localScale = new Vector3(0.4f, 0.4f, 1);
        }
        return _hold;
    }
}
