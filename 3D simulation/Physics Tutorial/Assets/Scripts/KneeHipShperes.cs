using UnityEngine;
using System.Collections;

public class KneeHipShperes : MonoBehaviour
{


    public GameObject reference;
    public GameObject staticpoint;
    public GameObject self;
    public string option;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch(option)
        {
            case "a":
            case "A":
                updateJointsA();
                break;
            case "b":
            case "B":
                updateJointsB();
                break;
        }
    }

    void updateJointsA()
    {
        self.transform.position = new Vector3(reference.transform.position.x, staticpoint.transform.position.y, reference.transform.position.z);
    }

    void updateJointsB()
    {
        self.transform.position = new Vector3(staticpoint.transform.position.x, reference.transform.position.y, reference.transform.position.z);
    }
}