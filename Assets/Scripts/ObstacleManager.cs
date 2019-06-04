using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    //if debugging
    public bool debug = false;
    [Space]

    //amount of those f**kers lol
    public int Amount;
    //the obstacle prefab
    public Transform ObstaclePrefab;
    // to target players position
    public Transform PlayerPosition;
    //Radius where the objects will spawn
    public float Radius;

    // time that the objects will be kept alive; // 
    public float TimeAlive;
    
    public float Alive;

    public float isAlive
    {

        set
        {
            Alive = value;
        }

        get
        {
            return Alive;

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        if (debug)
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab) && debug)
        {
            Spawn();
        }
     
    }
    // hopefully will give me a random point inside a circle
    public Vector3 RandomCircle(Vector2 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = 0;
        return pos;


    }

    public void Spawn()
    {

        for (int i = 0; i < Amount; i++)
        {
          var instance= Instantiate(ObstaclePrefab,RandomCircle(PlayerPosition.position, Radius),Quaternion.identity);

            instance.transform.parent = gameObject.transform;
            Destroy(instance.gameObject, TimeAlive);
        }

    }

    public void Spawn(int _amount, float _aliveTime, float _radius, bool randomSize = false)
    {
        for (int i = 0; i < _amount; i++)
        {
            var instance = Instantiate(ObstaclePrefab, RandomCircle(PlayerPosition.position, _radius), Quaternion.identity);

            instance.transform.parent = gameObject.transform;
            if (randomSize) instance.transform.localScale *= Random.Range(0.9f, 2.4f);
            Destroy(instance.gameObject, _aliveTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
       // Gizmos.DrawSphere(transform.position, Radius);
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
