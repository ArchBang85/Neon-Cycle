using UnityEngine;
using System.Collections;

public class F_GameController : MonoBehaviour {
    public GameObject player;
    public GameObject goal;
    public GameObject guideArrow;
    public GameObject[] Wheels = new GameObject[3];
    public float[] speeds = new float[3];
    public float[] enginePowers = new float[3];
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // rotate wheels

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

        // each wheel puts out a thrust wherever it's facing
        for (int e = 0; e < 3; e++)
        {
            player.transform.GetComponent<Rigidbody2D>().AddForce(-Wheels[e].transform.up * Time.deltaTime * enginePowers[e]);
        }


        // indicate to the player where the goal is


        //Transform wheelChild = unit.playerWheelTransform;
        Vector2 goalDir = goal.transform.position - player.transform.position;
        Quaternion guideRotation = Quaternion.LookRotation(Vector3.forward, goalDir);
        guideArrow.transform.rotation = guideRotation;


    }
}
