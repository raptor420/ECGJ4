using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{//put this on camera to shake, or anything actually.. it'll shake it
    // type public Shaker shaker to get a reference of it and call Shaerk.Shake(duration)
    Transform target; // set it to the object you wanna shake
    Vector3 initialsPosition; // initial position, before shaking the position of the object
    float pendingShakeDuration = 0f;
    bool isShhaking = false;
    [Range(0f,1f)]
    public float Intensity;
    
    private void Start()
    {
        target = GetComponent<Transform>();
        initialsPosition = target.position;
    }
    public void Shake(float duration)
    {
        //Shake is called, if it isn't minus we add to our pending shake time
        if(duration > 0)
        {

            pendingShakeDuration += duration;
        }

    }
    private void Update()
    {
        if (pendingShakeDuration > 0 && !isShhaking)
        {
            StartCoroutine(DoShake());
        }
    }

    IEnumerator DoShake()
    {
        //now we are shaking
        isShhaking = true;
        var StartTime = Time.realtimeSinceStartup;
        while(Time.realtimeSinceStartup < StartTime + pendingShakeDuration)
        { //while pendingshakeduration isn't over we must SHAKE!
            var randompoint = new Vector3(Random.Range(1f, 1f)*Intensity, Random.Range(1f, 1f)*Intensity, initialsPosition.z);
            target.localPosition = randompoint;
            yield return null;
        }
        pendingShakeDuration =0;
        target.localPosition = initialsPosition;
        // time's up, shaking stop.
        isShhaking = false;


    }
}
