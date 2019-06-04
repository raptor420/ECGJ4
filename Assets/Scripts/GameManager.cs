using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public UrgentSoundManager sound;
    public GameObject WinCanvas;
    public GameObject LoseCanvas; 
    public GameObject singleplayer; 
    public static GameManager instance;

    public Boss boss;
    [Space]
    [Space]
    public bool isDebug = false;

    void Start()
    {
        LoseCanvas.SetActive(false);
        WinCanvas.SetActive(false);

        instance = this;

        /*if (isDebug)
        {
            StartCoroutine(boss.TestProjectileShoot());
        }*/
    }
    
    public void StartOnePlayer()
    {
        //later implemented
    }

    public void StartTwoPlayer()
    {
        //later implemented
    }

    public void PlayerDie()
    {
        if (isDebug)
        {
            return;

        }

        else
        {

            singleplayer.SetActive(false);
            LoseCanvas.SetActive(true);
            sound.Playaudio(0);

        }

      
        //game over
    }

    public void BossDie()
    {
        WinCanvas.SetActive(true);

        sound.Playaudio(1);
    }
    public void PlayAgain()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

}
