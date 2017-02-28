using UnityEngine;
using System.Collections;

public class KneeHipShperes : MonoBehaviour {


    public GameObject knee;
    public GameObject hip;
    public GameObject self;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        updateAroundKneeHip();
	}

    void updateAroundKneeHip()
    {
        self.transform.position = new Vector3 (knee.transform.position.x, hip.transform.position.y, knee.transform.position.z);
    }
} 
