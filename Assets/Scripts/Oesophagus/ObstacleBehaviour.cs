using UnityEngine;
using System.Collections;

public class ObstacleBehaviour : MonoBehaviour {
    GameObject player;
    public enum obstacleType { ship, wall };
    public obstacleType myObstacleType;
    public GameObject AcidEffect;
    float c = 3.0f;
    float followingForce;
    public Material RotatorGreen;

    void Start() {
        player = GameObject.Find("Character");
        followingForce = Random.Range(0.6f, 1.2f);

    }

    void Update() {
        if (myObstacleType == obstacleType.ship)
        {
            // home in for a few seconds
            c -= Time.deltaTime;
            if (c > 0)
            {
                try
                {
                    transform.GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position) * Time.deltaTime * followingForce);
                }
                catch
                {

                }
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {

        if (collision.transform.tag == "shield")
        {
            if (myObstacleType == obstacleType.ship)
            {
                transform.GetComponent<Rigidbody>().AddForce((transform.position - collision.transform.position) * 25);

                GameObject.Find("Main Camera").GetComponent<O_GameController>().levelScore += 2;

                this.GetComponent<TrailRenderer>().enabled = true;
                this.GetComponent<Renderer>().material = RotatorGreen;

                // change the layer so that the incoming ship won't recollide with the player
                this.gameObject.layer = 14;
                this.transform.tag = "Untagged";

                // Force Feedback dummy
                GameObject.Find("Main Camera").GetComponent<O_GameController>().ForceFeedback(1, 2.0f, 0.2f, true);

            }
            if (myObstacleType == obstacleType.wall)
            {
                GameObject.Find("Character").transform.GetComponent<Rigidbody>().AddForce((collision.transform.position - transform.position ) * 0.5f);
            }
        }
    }

    IEnumerator StartParticles(ParticleSystem p)
    {
        yield return new WaitForSeconds(0.05f);
        p.Play();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 14)
        {
            // destroy wall if hit by rebounded ship
            // maybe the SHIP should be destroyed and the wall ILLUMINATED?
            if (myObstacleType == obstacleType.wall)
            {
                Destroy(this.gameObject, 0.05f);
                GameObject newAcidEffect = (GameObject)Instantiate(AcidEffect, collision.transform.position, Quaternion.identity);
                Destroy(newAcidEffect, 1.1f);
                GameObject.Find("Main Camera").GetComponent<O_GameController>().levelScore += 2;
            }
                   
        }
        // hits the level end barrier
        if (collision.transform.tag == "end")
        {
            Destroy(this.gameObject, 0.2f);
        }
               
    }
}