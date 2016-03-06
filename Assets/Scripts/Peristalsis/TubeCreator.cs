using UnityEngine;
using System.Collections;

public class TubeCreator : MonoBehaviour {

    public GameObject TubePart;
    public int parts = 12;
    // Use this for initialization
	void Start () {
	    for (int i = 0; i < parts; i++)
        {
            GameObject tp = (GameObject)Instantiate(TubePart, new Vector3(transform.position.x + (i * 0.5f), transform.position.y + Random.Range(-0.3f, 0.3f), 0), Quaternion.identity);
            tp.transform.eulerAngles = new Vector3(90, 0, 0);
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
