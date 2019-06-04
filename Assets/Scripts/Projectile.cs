using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;//now many hit points it will damage, increase damage for more damaging projectiles
    public float DestroyDelay = 8f; //how may seconds to wait before removing the orijectile from scene after deflected

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8) //if it hits the attacking character
        {
            PlayerHealth.ph.Damage(damage);

            Destroy(gameObject);

            //Play hit animation, if any
        }

        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10) //if it hits the defending character or other things on the layer 9:Blue
        {
            //play distroy animation
            Destroy(gameObject);
        }
    }
    
    public void TestShoot()
    {
        GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * 120f);
    }
}
