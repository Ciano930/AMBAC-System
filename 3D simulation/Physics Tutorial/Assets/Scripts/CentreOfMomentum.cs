using UnityEngine;
using System.Collections;

public class CentreOfMomentum : MonoBehaviour {

    //To find COM we must know the Mass of all elements of the robot. 
    //we're presuming that the mass of the robot is the same as the mass of a human (averaged between male and female) at 11 stone, 70kg

    public GameObject Robot;//the parent class and main gameObject for the full robot

    public GameObject Waist;

    
    public GameObject Torso;
    public GameObject midsection;

    public GameObject Legs;
    public GameObject right_leg;
    public GameObject left_leg;
    //These have been split into two parts to balance them correctly.
    public GameObject RThigh1;
    public GameObject LThigh1;
    public GameObject RThigh2;
    public GameObject LThigh2;

    public GameObject RShin;
    public GameObject LShin;
    public GameObject RFoot;
    public GameObject LFoot;

    public GameObject COM;

    public float weightMod;
    
    public Vector3 totalCOMPos;
    

    //HERE IS WHERE UPPER TORSO WILL GO WHEN WE NEED THEM.

    // Use this for initialization
    void Start () {


        //body segment Mass (in percents)
        //head 8.23
        //Thorax 18.56
        //Abdomen 12.65
        //pelvis 14.81
        //Upper Arm (shoulder/Bicep) 3.075
        //Forearm 1.72
        //Hand 0.575
        //Thigh 11.125
        //shin 5.05
        //foot 1.38

        //This gives us a 100.1 % range. a relative accuracy of need

        //Out of 70kg that leaves us with these values.
        //Head 5.761kg
        //Thorax 12.992kg
        //Abdomen 8.855kg
        //Pelvis 10.367kg
        //Upper Arm 2.1525kg
        //Forearm 1.204kg
        //Hand 0.4025kg
        //Thigh 7.7875kg
        //Shin 3.535kg
        //Foot 0.966kg


        //Now we assign these masses to the robot.

        Robot = GameObject.FindWithTag("Robot");
        Debug.Log("found Robot");

        Waist = Robot.transform.Find("waist").gameObject;
        Debug.Log("found Waist");

        Legs = Robot.transform.Find("Legs").gameObject;

        Torso = Robot.transform.Find("Torso").gameObject;

        midsection = Torso.transform.Find("midsection").gameObject;
        Debug.Log("found Waist");

        left_leg = Legs.transform.Find("left leg").gameObject;
        LThigh1 = left_leg.transform.Find("thigh").transform.Find("L leg2").gameObject;//Inner Thigh
        LThigh2 = left_leg.transform.Find("thigh").transform.Find("L leg3").gameObject;//Outer Thigh
        LShin = left_leg.transform.Find("shin").transform.Find("Shin L").gameObject;
        LFoot = left_leg.transform.Find("foot").transform.Find("Foot").gameObject;

        right_leg = Legs.transform.Find("right leg").gameObject;
        RThigh1 = right_leg.transform.Find("thigh").transform.Find("right leg3").gameObject;//Inner Thigh
        RThigh2 = right_leg.transform.Find("thigh").transform.Find("right leg2").gameObject;//Outer Thigh
        RShin = right_leg.transform.Find("shin").transform.Find("Shin R").gameObject;
        RFoot = right_leg.transform.Find("foot").transform.Find("Foot").gameObject;
        Debug.Log("found legs");

        weightMod = 10.0f;

        //now that the GameObjects are found we need to get their rigidbodies. TRYING THIS AT ACTUAL WEIGHTS CAUSE LIMBS TO BE CRUSHED will try at 1/10th.
        midsection.GetComponent<Rigidbody>().mass = 8.9f / weightMod;

        Waist.GetComponent<Rigidbody>().mass = 10.4f / weightMod;

        LThigh1.GetComponent<Rigidbody>().mass = (7.8f/2) / weightMod;
        RThigh1.GetComponent<Rigidbody>().mass = (7.8f / 2) / weightMod;
        LThigh2.GetComponent<Rigidbody>().mass = (7.8f / 2) / weightMod;
        RThigh2.GetComponent<Rigidbody>().mass = (7.8f / 2) / weightMod;

        LShin.GetComponent<Rigidbody>().mass = 3.5f / weightMod;
        RShin.GetComponent<Rigidbody>().mass = 3.5f / weightMod;
        LFoot.GetComponent<Rigidbody>().mass = 1.0f / weightMod;
        RFoot.GetComponent<Rigidbody>().mass = 1.0f / weightMod;

        /*
         After testing I found that this form doesn't work as the rigidbodies for the full legs do not play as nicely as I need them to. However there is a possible solution via splitting the weights across the relevant parts. 
         I will attempt this as a fix and hopefully can then move to actually adjusting the COM.
         */

        Debug.Log("Mass Assigned");

        COM = GameObject.Find("COM").gameObject;

    }

