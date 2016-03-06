using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

    public float rotationSpeed = 2.0f;
    public bool randomiseSpeed = false;
    public enum dirEnum { forward, up, right};
    public dirEnum dir = dirEnum.forward;
	// Use this for initialization
	void Start () {
	    if(randomiseSpeed)
        {
            rotationSpeed = Random.Range(-rotationSpeed, rotationSpeed);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (dir == dirEnum.forward)
        {
            this.transform.Rotate(Vector3.forward, Time.deltaTime * rotationSpeed);
        } else if(dir == dirEnum.right) {
            this.transform.Rotate(Vector3.right, Time.deltaTime * rotationSpeed);
        } else if (dir == dirEnum.up)
        {
            this.transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
        }
	}
}
