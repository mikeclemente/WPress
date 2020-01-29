using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController main;

    public int playerIndex;
    public Animator anim;

    public bool npc = false;

    private int keyIndex = 8;
    private bool canPlay = false;

    private float timer;
    private float nextKeyTime;
    // Start is called before the first frame update
    private void Awake()
    {
        main = this;
    }

    IEnumerator Start()
    {

        while (LevelController.instance.canPlay == false)
        {
            yield return null;
        }

        canPlay = true;

    }

    // Update is called once per frame
    void Update()
    {
        if(canPlay == false)
        {
            return;
        }

        timer += Time.deltaTime;

        if(npc)
        {
            if(Time.time > nextKeyTime)
            {
                nextKeyTime = Time.time + Random.Range(0.25f, 0.5f);
                KeyPress();
            }
            return;
        }

        if (Input.GetButtonDown(LevelController.instance.gameKeys[keyIndex].key[playerIndex]))
        {
            KeyPress();
        }
        else if (Input.anyKeyDown)
        {
            timer += 0.2f;
        }
    }

    void KeyPress()
    {
        LevelController.instance.NextKey(playerIndex, keyIndex);
        keyIndex--;

        if (keyIndex < 0)
        {
            canPlay = false;
            LevelController.instance.UpdatePlayerTime(timer, playerIndex);
        } 
    }

    public void SetAnimation(string triggerAnimation)
    {
        anim.SetTrigger(triggerAnimation);
    }
}
