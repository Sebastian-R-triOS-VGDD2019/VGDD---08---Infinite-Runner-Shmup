using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharacterMovement player;
    public GameObject pickups;
    public HUDManager HUD;

    float time = 0f;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //player.ChangeState(CharacterMovement.eState.STANDINGBY);
    }



    void Update()
    {
        if (player.state == CharacterMovement.eState.NORMAL)
        {
            time += Time.deltaTime;


        }
        else if(player.state == CharacterMovement.eState.STANDINGBY)
        {
            //if(Input.GetKeyDown(KeyCode.Space))
            //{
            //    player.ChangeState(CharacterMovement.eState.NORMAL);
            //}
        }
        else if(player.state == CharacterMovement.eState.DEAD)
        {
            //Debug.Log("Yo dead, son!");
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("game");
            }
        }
    }

    public float getPlayTime()
    {
        return time;
    }
}
