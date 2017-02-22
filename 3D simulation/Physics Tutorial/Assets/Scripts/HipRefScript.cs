using UnityEngine;
using System.Collections;

public class HipRefScript : MonoBehaviour {


    public GameObject trackedPoint;
    public GameObject trackedSource;
    public GameObject trackedPoint2;

    private float sideA;
    private float sideB;
    private float sideC;

    private float realAngle;
    private float difference;


    // Use this for initialization
    void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {
        //we need first the three distances of the joints. 
        //from Hip To Hip
        sideA = Vector3.Distance(trackedSource.transform.position, trackedPoint2.transform.position);
        //from HipSource to Ref
        sideB = Vector3.Distance(trackedSource.transform.position, trackedPoint.transform.position);
        //from Hip to Ref
        sideC = Vector3.Distance(trackedPoint.transform.position, trackedPoint2.transform.position);

        //now we use the SSS triangle equation for angle
        //angle =( mathf.acos((A*A)+(B*B)-(C*C) / (2*A*B)) ) *  mathf.Rad2Deg
        realAngle = Mathf.Acos(((sideA * sideA) + (sideB * sideB) - (sideC * sideC)) / (2 * sideA * sideB)) * Mathf.Rad2Deg;

        if (realAngle != 90)
        {
            difference = 90 - realAngle;
            trackedSource.transform.Rotate(Vector3.down * difference);//rotates difference
        }
    }
}
