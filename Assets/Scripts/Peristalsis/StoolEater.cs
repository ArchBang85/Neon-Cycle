using UnityEngine;
using System.Collections;

public class StoolEater : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnColliderHit(Collision hit)
    {

        if (hit.gameObject.tag == "Stool")
        {
            Destroy(hit.gameObject, 0.2f);
            Debug.Log("Destroying Stool");
        }

    }
}
