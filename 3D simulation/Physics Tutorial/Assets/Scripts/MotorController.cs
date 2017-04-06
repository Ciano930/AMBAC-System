using UnityEngine;
using System.Collections;

public class MotorController : MonoBehaviour
{

    public HingeJoint jointHinge;
    public JointLimits jointLimit;
    public JointMotor jointMotor;

    public GameObject trackedPoint;
    public GameObject trackedSource;
    public GameObject trackedPoint2; //needed for distance of the knee.
    public GameObject hingeSource;

    public float angleFromZero;
    public float realAngle;
    public int pin;
    //public float time;

    private float calibratedZeroAngle;

    private bool calibrated;

    public char option;
    private bool run;

    private float sideA;
    private float sideB;
    private float sideC;

    private float timer;
    private float timeMax;
    public float interval;

    public bool sentMessage;
    //now we need to call our connection script

    public SocketToRPI socketScript;

    //The main logic for these will change depending on which of these motors we are using and so a value will be set ahead of time.
    // Use this for initialization
    void Start()
    {
        timeMax = 0.2f;//runs every .2 of a second (5 times a second)
        timer = timer + interval;

        if (option == 'X') // will Either be a an X, Y
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
        if (run)
        {

            timer += Time.deltaTime;
            UpdateMotor();
                
        }
    }

    void UpdateMotor()
    {
        //if (Input.GetKeyDown("space"))
        if(Input.anyKey)
        {
            calibrate();
        }

        if (calibrated)
        {
            //we need first the three distances of the joints. 
            //from Knee to Hip (A)
            sideA = Vector3.Distance(trackedSource.transform.position, trackedPoint2.transform.position);
            //from Knee to Ankle (B)
            sideB = Vector3.Distance(trackedSource.transform.position, trackedPoint.transform.position);
            //from Ankle to hip (C)
            sideC = Vector3.Distance(trackedPoint.transform.position, trackedPoint2.transform.position);

            //now we use the SSS triangle equation for angle
            //angle =( mathf.acos((A*A)+(B*B)-(C*C) / (2*A*B)) ) *  mathf.Rad2Deg
            realAngle = Mathf.Acos(((sideA * sideA) + (sideB * sideB) - (sideC * sideC) ) / (2 * sideA * sideB)) * Mathf.Rad2Deg;
            

            //Now we have the correct angle next step is t o update the motor

            updateMotorAngle();
        }

    }

    void universalHingeUpdateMotor()
    {
        if (Input.anyKey)
        {
            calibrate();
        }
        
    }

    void calibrate()
    {
        Debug.Log("Calibrating");
        //this will be the zero position for our servo's and all motion from here will be an angle calculated using dot product

        //we need first the three distances of the joints. 
        //(A)
        sideA = Vector3.Distance(trackedSource.transform.position, trackedPoint2.transform.position);
        //(B)
        sideB = Vector3.Distance(trackedSource.transform.position, trackedPoint.transform.position);
        //(C)
        sideC = Vector3.Distance(trackedPoint.transform.position, trackedPoint2.transform.position);

        //now we use the SSS triangle equation for angle
        //angle =( mathf.acos((A*A)+(B*B)-(C*C) / (2*A*B)) ) *  mathf.Rad2Deg
        calibratedZeroAngle = Mathf.Acos(((sideA * sideA) + (sideB * sideB) - (sideC * sideC)) / (2 * sideA * sideB)) * Mathf.Rad2Deg;

        calibrated = true;
    }

    void updateMotorAngle()
    {
        angleFromZero = calibratedZeroAngle - realAngle;

        if (timer > timeMax)
        {
           socketScript.sendAngle(pin, angleFromZero);
           timer = 0.0f;
        }

        //for simulation calibration
        jointLimit.max = angleFromZero+1;
        jointLimit.min = angleFromZero-1;

        jointHinge.limits = jointLimit;
    }
}
