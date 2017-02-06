using UnityEngine;
using System.Collections;

public class MotorController : MonoBehaviour {

    public GameObject jointPositioner;
    public GameObject targetingPointer;

    public HingeJoint jointHinge;
    public JointLimits jointLimit;
    public JointMotor jointMotor;

    public float rotDifference;
    private float targetRot;

    public int option;
    private bool run;

    //The main logic for these will change depending on which of these motors we are using and so a value will be set ahead of time.
    // Use this for initialization
    void Start()
    {
        if (option == 1 || option == 2) // will Either be a single Joint like the knee or elbow or a universal joint like the shoulder or hip 
        {
            run = true;

            //now lets set up the motor
            jointMotor.force = 90;
            jointMotor.targetVelocity = 0;
            jointMotor.freeSpin = false;
            jointHinge.motor = jointMotor;
            jointHinge.useMotor = true;

            jointHinge.limits = jointLimit;
        }
        else
        {
            Debug.Log(option + " is not a valid option");
            run = false;
        }
    }

	
	// Update is called once per frame
	void Update () {

        if(run)
        {
            switch(option)
            {
                case 1:
                    SingleHingeUpdateMotor();
                    break;
                case 2:
                    break;
            }
        }
	
	}

    void SingleHingeUpdateMotor()
    {
        //first we need the actual angle from the joint from the Quaternion rotation.
        float angle = targetingPointer.transform.eulerAngles.x;

        Debug.Log("angle:" + angle);
        if(angle > 180)
        {
            //angle = angle - 360;
        }

        jointLimit.min = -angle;
        jointMotor.targetVelocity = -90;
        jointHinge.limits = jointLimit;

        jointHinge.motor = jointMotor;
    }


    void UniversalJointUpdateMotor()
    {

    }
}
