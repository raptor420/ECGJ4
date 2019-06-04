using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamager : MonoBehaviour
{
    public bool isSpecialAtk;
    public Boss boss;
    public Momentum mom;//entum manager

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Boss")
        {
            boss.Damage(mom.Ranges[mom.GetRangeIndex()].z);

            if (isSpecialAtk)
                boss.Damage(mom.Ranges[mom.GetRangeIndex()].z * 2f);

            GetComponent<Cinemachine.CinemachineImpulseSource>().GenerateImpulse();
        }
    }
}
