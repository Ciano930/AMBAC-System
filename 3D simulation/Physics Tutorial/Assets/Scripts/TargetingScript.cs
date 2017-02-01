using UnityEngine;
using System.Collections;

public class TargetingScript: MonoBehaviour {

    public GameObject pointer;
    public GameObject Sphere;

    public GameObject targetSphere;

    public GameObject sourceJoint;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

        //first lets attach the rotation spheres to the joints
        Sphere.transform.position = sourceJoint.transform.position;

        //as sphere and pointer are the same connected object we just need to rotate sphere.
        Sphere.transform.rotation = Quaternion.LookRotation(targetSphere.transform.position - Sphere.transform.position);
    }
}
