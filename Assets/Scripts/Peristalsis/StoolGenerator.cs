using UnityEngine;
using System.Collections;

public class StoolGenerator : MonoBehaviour {

    public GameObject Stool;
    private float counter = 1.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        counter -= Time.deltaTime;
	    if (counter < 0)
        {
            counter = Random.Range(0.1f, 0.5f);
            GameObject s = (GameObject)Instantiate(Stool, new Vector3(transform.position.x, transform.position.y + Random.Range(-2.0f, 2.0f), 0), Quaternion.identity);
            s.GetComponent<Rigidbody>().AddForce(new Vector3(120.0f, 0, 0));
            s.transform.eulerAngles = new Vector3(90, 0, 0);
            s.transform.SetParent(GameObject.Find("Stools").transform);
        }
	}
}
