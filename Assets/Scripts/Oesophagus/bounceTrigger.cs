using UnityEngine;
using System.Collections;

public class bounceTrigger : MonoBehaviour {

    public bool downwards = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
      
        if (collision.transform.tag == "Player")
        {
            if(downwards)
            {
                collision.transform.GetComponent<Rigidbody>().AddForce(-Vector3.up * 5.0f);
            }
            else
            {
                collision.transform.GetComponent<Rigidbody>().AddForce(Vector3.up * 5.0f);
            }
        }
    }
}
