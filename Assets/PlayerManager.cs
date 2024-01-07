using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerManager : MonoBehaviour
{
    public AudioSource NeutralSource;
    public AudioSource FaintLeftSource;
    public AudioSource LeftSource;
    public AudioSource FaintRightSource;
    public AudioSource RightSource;
    public AudioSource FaintForwardSource;
    public AudioSource ForwardSource;
    public AudioClip walk;
    public Room currentRoom;
    public bool canMove;

    [Header("Items")]
    public Animator slot1;
    public Animator slot2;
    public Animator slot3;
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public Transform itemSelection;
    int currentSlot;

    bool slots23;
    AudioReverbZone arz;
    GameManager gm;
    private void Awake()
    {
        slots23 = false;
        arz = GetComponent<AudioReverbZone>();
        gm = Camera.main.GetComponent<GameManager>();
    }
    public void SetActiveRoom(Room _room, bool _lock)
    {
        if (canMove && _lock == false)
        {
            if (currentRoom.backgroundImage != null)
            {
                currentRoom.backgroundImage.SetBool("Active", false);
            }

            Play2DSound(walk, 1f);
            currentRoom = _room;
            transform.position = currentRoom.transform.position;

            if (currentRoom.backgroundImage != null)
            {
                currentRoom.backgroundImage.SetBool("Active", true);
            }
            if (currentRoom.darkRoom == true)
            {
                arz.enabled = false;
            }
            else
            {
                arz.enabled = true;
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            SwapItem();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GiveItem(gm.Lock))
            {
                SayText("(LOCK GIVEN)", 2, 16, Color.white);
            }
            else
            {
                SayText("(HANDS FULL)", 2, 16, Color.white);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && item1 != null && currentRoom.darkRoom == false)
        {
            if (currentSlot == 1)
            {
                slot1.SetBool("Active", false);
                //slot2.SetBool("Active", false);
                //slot3.SetBool("Active", false);
                itemSelection.localPosition = new Vector3(-20f, -9f, 10f);
                currentSlot = 0;
                UpdateRoomColors();
            }
            else
            {
                currentSlot = 1;
                itemSelection.localPosition = new Vector3(-18.27f, -9.68f, 10f);
                slot1.SetBool("Active", true);
                //slot2.SetBool("Active", false);
                //slot3.SetBool("Active", false);
                UpdateRoomColors();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && slots23)
        {
            if (currentSlot == 2)
            {
                slot1.SetBool("Active", false);
                slot2.SetBool("Active", false);
                slot3.SetBool("Active", false);
                itemSelection.localPosition = new Vector3(-20f, -9f, 10f);
                currentSlot = 0;
            }
            else
            {
                currentSlot = 2;
                itemSelection.localPosition = new Vector3(-17.2f, -9.68f, 10f);
                slot1.SetBool("Active", false);
                slot2.SetBool("Active", true);
                slot3.SetBool("Active", false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && slots23)
        {
            if (currentSlot == 3)
            {
                slot1.SetBool("Active", false);
                slot2.SetBool("Active", false);
                slot3.SetBool("Active", false);
                itemSelection.localPosition = new Vector3(-20f, -9f, 10f);
                currentSlot = 0;
            }
            else
            {
                currentSlot = 3;
                itemSelection.localPosition = new Vector3(-16.13f, -9.68f, 10f);
                slot1.SetBool("Active", false);
                slot2.SetBool("Active", false);
                slot3.SetBool("Active", true);
            }
        }
    }
    public void SayText(string _text, float fadeTime, float _textSpeed, Color _color)
    {
        GetComponent<TextController>().SayText(_text, fadeTime, _textSpeed, _color);
    }
    void UseItem(string _dir)
    {
        currentSlot = 0;
        UpdateRoomColors();
        itemSelection.localPosition = new Vector3(-20f, -9f, 10f);
        if (item1.name == "lock")
        {
            if (_dir == "north" && currentRoom.lockedNorth == false)
            {
                currentRoom.LockConnection(_dir, true);
                Destroy(item1);
                item1 = null;
                GiveItem(gm.Key);
            }
            else if (_dir == "south" && currentRoom.lockedSouth == false)
            {
                currentRoom.LockConnection(_dir, true);
                Destroy(item1);
                item1 = null;
                GiveItem(gm.Key);
            }
            else if (_dir == "east" && currentRoom.lockedEast == false)
            {
                currentRoom.LockConnection(_dir, true);
                Destroy(item1);
                item1 = null;
                GiveItem(gm.Key);
            }
            else if (_dir == "west" && currentRoom.lockedWest == false)
            {
                currentRoom.LockConnection(_dir, true);
                Destroy(item1);
                item1 = null;
                GiveItem(gm.Key);
            }
        }
        else if(item1.name == "key")
        {
            if (_dir == "north" && currentRoom.lockedNorth == true)
            {
                currentRoom.LockConnection(_dir, false);
                Destroy(item1);
            }
            else if (_dir == "south" && currentRoom.lockedSouth == true)
            {
                currentRoom.LockConnection(_dir, false);
                Destroy(item1);
            }
            else if (_dir == "east" && currentRoom.lockedEast == true)
            {
                currentRoom.LockConnection(_dir, false);
                Destroy(item1);
            }
            else if (_dir == "west" && currentRoom.lockedWest == true)
            {
                currentRoom.LockConnection(_dir, false);
                Destroy(item1);
            }
        }
    }
    void UpdateRoomColors()
    {
        if(currentSlot >= 1)
        {
            if(currentRoom.north != null)
            {
                if (currentRoom.north.south != null)
                {
                    if(item1.name == "lock")
                    {
                        if(currentRoom.lockedNorth == false)
                        {
                            currentRoom.north.GetComponent<SpriteRenderer>().color = Color.yellow;
                        }
                    }
                    else if(item1.name == "key")
                    {
                        if(currentRoom.lockedNorth == true)
                        {
                            currentRoom.north.GetComponent<SpriteRenderer>().color = Color.yellow;
                        }
                    }
                }
            }
            if (currentRoom.south != null)
            {
                if (currentRoom.south.north != null)
                {
                    if (item1.name == "lock")
                    {
                        if (currentRoom.lockedSouth == false)
                        {
                            currentRoom.south.GetComponent<SpriteRenderer>().color = Color.yellow;
                        }
                    }
                    else if (item1.name == "key")
                    {
                        if (currentRoom.lockedSouth == true)
                        {
                            currentRoom.south.GetComponent<SpriteRenderer>().color = Color.yellow;
                        }
                    }
                }
            }
            if (currentRoom.east != null)
            {
                if (currentRoom.east.west != null)
                {
                    if (item1.name == "lock")
                    {
                        if (currentRoom.lockedEast == false)
                        {
                            currentRoom.east.GetComponent<SpriteRenderer>().color = Color.yellow;
                        }
                    }
                    else if (item1.name == "key")
                    {
                        if (currentRoom.lockedEast == true)
                        {
                            currentRoom.east.GetComponent<SpriteRenderer>().color = Color.yellow;
                        }
                    }
                }
            }
            if (currentRoom.west != null)
            {
                if (currentRoom.west.east != null)
                {
                    if (item1.name == "lock")
                    {
                        if (currentRoom.lockedWest == false)
                        {
                            currentRoom.west.GetComponent<SpriteRenderer>().color = Color.yellow;
                        }
                    }
                    else if (item1.name == "key")
                    {
                        if (currentRoom.lockedWest == true)
                        {
                            currentRoom.west.GetComponent<SpriteRenderer>().color = Color.yellow;
                        }
                    }
                }
            }
        }
        else
        {
            slot1.SetBool("Active", false);
            if (currentRoom.north != null)
            {
                currentRoom.north.GetComponent<SpriteRenderer>().color = Color.white;
            }
            if (currentRoom.south != null)
            {
                currentRoom.south.GetComponent<SpriteRenderer>().color = Color.white;
            }
            if (currentRoom.east != null)
            {
                currentRoom.east.GetComponent<SpriteRenderer>().color = Color.white;
            }
            if (currentRoom.west != null)
            {
                currentRoom.west.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
    public bool GiveItem(GameObject _item)
    {
        if(item1 == null)
        {
            GameObject _obj = Instantiate(_item, slot1.transform);
            _obj.name = _item.name;
            item1 = _obj;
            return true;
        }
        else
        {
            return false;
        }
    }
    void SwapItem()
    {
        GameObject _hold = currentRoom.SwapItem(item1);
        item1 = _hold;
        if (item1 != null)
        {
            item1.transform.SetParent(slot1.transform);
            item1.transform.localPosition = Vector3.zero;
            item1.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void MoveUp()
    {
        if (currentSlot == 0)
        {
            Room _room = currentRoom.returnRoom("north");
            if (_room != null)
            {
                SetActiveRoom(_room, currentRoom.lockedNorth);
            }
        }
        else if (currentSlot >= 1 && item1 != null)
        {
            UseItem("north");
        }
    }
    public void MoveDown()
    {
        if (currentSlot == 0)
        {
            Room _room = currentRoom.returnRoom("south");
            if (_room != null)
            {
                SetActiveRoom(_room, currentRoom.lockedSouth);
            }
        }
        else if (currentSlot >= 1 && item1 != null)
        {
            UseItem("south");
        }
    }
    public void MoveLeft()
    {
        if (currentSlot == 0)
        {
            Room _room = currentRoom.returnRoom("east");
            if (_room != null)
            {
                SetActiveRoom(_room, currentRoom.lockedEast);
            }
        }
        else if (currentSlot >= 1 && item1 != null)
        {
            UseItem("east");
        }
    }
    public void MoveRight()
    {
        if (currentSlot == 0)
        {
            Room _room = currentRoom.returnRoom("west");
            if (_room != null)
            {
                SetActiveRoom(_room, currentRoom.lockedWest);
            }
        }
        else if (currentSlot >= 1 && item1 != null)
        {
            UseItem("west");
        }
    }
    public void Play2DSound(AudioClip _clip, float _volume)
    {
        NeutralSource.PlayOneShot(_clip, _volume);
    }
    public void PlayFaintLeftSound(AudioClip _clip, float _volume)
    {
        FaintLeftSource.PlayOneShot(_clip, _volume);
    }
    public void PlayLeftSound(AudioClip _clip, float _volume)
    {
        LeftSource.PlayOneShot(_clip, _volume);
    }
    public void PlayFaintRightSound(AudioClip _clip, float _volume)
    {
        FaintRightSource.PlayOneShot(_clip, _volume);
    }
    public void PlayRightSound(AudioClip _clip, float _volume)
    {
        RightSource.PlayOneShot(_clip, _volume);
    }
    public void PlayFaintForwardSound(AudioClip _clip, float _volume)
    {
        FaintForwardSource.PlayOneShot(_clip, _volume);
    }
    public void PlayForwardSound(AudioClip _clip, float _volume)
    {
        ForwardSource.PlayOneShot(_clip, _volume);
    }
}
