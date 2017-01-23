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
    public GameObject RThigh;
    public GameObject LThigh;
    public GameObject RShin;
    public GameObject LShin;
    public GameObject RFoot;
    public GameObject LFoot;
    

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

        midsection = Robot.transform.Find("midsection").gameObject;
        Debug.Log("found Waist");

        left_leg = Legs.transform.Find("left leg").gameObject;
        LThigh = left_leg.transform.Find("thigh").gameObject;
        LShin = left_leg.transform.Find("shin").gameObject;
        LFoot = left_leg.transform.Find("foot").gameObject;

        right_leg = Legs.transform.Find("right leg").gameObject;
        RThigh = right_leg.transform.Find("thigh").gameObject;
        RShin = right_leg.transform.Find("shin").gameObject;
        RFoot = right_leg.transform.Find("foot").gameObject;
        Debug.Log("found legs");

        //now that the GameObjects are found we need to get their rigidbodies.
        midsection.GetComponent<Rigidbody>().mass = 8.9f;

        Waist.GetComponent<Rigidbody>().mass = 10.4f;

        LThigh.GetComponent<Rigidbody>().mass = 7.8f;
        RThigh.GetComponent<Rigidbody>().mass = 7.8f;
        LShin.GetComponent<Rigidbody>().mass = 3.5f;
        RShin.GetComponent<Rigidbody>().mass = 3.5f;
        LFoot.GetComponent<Rigidbody>().mass = 1.0f;
        RFoot.GetComponent<Rigidbody>().mass = 1.0f;

        Debug.Log("Mass Assigned");
        //Now that we have found all the relevant parts we need to assign them their Mass.

    }

    // Update is called once per frame
    void Update () {
	
	}
}
