using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SinglePlayer : MonoBehaviour
{
    
    private  static SinglePlayer _SP;
    public static SinglePlayer SP{
        
    get{
            return _SP;
        }
}
    public GameObject atkChar; //Red Character
    public GameObject dfnChar; //Blue Character
    public GameObject boss;
   
    [Space]
    public Vector2 untetheredAttackAimAssistRangeR;//when rotating right //aim assist not implemented currently
    public Vector2 untetheredAttackAimAssistRangeL;//              left
    [Space]
    public float distance = 10f;
    public float torque; //The power of spinning

    public GameObject pivot; //current
    public GameObject freeChar; //the character which isn't pivoted

    public Cinemachine.CinemachineVirtualCamera CMVCAtk; //virtual cameras
    public Cinemachine.CinemachineVirtualCamera CMVCDfn;

    //if the character is pivoted
    [Space]
    public  bool atkIsPivoted;
    public bool dfnIsPivoted;

    [Space]//these are colors of the characters, when pivoted and when not pivoted
    //public Color redN;
    //public Color redP;
    //public Color blueN;
    //public Color blueP;

    private bool tetheredAttack;

    public GameObject MCam;

    public float momentum;
    void FixedUpdate()
    {

        /*Quaternion newRot = pivot.transform.rotation;
        Vector3 newAngle = newRot.eulerAngles;*/

        if (pivot != null)
        {// m cam was turned on in artyoms code
           MCam.SetActive(false);
            Vector2 forceVector; //in which direction the force is applied
            if (atkIsPivoted)
            {
                freeChar = dfnChar;
            }
            else
            {
                freeChar = atkChar;
            }

            if (freeChar.transform.position.x > pivot.transform.position.x)
            {
                if (freeChar.transform.position.y > pivot.transform.position.y)
                    forceVector = new Vector2(1f, -1f);
                else if (freeChar.transform.position.y < pivot.transform.position.y)
                    forceVector = new Vector2(-1f, -1f);
                else
                    forceVector = new Vector2(0f, -1f);
            }
            else if (freeChar.transform.position.x < pivot.transform.position.x)
            {
                if (freeChar.transform.position.y > pivot.transform.position.y)
                    forceVector = new Vector2(1f, 1f);
                else if (freeChar.transform.position.y < pivot.transform.position.y)
                    forceVector = new Vector2(-1f, 1f);
                else
                    forceVector = new Vector2(0f, 1f);
            }
            else
            {
                if (freeChar.transform.position.y > pivot.transform.position.y)
                    forceVector = new Vector2(1f, 0f);
                else
                    forceVector = new Vector2(-1f, 0f);
            }
            freeChar.GetComponent<Rigidbody2D>().AddForce(forceVector * torque * Input.GetAxis("Horizontal"));
            freeChar.transform.position = (freeChar.transform.position - pivot.transform.position).normalized * distance + pivot.transform.position;
        }
       else MCam.SetActive(true);
        
        /*pivot.GetComponent<Rigidbody2D>().WakeUp();

        newRot.eulerAngles = newAngle;
        pivot.transform.rotation = newRot;*/



        if (Input.GetKeyUp(KeyCode.S))
        {
            if (!dfnIsPivoted) dfnIsPivoted = true;
            else dfnIsPivoted = false;

            if (!atkIsPivoted && dfnIsPivoted) pivot = dfnChar;
            else if (atkIsPivoted) pivot = atkChar;
        }

        else if (Input.GetKeyUp(KeyCode.W))
        {
            if (!atkIsPivoted) atkIsPivoted = true;
            else atkIsPivoted = false;

            if (atkIsPivoted && !dfnIsPivoted) pivot = atkChar;
            else if (dfnIsPivoted) pivot = dfnChar;
        }

        else if (Input.GetKeyUp(KeyCode.Space) && pivot != null) //swap pivots when only one is pivoted
        {
            if (!dfnIsPivoted) dfnIsPivoted = true;
            else dfnIsPivoted = false;

            if (!atkIsPivoted && dfnIsPivoted) pivot = dfnChar;
            else if (atkIsPivoted) pivot = atkChar;

            if (!atkIsPivoted) atkIsPivoted = true;
            else atkIsPivoted = false;

            if (atkIsPivoted && !dfnIsPivoted) pivot = atkChar;
            else if (dfnIsPivoted) pivot = dfnChar;
        }

        
        if (dfnIsPivoted && atkIsPivoted)
        {
            freeChar = null;
            pivot = null;
            /*dfnChar.gameObject.transform.SetParent(transform);
            atkChar.gameObject.transform.SetParent(transform);

            atkChar.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            dfnChar.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;*/
            dfnChar.GetComponent<HingeJoint2D>().connectedBody = null;
            atkChar.GetComponent<HingeJoint2D>().connectedBody = null; 
            //Camera
        }

        else if (!dfnIsPivoted && !atkIsPivoted) //WIP when both unttethered
        {
            if (!tetheredAttack) { tetheredAttack = false; StartCoroutine(TetheredAttack()); }
        }
        

        /*if (pivot == dfnChar)
        {
            dfnChar.gameObject.transform.SetParent(transform);
            atkChar.gameObject.transform.SetParent(dfnChar.gameObject.transform);
            pivot.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            atkChar.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

            CMVCDfn.gameObject.SetActive(true);
            CMVCAtk.gameObject.SetActive(false);
        }
        else if (pivot == atkChar)
        {
            atkChar.gameObject.transform.SetParent(transform);
            dfnChar.gameObject.transform.SetParent(atkChar.gameObject.transform);
            pivot.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            dfnChar.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

            CMVCDfn.gameObject.SetActive(false);
            CMVCAtk.gameObject.SetActive(true);
        }*/

        if (pivot == dfnChar)
        {
            dfnChar.GetComponent<HingeJoint2D>().connectedBody = null;
            atkChar.GetComponent<HingeJoint2D>().connectedBody = dfnChar.GetComponent<Rigidbody2D>();
            CMVCDfn.gameObject.SetActive(true);
            CMVCAtk.gameObject.SetActive(false);
        }

        else if (pivot == atkChar)
        {
            atkChar.GetComponent<HingeJoint2D>().connectedBody = null;
            dfnChar.GetComponent<HingeJoint2D>().connectedBody = atkChar.GetComponent<Rigidbody2D>();
            CMVCDfn.gameObject.SetActive(false);
            CMVCAtk.gameObject.SetActive(true);
        }

        if (dfnIsPivoted)
        {
            dfnChar.GetComponent<Characters>().body.SetActive(true);
            dfnChar.GetComponent<Characters>().arm.gameObject.SetActive(true);
            dfnChar.GetComponent<Characters>().ball.SetActive(false);
        }
        else
        {
            dfnChar.GetComponent<Characters>().body.SetActive(false);
            dfnChar.GetComponent<Characters>().arm.gameObject.SetActive(false);
            dfnChar.GetComponent<Characters>().ball.SetActive(true);
        }

        if (atkIsPivoted)
        {
            atkChar.GetComponent<Characters>().body.SetActive(true);
            atkChar.GetComponent<Characters>().arm.gameObject.SetActive(true);
            atkChar.GetComponent<Characters>().ball.SetActive(false);
        }
        else
        {
            atkChar.GetComponent<Characters>().body.SetActive(false);
            atkChar.GetComponent<Characters>().arm.gameObject.SetActive(false);
            atkChar.GetComponent<Characters>().ball.SetActive(true);
        }

    }
    

    private IEnumerator TetheredAttack()///wip
    {
        dfnChar.GetComponent<HingeJoint2D>().connectedBody = atkChar.GetComponent<Rigidbody2D>();
        atkChar.GetComponent<HingeJoint2D>().connectedBody = dfnChar.GetComponent<Rigidbody2D>();

        Vector2 attackMomentum = freeChar.GetComponent<Rigidbody2D>().velocity;
        GameObject fc = freeChar; GameObject pv = pivot; freeChar = null; pivot = null;

        /*Debug.Log(pv.GetComponent<Rigidbody2D>().angularVelocity); Debug.Log(TAAngleToRotate());
        if ((pv.GetComponent<Rigidbody2D>().angularVelocity > 0 && 
                (TAAngleToRotate() < untetheredAttackAimAssistRangeL.x || TAAngleToRotate() > untetheredAttackAimAssistRangeL.y))
           || (pv.GetComponent<Rigidbody2D>().angularVelocity < 0) &&
                (TAAngleToRotate() < untetheredAttackAimAssistRangeL.x || TAAngleToRotate() > untetheredAttackAimAssistRangeL.y))*/

        atkChar.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        atkChar.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        {
            while (TAAngleToRotate() < -5f || TAAngleToRotate() > +5f)
            {
                atkChar.GetComponent<Rigidbody2D>().AddForce((boss.transform.position - atkChar.transform.position).normalized * attackMomentum.magnitude);

                yield return null;
            }

            yield return new WaitForFixedUpdate();

            atkChar.GetComponent<Rigidbody2D>().AddForce(
                (boss.transform.position - atkChar.transform.position).normalized * 10 );
            tetheredAttack = false;
        }


    }

    private float TAAngleToRotate()//TA = tethered attack
    {
        Vector2 dfnDistance2Boss = boss.transform.position - dfnChar.transform.position;
        float dfnAngle2Boss = Mathf.Atan2(dfnDistance2Boss.y, dfnDistance2Boss.x) * Mathf.Rad2Deg;
        Vector2 dfnDistance2atk = atkChar.transform.position - dfnChar.transform.position;
        float dfnAngle2atk = Mathf.Atan2(dfnDistance2atk.y, dfnDistance2atk.x) * Mathf.Rad2Deg;

        return dfnAngle2Boss - dfnAngle2atk;
    }
    

    private void Awake()
    {
        _SP = this;
    }
   
    /// <summary>
    /// Momentum testing-Farhan
    /// </summary>
    private void Update()
    {
      //  MomentumCalc();
       // Debug.Log(momentum);
    }

  

}