    // Update is called once per frame
    void Update () {

        //Now we can update the COM pos
        totalCOMPos.x = midsection.GetComponent<Rigidbody>().mass * midsection.transform.position.x +
                        Waist.GetComponent<Rigidbody>().mass * Waist.transform.position.x +
                        LThigh1.GetComponent<Rigidbody>().mass * LThigh1.transform.position.x +
                        LThigh2.GetComponent<Rigidbody>().mass * LThigh2.transform.position.x +
                        RThigh1.GetComponent<Rigidbody>().mass * RThigh1.transform.position.x +
                        RThigh2.GetComponent<Rigidbody>().mass * RThigh2.transform.position.x +
                        LShin.GetComponent<Rigidbody>().mass * LShin.transform.position.x +
                        RShin.GetComponent<Rigidbody>().mass * RShin.transform.position.x +
                        LFoot.GetComponent<Rigidbody>().mass * LFoot.transform.position.x +
                        RFoot.GetComponent<Rigidbody>().mass * RFoot.transform.position.x;
        totalCOMPos.y = midsection.GetComponent<Rigidbody>().mass * midsection.transform.position.y +
                        Waist.GetComponent<Rigidbody>().mass * Waist.transform.position.y +
                        LThigh1.GetComponent<Rigidbody>().mass * LThigh1.transform.position.y +
                        LThigh2.GetComponent<Rigidbody>().mass * LThigh2.transform.position.y +
                        RThigh1.GetComponent<Rigidbody>().mass * RThigh1.transform.position.y +
                        RThigh2.GetComponent<Rigidbody>().mass * RThigh2.transform.position.y +
                        LShin.GetComponent<Rigidbody>().mass * LShin.transform.position.y +
                        RShin.GetComponent<Rigidbody>().mass * RShin.transform.position.y +
                        LFoot.GetComponent<Rigidbody>().mass * LFoot.transform.position.y +
                        RFoot.GetComponent<Rigidbody>().mass * RFoot.transform.position.y;
        totalCOMPos.z = midsection.GetComponent<Rigidbody>().mass * midsection.transform.position.z +
                        Waist.GetComponent<Rigidbody>().mass * Waist.transform.position.z +
                        LThigh1.GetComponent<Rigidbody>().mass * LThigh1.transform.position.z +
                        LThigh2.GetComponent<Rigidbody>().mass * LThigh2.transform.position.z +
                        RThigh1.GetComponent<Rigidbody>().mass * RThigh1.transform.position.z +
                        RThigh2.GetComponent<Rigidbody>().mass * RThigh2.transform.position.z +
                        LShin.GetComponent<Rigidbody>().mass * LShin.transform.position.z +
                        RShin.GetComponent<Rigidbody>().mass * RShin.transform.position.z +
                        LFoot.GetComponent<Rigidbody>().mass * LFoot.transform.position.z +
                        RFoot.GetComponent<Rigidbody>().mass * RFoot.transform.position.z;

        //now we divide this by the number of parts
        totalCOMPos = totalCOMPos / 8.0f;

        COM.transform.position = totalCOMPos;
    }
}
