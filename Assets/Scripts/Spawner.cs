using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//will not be used now, but could be used by some modifications later ~Artemis
public class Spawner : MonoBehaviour
{
    public Transform[] Spawnpoints;
    public Transform[] EnemyPrefabs;
    public float SpawnTime;
    public float WaitAfterSpawn;
    public  PlayerHealth playerhealth; 
    


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(SpawnTime);

        while (playerhealth!=null)
        {
            int random = Random.Range(0, EnemyPrefabs.Length);
            int randomspawnpoint = Random.Range(0, Spawnpoints.Length);


            Instantiate(EnemyPrefabs[random], Spawnpoints[randomspawnpoint]);
            yield return new WaitForSeconds(WaitAfterSpawn);

        }
        



    }
    
}
