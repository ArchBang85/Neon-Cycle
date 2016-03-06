using UnityEngine;
using System.Collections;

public class EndTrigger : MonoBehaviour {

    public GameObject AcidEffect; 
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject newAcidEffect = (GameObject)Instantiate(AcidEffect, collision.transform.position, Quaternion.identity);
        Destroy(newAcidEffect, 3.0f);
        if (collision.transform.tag == "other")
        {

            Destroy(collision.gameObject, 0.01f);

        }
        if (collision.transform.tag == "Player")
        {
            // reload level

            Destroy(collision.gameObject, 0.5f);
            GameObject.Find("Main Camera").GetComponent<O_GameController>().RestartLevelWithDelay(3.0f);
        }
    }
    
    
  }