using UnityEngine;
using System.Collections;

//DETAILS ON WAIST STRATEGY
//meant for minor inclinations the waist script is for fine motion to avoid large stutter.
//Limit of inclination is 0.5 in Unity
//After this point the AbsStrategy needs to take over.


public class WaistStrategy1 : MonoBehaviour {

    public GameObject Robot;//the parent class and main gameObject for the full robot
    public GameObject Hip;
    public GameObject Waist;

    public HipScript1 hipScript;

    public float angle;
    public float waistAngle;
    public bool falling;

    public float reactionStrength;

    // Use this for initialization
    void Start () {
        //first we need to define the Game object we're looking at
        Robot = GameObject.FindWithTag("Robot");
        Debug.Log("found Robot ");
        
        //now lets find the hips
        Hip = Robot.transform.Find("Hips").gameObject;
        Debug.Log("found Hips");

        //next we need to find the waist orientation block
        //rotation we are look at is to keep the torso block at 0 x rotation
        Waist= Robot.transform.Find("waist").gameObject;

        //now we find our hipscript
        hipScript = (HipScript1)Hip.GetComponent(typeof(HipScript1));
        Debug.Log("found HipScript");
        angle = 0.02f;

        falling = false;

        reactionStrength = 10.0f;
    }
	
	// Update is called once per frame
	void Update () {
        waistAngle = Waist.transform.rotation.x;
        //we need to now check the orientation of the waist block.
        if (Waist.transform.rotation.x > angle) 
        {
            //falling backwards
            falling = true;
            if(hipScript.LhipSwivelMotor.force < hipScript.LhipSwivelMotor.targetVelocity)
            {
                hipScript.LhipSwivelMotor.force = hipScript.LhipSwivelMotor.force + 10;
            }
            hipScript.LhipSwivelMotor.targetVelocity = 90;

            if (hipScript.RhipSwivelMotor.force < hipScript.RhipSwivelMotor.targetVelocity)
            {
                hipScript.RhipSwivelMotor.force = hipScript.RhipSwivelMotor.force + 10;
            }
            hipScript.RhipSwivelMotor.targetVelocity = 90;

            //Now we update our limits to return the joint to a balance position

            hipScript.LhipSwivelLimits.max = 180;
            hipScript.LhipSwivelLimits.min = 0;

            hipScript.RhipSwivelLimits.max = 180;
            hipScript.RhipSwivelLimits.min = 0;
        }
        else if (Waist.transform.rotation.x < -angle)
        {
            //falling forwards
            falling = true;

            if (hipScript.LhipSwivelMotor.force > hipScript.LhipSwivelMotor.targetVelocity)
            {
                hipScript.LhipSwivelMotor.force = hipScript.LhipSwivelMotor.force - 5;
            }
            hipScript.LhipSwivelMotor.targetVelocity =  - 90;

            if (hipScript.RhipSwivelMotor.force > hipScript.RhipSwivelMotor.targetVelocity)
            {
                hipScript.RhipSwivelMotor.force = hipScript.RhipSwivelMotor.force - 5;
            }
            hipScript.RhipSwivelMotor.targetVelocity =  - 90;

            //Now we update our limits to return the joint to a balance position

            hipScript.LhipSwivelLimits.max = 0;
            hipScript.LhipSwivelLimits.min = -180;

            hipScript.RhipSwivelLimits.max = 0;
            hipScript.RhipSwivelLimits.min = -180;
        }
        else
        {
            //Debug.Log("Not falling");
            falling = false;
            hipScript.LhipSwivelLimits.max = 0.0f;
            hipScript.LhipSwivelLimits.min = 0.0f;

            hipScript.RhipSwivelLimits.max = 0.0f;
            hipScript.RhipSwivelLimits.min = 0.0f;
        }
	}
}
