using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class DetectJoints : MonoBehaviour {

    public GameObject BodySrcManager;
    public JointType TrackedJoint;

    private BodySourceManager bodyManager;
    private Body[] pilots;

    public GameObject relevantPoint;

    public int multiplier = 10;

    // Use this for initialization
    void Start () {
        if(BodySrcManager == null)
        {
            Debug.Log("No Gameobject bodySrcManager Assigned!!");
        }
        else
        {
            bodyManager = BodySrcManager.GetComponent<BodySourceManager>();
        }
	}
	
	// Update is called once per frame
	void Update () {

        if(bodyManager == null)
        {
            Debug.Log("BodyManager is not available");
            return;
        }
        else
        {
            pilots = bodyManager.GetData();

            if(pilots == null)
            {
                return;
            }
            else
            {
                foreach (var pilot in pilots)
                {
                    if(pilot == null)
                    {
                        continue;
                    }
                    else
                    {
                        if(pilot.IsTracked)
                        {
                            Debug.Log("Tracking now" + pilot.Joints[TrackedJoint].Position.X);
                            var pos = pilot.Joints[TrackedJoint].Position;
                            gameObject.transform.position = new Vector3(pos.X * multiplier, (pos.Y * multiplier), pos.Z * multiplier);
                        }
                    }
                }
            }
        }
	
	}
}
