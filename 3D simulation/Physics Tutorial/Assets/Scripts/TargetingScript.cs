using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class TargetingScript: MonoBehaviour {

    public GameObject pointer;//the direction the sphere is pointing marked by this pointer
    public GameObject Sphere;// the sphere itself

    public GameObject targetSphere;//The sphere the pointer should be aiming at

    public GameObject sourceJoint;//the robot joint that this relates to
    public GameObject sourceSphere;//The kinect sphere that controls this target

    public GameObject BodySrcManager;
    private BodySourceManager bodyManager;
    private Body[] pilots;

    // Use this for initialization
    void Start () {
        if (BodySrcManager == null)
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
        if (bodyManager == null)
        {
            Debug.Log("BodyManager is not available");
            return;
        }
        else
        {
            pilots = bodyManager.GetData();

            if (pilots == null)
            {
                return;
            }
            else
            {
                foreach (var pilot in pilots)
                {
                    if (pilot == null)
                    {
                        continue;
                    }
                    else
                    {
                        if (pilot.IsTracked)
                        {
                            //here we can call either the spheres as the sources or the joints.
                            UpdateAroundKinect();
                        }
                    }
                }
            }
        }
    }

    void UpdateAroundJoints()
    {
        //first lets attach the rotation spheres to the joints
        Sphere.transform.position = sourceJoint.transform.position;
        //as sphere and pointer are the same connected object we just need to rotate sphere.
        Sphere.transform.rotation = Quaternion.LookRotation(targetSphere.transform.position - Sphere.transform.position);
    }

    void UpdateAroundKinect()
    {
        //first lets attach the rotation spheres to the joints
        Sphere.transform.position = sourceSphere.transform.position;
        //as sphere and pointer are the same connected object we just need to rotate sphere.
        Sphere.transform.rotation = Quaternion.LookRotation(targetSphere.transform.position - Sphere.transform.position);
    }
}
