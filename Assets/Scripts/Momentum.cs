using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momentum : MonoBehaviour
{
    // note to self, is there any way to still apply spinning and decreasing when releasing button?

        //index shows us in which set of ranges of momentum our current momentum is
    int index = 0;
    // the previous index
    int oldindex;
    public SinglePlayer singleplayer;
    // same as the torque value set as SinglePlayer script
    public float IntialTorque; 
    // rate for torque to increase
    public float TorqueInscreaseSpeed;
    float momentum;
    // x,y=upaperbound & lowebound of momentum , z= damage value
    [Tooltip("x and y for range of momentum z ,for damage")]
    public Vector3[] Ranges = new Vector3[5];
    //float for how long the user is pressing
    public float timePressed;
    //for checking if user is pressing the button
    public bool buttonpressed = false;
    public AudioSource source;
    [Space]
    public UnityEngine.UI.Image momentumBar;
    public AudioClip Swingswoosh;
    // Start is called before the first frame update
   
    void Start()
    {
        IntialTorque = singleplayer.torque;
        source.clip = Swingswoosh;
       
    }

    // Update is called once per frame
    void Update()
    {
        // calculates momentum in update
        MomentumCalc();
        Debug.Log( "Range level " + GetRangeIndex() + " momentum " + momentum);
        momentumBar.fillAmount = momentum / Ranges[4].x;



        //Debug.Log("RANGE IS " + GetRangeIndex() + " Momentum " + momentum + " Velocity " + singleplayer.freeChar.GetComponent<Rigidbody2D>().velocity.magnitude );
        // setting torque depending on the range
        //singleplayer.torque = Ranges[GetRangeIndex()].z; 

        // can set players damage by acessing Ranges[GetRangeIndex()].z

        if ( Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) && SinglePlayer.SP.pivot != null)
        {
            //Debug.Log("pressed");
            buttonpressed = true;
          
             
            
        }
        else
        {
            buttonpressed = false;
            //source.Stop();
            
            timePressed = 0;

        }
        if (buttonpressed)
        {
            
            AddTorque();
            if (!source.isPlaying)
            {
                source.PlayOneShot(Swingswoosh);
            }
        }

       // float Horizontal = Input.GetAxis("Horizontal");
        //float Vertical = Input.GetAxis("Vertical");
    

        

    }

    void AddTorque()
    {

        
        timePressed += Time.deltaTime;
        singleplayer.torque = timePressed * TorqueInscreaseSpeed + IntialTorque;

    }
  
    void MomentumCalc()
    { var freechar = singleplayer.freeChar;

        if (freechar == null)
        {
            return;

        }
        Rigidbody2D r =  freechar.GetComponent<Rigidbody2D>();
        // Debug.Log(r.velocity.magnitude);

        float mass = r.mass;
        float velocity = r.mass * r.velocity.magnitude;
        momentum = mass * velocity;

       // Debug.Log("Momentum" + momentum);
    }

    public int GetRangeIndex() // finds where the value is in the array, and gets the index, updates old index
    {
      

        for(int i =0; i < Ranges.Length; i++)
        {

            if (momentum >= Ranges[i].x && momentum <= Ranges[i].y)
            {

                oldindex = index;
                index = i;
                

            }


        }

        return index;

    }

    // dont need now, script that returns true if momentum changes range
   bool Dirty
    {
       

        get
        {
            if (oldindex == index)
            {

                return false;
            }

            return true;
        }
    }


    
}
