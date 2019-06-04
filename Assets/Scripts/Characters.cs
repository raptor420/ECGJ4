using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    public Transform arm;
    public GameObject body;
    public GameObject ball;
    public Transform otherChar;

    [Space]
    public float rotOffset;
    private Vector3 correctArmPositionOffset;
    private void Awake()
    {
        correctArmPositionOffset = arm.position - transform.position;
    }

    private void Update()
    {
        TargetFreeChar();
        body.transform.rotation = Quaternion.identity;
        
    }

    void TargetFreeChar()//reused and modified Farhan's function ~artemis
    {
        // direction calculation
        Vector3 difference = otherChar.position - arm.position;
        //anglee of Z axis Calculation
        float rot = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        arm.rotation = Quaternion.Euler(0, 0, rot - rotOffset);
        arm.position = transform.position + correctArmPositionOffset;

    }
}
