using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth ph;
    
    public int MaxHealth;
    public int CurrentHealth;
    //shaketesting, Shaker Doesnot work if cinemachine is On.
    //[Range(0,1)]
    //public float shakeduration;
    //public Shaker shaker;

    [Space]
    public Transform HealthBar;
    public GameObject heartPrefab;
    public Sprite brokenHeart;
    //public float healthbarLerpTime;
    
     
    // Start is called before the first frame update
    void Awake()
    {
        ph = this;
        SetHealthBar();

    }

    // Update is called once per frame
    /*void Update()
    {
        HealthBar.fillAmount = ((float)CurrentHealth / (float)MaxHealth);
        if (CurrentHealth <= 0)
        {
            //Don't Destroy(this.gameObject) game over.

        }
        //  CurrentHealth--;

    }*/
    public void Damage(int amount)
    {
        SinglePlayer.SP.atkChar.GetComponent<Cinemachine.CinemachineImpulseSource>().GenerateImpulse();
        
        for (int i = 0; i < amount; i++)
        {
            if (CurrentHealth > 0)
            {
                HealthBar.GetChild(CurrentHealth - 1).GetComponent<Image>().sprite = brokenHeart;
                CurrentHealth--;
            }
        }
        if (CurrentHealth <= 0) { GameManager.instance.PlayerDie(); }

        //testing shake, Shaker Doesnot work if cinemachine is On.
        //shaker.Shake(shakeduration);

    }

    public void Repenish(int amount)
    {
        CurrentHealth += amount;
        for (int i = 0; i < amount; i++)
        {
            Instantiate(heartPrefab, HealthBar);
        }
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
    }

    public void SetHealthBar()
    {
        CurrentHealth = MaxHealth;
        for (int i = 0; i < HealthBar.childCount; i++)
        {
            Destroy(HealthBar.GetChild(i).gameObject);
        }
        for (int i = 0; i < MaxHealth; i++)
        {
            Instantiate(heartPrefab, HealthBar);
        }
    }
}
