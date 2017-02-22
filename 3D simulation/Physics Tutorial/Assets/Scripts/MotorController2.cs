using UnityEngine;
using System.Collections;

public class MotorController2 : MonoBehaviour
{

    public GameObject jointPositioner;
    public GameObject targetingPointer;

    public HingeJoint jointHinge;
    public JointLimits jointLimit;
    public JointMotor jointMotor;

    public float calibratedZeroAngle;
    private float targetRot;

    private bool calibrated;

    public char option;
    private bool run;

    //The main logic for these will change depending on which of these motors we are using and so a value will be set ahead of time.
    // Use this for initialization
    void Start()
    {
        if (option == 'X' || option == 'Y' || option == 'Z') // will Either be a an X, Y or Z Joint
        {
            run = true;
            calibrated = false;

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
    void Update()
    {


        //we need to know which of the axis' of rotation will affect our motor as some targeting spheres affect multiple motors.
        if (run)
        {
            switch (option)
            {
                case 'X'://X axis
                    XaxisHingeUpdateMotor();
                    break;
                case 'Y'://Y axis
                    YaxisHingeUpdateMotor();
                    break;
                case 'Z'://Z axis
                    ZaxisHingeUpdateMotor();
                    break;
            }
        }

    }

    void XaxisHingeUpdateMotor()
    {

        if (Input.anyKey)
        {
            Debug.Log("Calibrating");
            calibratedZeroAngle = targetingPointer.transform.eulerAngles.x;
            calibrated = true;
            Debug.Log("Calibrating at : " + calibratedZeroAngle);
        }
        //Now we have the 0 point we can see how far the angle is from that point and depending if it is a positive action or negative

        if (jointHinge.angle < (targetingPointer.transform.eulerAngles.x - calibratedZeroAngle) && calibrated)
        {
            jointLimit.min = (targetingPointer.transform.eulerAngles.x - calibratedZeroAngle);
            jointLimit.max = (targetingPointer.transform.eulerAngles.x - calibratedZeroAngle);
            jointMotor.targetVelocity = 90;
            jointMotor.force = 30;
            jointHinge.limits = jointLimit;

            jointHinge.motor = jointMotor;

            //Debug.Log("less than at X: " + targetingPointer.transform.eulerAngles.x);

        }
        else if (jointHinge.angle > (targetingPointer.transform.eulerAngles.x - calibratedZeroAngle) && calibrated)
        {
            //No such thing as negative on a knee joint so will test first
            jointLimit.min = (targetingPointer.transform.eulerAngles.x - calibratedZeroAngle);
            jointMotor.targetVelocity = 90;
            jointMotor.force = 30;
            jointHinge.limits = jointLimit;

            jointHinge.motor = jointMotor;

        }
    }


    void YaxisHingeUpdateMotor()
    {

        if (Input.anyKey)
        {
            Debug.Log("Calibrating");
            calibratedZeroAngle = targetingPointer.transform.eulerAngles.y;
            calibrated = true;
            Debug.Log("Calibrating at : " + calibratedZeroAngle);
        }
        //Now we have the 0 point we can see how far the angle is from that point and depending if it is a positive action or negative

        if (jointHinge.angle < (targetingPointer.transform.eulerAngles.y - calibratedZeroAngle) && calibrated)
        {
            jointLimit.min = (targetingPointer.transform.eulerAngles.y - calibratedZeroAngle);
            jointLimit.max = (targetingPointer.transform.eulerAngles.y - calibratedZeroAngle);
            jointMotor.targetVelocity = 90;
            jointMotor.force = 30;
            jointHinge.limits = jointLimit;

            jointHinge.motor = jointMotor;

            //Debug.Log("less than at Y: " + targetingPointer.transform.eulerAngles.y);

        }
        else if (jointHinge.angle > (targetingPointer.transform.eulerAngles.y - calibratedZeroAngle) && calibrated)
        {
            //No such thing as negative on a knee joint so will test first
            jointLimit.min = (targetingPointer.transform.eulerAngles.y - calibratedZeroAngle);
            jointMotor.targetVelocity = 90;
            jointMotor.force = 30;
            jointHinge.limits = jointLimit;

            jointHinge.motor = jointMotor;

        }
    }

    void ZaxisHingeUpdateMotor()
    {

        if (Input.anyKey)
        {
            Debug.Log("Calibrating");
            calibratedZeroAngle = targetingPointer.transform.eulerAngles.z;
            calibrated = true;
            Debug.Log("Calibrating at : " + calibratedZeroAngle);
        }
        //Now we have the 0 point we can see how far the angle is from that point and depending if it is a positive action or negative

        if (jointHinge.angle < (targetingPointer.transform.eulerAngles.z - calibratedZeroAngle) && calibrated)
        {
            jointLimit.min = (targetingPointer.transform.eulerAngles.z - calibratedZeroAngle);
            jointLimit.max = (targetingPointer.transform.eulerAngles.z - calibratedZeroAngle);
            jointMotor.targetVelocity = 90;
            jointMotor.force = 30;
            jointHinge.limits = jointLimit;

            jointHinge.motor = jointMotor;

            //Debug.Log("less than at z: " + targetingPointer.transform.eulerAngles.x);

        }
        else if (jointHinge.angle > (targetingPointer.transform.eulerAngles.z - calibratedZeroAngle) && calibrated)
        {
            //No such thing as negative on a knee joint so will test first
            jointLimit.min = (targetingPointer.transform.eulerAngles.z - calibratedZeroAngle);
            jointMotor.targetVelocity = 90;
            jointMotor.force = 30;
            jointHinge.limits = jointLimit;

            jointHinge.motor = jointMotor;

        }
    }
}




