using UnityEngine;
using System.Collections;

public class TargetingScript: MonoBehaviour {

    public GameObject pointer;//the direction the sphere is pointing marked by this pointer
    public GameObject Sphere;// the sphere itself

    public GameObject targetSphere;//The sphere the pointer should be aiming at

    public GameObject sourceJoint;//the robot joint that this relates to
    public GameObject sourceSphere;//The kinect sphere that controls this target
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

        //here we can call either the spheres as the sources or the joints.
        UpdateAroundKinect();
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
