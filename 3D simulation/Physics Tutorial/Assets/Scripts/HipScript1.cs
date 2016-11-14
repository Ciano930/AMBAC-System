using UnityEngine;
using System.Collections;

public class HipScript1 : MonoBehaviour {

    public GameObject Robot;//the parent class and main gameObject for the full robot
    public GameObject Hip;

    public GameObject LHip;
    public GameObject LHipHinge1;
    public GameObject LHipHinge2;
    public GameObject LHipHinge3;

    public GameObject RHip;
    public GameObject RHipHinge1;
    public GameObject RHipHinge2;
    public GameObject RHipHinge3;

    public HingeJoint LhipSwivel;
    public HingeJoint LhipHinge;
    public HingeJoint LthighSwivel;

    public HingeJoint RhipSwivel;
    public HingeJoint RhipHinge;
    public HingeJoint RthighSwivel;
    
    public JointMotor LhipSwivelMotor;
    public JointMotor LhipHingeMotor;
    public JointMotor LthighSwivelMotor;

    public JointMotor RhipSwivelMotor;
    public JointMotor RhipHingeMotor;
    public JointMotor RthighSwivelMotor;

    // Use this for initialization
    void Start()
    {
        //first we need to define the Game object we're looking at

        Robot = GameObject.FindWithTag("Robot");
        Debug.Log("found Robot ");

        //made mistake of not finding the Hips at all! Will do that and then overwrite itself.
        Hip = Robot.transform.Find("Hips").gameObject;
        Debug.Log("found Hips");

        setUpLeft();//Calling the left set up function

        //Now we repeat the set up for the right side
        setUpRight();
    }


    void setUpLeft(){

        //Here we will set up the left

        //The left Hip setup
        LHip = Hip.transform.Find("Hipleft").gameObject;
        //now we make sure we have found that hip assembly
        if (LHip != null)
        {
            //now lets assign the Hinges gameObjects
            LHipHinge1 = LHip.transform.Find("hipleftHinge1").gameObject;
            LHipHinge2 = LHip.transform.Find("hipleftHinge2").gameObject;
            LHipHinge3 = LHip.transform.Find("hipleftHinge3").gameObject;

            //Now as before lets do some debugging and ensure they have been located.
            if (LHipHinge1 != null && LHipHinge2 != null && LHipHinge3 != null)
            {
                //Now we need to do our final assignments which are the hinges themselves

                //Hinge1 Hip swivel
                LhipSwivel = LHipHinge1.GetComponent<HingeJoint>();
                LhipSwivelMotor = LhipSwivel.motor;

                //Hinge2 Hip Hinge
                LhipHinge = LHipHinge2.GetComponent<HingeJoint>();
                LhipHingeMotor = LhipHinge.motor;

                //Hinge3 Thigh Swivel
                LthighSwivel = LHipHinge3.GetComponent<HingeJoint>();
                LthighSwivelMotor = LthighSwivel.motor;

                //Now we do our last little debug

                if (LhipSwivel != null && LhipHinge != null && LthighSwivel != null)
                {
                    Debug.Log("Left Hip fully assigned");
                }
                else
                {
                    Debug.Log("Left Hinge Joints not found");
                }
            }
            else
            {
                Debug.Log("Left Hip Hinges not found");
            }
        }
        else
        {
            Debug.Log("Hip not found");
        }
    }
    
    void setUpRight(){
        //Here we will set up the right

        //The left Hip setup
        RHip = Hip.transform.Find("Hipright").gameObject;
        //now we make sure we have found that hip assembly
        if (RHip != null)
        {
            //now lets assign the Hinges gameObjects
            RHipHinge1 = RHip.transform.Find("hipRightHinge1").gameObject;
            RHipHinge2 = RHip.transform.Find("hipRightHinge2").gameObject;
            RHipHinge3 = RHip.transform.Find("hipRightHinge3").gameObject;

            //Now as before lets do some debugging and ensure they have been located.
            if (RHipHinge1 != null && RHipHinge2 != null && RHipHinge3 != null)
            {
                //Now we need to do our final assignments which are the hinges themselves

                //Hinge1 Hip swivel
                RhipSwivel = RHipHinge1.GetComponent<HingeJoint>();
                RhipSwivelMotor = RhipSwivel.motor;

                //Hinge2 Hip Hinge
                RhipHinge = RHipHinge2.GetComponent<HingeJoint>();
                RhipHingeMotor = RhipHinge.motor;

                //Hinge3 Thigh Swivel
                RthighSwivel = RHipHinge3.GetComponent<HingeJoint>();
                RthighSwivelMotor = RthighSwivel.motor;

                //Now we do our last little debug

                if (RhipSwivel != null && RhipHinge != null && RthighSwivel != null)
                {
                    Debug.Log("Left Hip fully assigned");
                }
                else
                {
                    Debug.Log("Left Hinge Joints not found");
                }
            }
            else
            {
                Debug.Log("Left Hip Hinges not found");
            }
        }
        else
        {
            Debug.Log("Hip not found");
        }
    }








    //=================================================================================================================================
    //============================= Now that we have finished the set up and everything is assigned lets try and have the legs Move.
    //=================================================================================================================================



}
