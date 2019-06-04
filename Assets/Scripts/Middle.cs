using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Middle : MonoBehaviour
{
    public Transform red;
    public Transform blue;

    void Update()
    {
        transform.position = (red.position + blue.position) / 2;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        // Gizmos.DrawSphere(transform.position, Radius);
        Gizmos.DrawWireSphere(transform.position, 1.25f);
    }
}
