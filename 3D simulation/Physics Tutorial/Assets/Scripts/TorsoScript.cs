using UnityEngine;
using System.Collections;

public class TorsoScript : MonoBehaviour {

    public GameObject Robot;//the parent class and main gameObject for the full robot
    public GameObject Torso;

    public GameObject lowerAbs1;
    public GameObject lowerAbs2;
    public GameObject upperAbs1;
    public GameObject upperAbs2;

    public GameObject waistPivot;//For now we will work without the ability to rotate the waist. 

    public HingeJoint LowerAbs1Joint;
    public JointLimits LowerAbs1Limits;

    public HingeJoint LowerAbs2Joint;
    public JointLimits LowerAbs2Limits;

    public HingeJoint UpperAbs1Joint;
    public JointLimits UpperAbs1Limits;

    public HingeJoint UpperAbs2Joint;
    public JointLimits UpperAbs2Limits;

    public JointMotor LowerAbs1Motor;
    public JointMotor LowerAbs2Motor;

    public JointMotor UpperAbs1Motor;
    public JointMotor UpperAbs2Motor;

    // Use this for initialization
    void Start()
    {
        //first we need to define the Game object we're looking at

        Robot = GameObject.FindWithTag("Robot");
        Debug.Log("found Robot ");

        //made mistake of not finding the Hips at all! Will do that and then overwrite itself.
        Torso = Robot.transform.Find("Torso").gameObject;
        Debug.Log("found Torso");

        //Here we will set up the lowerAbs
        
        lowerAbs1 = Torso.transform.Find("lowerAbs1").gameObject;
        upperAbs1 = Torso.transform.Find("upperAbs1").gameObject;
        lowerAbs2 = Torso.transform.Find("lowerAbs2").gameObject;
        upperAbs2 = Torso.transform.Find("upperAbs2").gameObject;
        //now we make sure we have found that hip assembly
        if (lowerAbs1 != null && lowerAbs2 != null && upperAbs1 != null && upperAbs2 != null)
        {
            //Now we need to do our assignments which are the joint Hinges

            //==========LOWER ABS=============//
            LowerAbs1Joint = lowerAbs1.GetComponent<HingeJoint>();
            LowerAbs1Motor = LowerAbs1Joint.motor;
            //Next we need to set up the Motor action
            setUpMotor(LowerAbs1Joint, LowerAbs1Motor);

            //Now we set up our joint limits
            LowerAbs1Limits = LowerAbs1Joint.limits;

            //Hinge2 lower
            LowerAbs2Joint = lowerAbs2.GetComponent<HingeJoint>();
            LowerAbs2Motor = LowerAbs2Joint.motor;
            //Next we need to set up the Motor action
            setUpMotor(LowerAbs2Joint, LowerAbs2Motor);

            //Now we set up our joint limits
            LowerAbs2Limits = LowerAbs2Joint.limits;

            //==========UPPER ABS=============//
            UpperAbs1Joint = upperAbs1.GetComponent<HingeJoint>();
            UpperAbs1Motor = UpperAbs1Joint.motor;
            //Next we need to set up the Motor action
            setUpMotor(UpperAbs1Joint, UpperAbs1Motor);

            //Now we set up our joint limits
            UpperAbs1Limits = UpperAbs1Joint.limits;

            //Hinge2 upper 
            /*=================================NOT NEEDED UNTIL UPPER BODY IS ADDED====================
            UpperAbs2Joint = upperAbs2.GetComponent<HingeJoint>();
            UpperAbs2Motor = UpperAbs2Joint.motor;
            //Next we need to set up the Motor action
            setUpMotor(UpperAbs2Joint, UpperAbs2Motor);

            //Now we set up our joint limits
            UpperAbs2Limits = UpperAbs2Joint.limits;
            //Now we do our last little debug

            if (UpperAbs2Joint != null && UpperAbs1Joint != null && LowerAbs2Joint != null && LowerAbs1Joint != null)
            {
                Debug.Log("Abs fully assigned");
            }
            else
            {
                Debug.Log("Ab Joints not found");
            }*/
        }
        else
        {
           Debug.Log("Ab objects not found");
        }
    }
    
    //Here we set up the motors on the hinges. Next I will alter this to include the starting degree point but for now it is 90
    void setUpMotor(HingeJoint hinge, JointMotor motor)
    {
        motor.force = 0;
        motor.targetVelocity = 0;
        motor.freeSpin = false;
        hinge.motor = motor;
        hinge.useMotor = true;
    }

    void Update()
    {
        // Debug.Log("RHipHinge1 Max:" + RhipSwivelLimits.max + " Min:" + RhipSwivelLimits.min);
        Debug.Log("LowerAbs1 force:" + LowerAbs1Motor.force + " targetVelocity:" + LowerAbs1Motor.targetVelocity);

        LowerAbs1Joint.limits = LowerAbs1Limits;
        LowerAbs2Joint.limits = LowerAbs1Limits;

        LowerAbs1Joint.motor = LowerAbs1Motor;
        LowerAbs2Joint.motor = LowerAbs2Motor;

    }
}