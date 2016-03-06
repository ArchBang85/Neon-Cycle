using UnityEngine;
using System.Collections;

public class S_GameController : MonoBehaviour {
    public GameObject[] Wheels = new GameObject[3];
    public float[] speeds = new float[3];
    public int piecesEaten = 0;
	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
	

        // Keyboard controls
        if (Input.GetKey(KeyCode.Q))
        {
            Wheels[0].transform.Rotate(Vector3.forward, Time.deltaTime * speeds[0]);
        }
        if (Input.GetKey(KeyCode.W))
        {
            Wheels[0].transform.Rotate(Vector3.forward, -Time.deltaTime * speeds[0]);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Wheels[1].transform.Rotate(Vector3.forward, Time.deltaTime * speeds[1]);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Wheels[1].transform.Rotate(Vector3.forward, -Time.deltaTime * speeds[1]);
        }
        if (Input.GetKey(KeyCode.Z))
        {
            Wheels[2].transform.Rotate(Vector3.forward, Time.deltaTime * speeds[2]);
        }
        if (Input.GetKey(KeyCode.X))
        {
            Wheels[2].transform.Rotate(Vector3.forward, -Time.deltaTime * speeds[2]);
        }
	}
}
