using UnityEngine;
using System.Collections;

public class WaistStrategy1 : MonoBehaviour {

    public GameObject Robot;//the parent class and main gameObject for the full robot
    public GameObject Hip;
    public GameObject WaistPivot;

    public HipScript1 hipScript;

    public float angle;


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
        WaistPivot = Robot.transform.Find("waist pivot").gameObject;

        //now we find our hipscript
        hipScript = (HipScript1)Hip.GetComponent(typeof(HipScript1));
        Debug.Log("found HipScript");
        angle = 3.0f;
    }
	
	// Update is called once per frame
	void Update () {

        //we need to now check the orientation of the waist block.
        if (WaistPivot.transform.rotation.x > angle) 
        {
            hipScript.LhipSwivelMotor.force = 90;
            hipScript.LhipSwivelMotor.targetVelocity = 90;
            hipScript.RhipSwivelMotor.force = 90;
            hipScript.RhipSwivelMotor.targetVelocity = 90;

            //Now we update our limits to return the joint to a balance position

            hipScript.LhipSwivelLimits.max = hipScript.LhipSwivelLimits.max + 0.5f;
            hipScript.LhipSwivelLimits.max = hipScript.LhipSwivelLimits.min + 0.5f;

            hipScript.RhipSwivelLimits.max = hipScript.RhipSwivelLimits.max + 0.5f;
            hipScript.RhipSwivelLimits.max = hipScript.RhipSwivelLimits.min + 0.5f;
        }
        else if (WaistPivot.transform.rotation.x < -angle)
        {
            hipScript.LhipSwivelMotor.force = 90;
            hipScript.LhipSwivelMotor.targetVelocity = -90;
            hipScript.RhipSwivelMotor.force = 90;
            hipScript.RhipSwivelMotor.targetVelocity = -90;

            //Now we update our limits to return the joint to a balance position

            hipScript.LhipSwivelLimits.max = hipScript.LhipSwivelLimits.max - 0.5f;
            hipScript.LhipSwivelLimits.max = hipScript.LhipSwivelLimits.min - 0.5f;

            hipScript.RhipSwivelLimits.max = hipScript.RhipSwivelLimits.max - 0.5f;
            hipScript.RhipSwivelLimits.max = hipScript.RhipSwivelLimits.min - 0.5f;
        }
        else
        {
            hipScript.LhipSwivelLimits.max = 0.0f;
            hipScript.LhipSwivelLimits.min = 0.0f;

            hipScript.RhipSwivelLimits.max = 0.0f;
            hipScript.RhipSwivelLimits.min = 0.0f;
        }
	}
}
