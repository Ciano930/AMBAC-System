using UnityEngine;
using System.Collections;

public class AbStrategy : MonoBehaviour {

    public GameObject Robot;//the parent class and main gameObject for the full robot
    public GameObject Torso;
    public GameObject lowerAbs1;//The gameobject that holds the motor

    public float angle;
    public float waistAngle;
    public bool falling;

    public float reactionStrength;

    // Use this for initialization
    void Start () {

        //first we need to define the Game object we're looking at
        Robot = GameObject.FindWithTag("Robot");
        Debug.Log("found Robot ");

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
