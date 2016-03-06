using UnityEngine;
using System.Collections;

public class SpaceObstacleSpawner : MonoBehaviour {
    public Transform[] obstacles;
    Transform lastObstacleSpawned = null;
    private float lastObstacleX = 0;
    private float lastObstacleY = 0;
    private Vector3 lastObstaclePos;
    public GameObject goalZone;
    public GameObject player;
    public float safeBuffer = 7.0f;
    float spawnCounter = 1f;
    // need to populate space with asteroids for player to avoid
    // goal is in the center of the play area so the player can approach from any angle
    // could just create the asteroids in between the goal zone and the player and hurl them towards the player, more or less


    void Start()
    {

     }

    void Update()
    {
        // only spawn if player is not very near the object

        if ((player.transform.position - goalZone.transform.position).magnitude > safeBuffer)
        {
            spawnCounter -= Time.deltaTime;
            if (spawnCounter < 0)
            {
                spawnCounter = Random.Range(1.5f, 4f);
                if (Random.Range(0, 10) < 8)
                {
                    // pos = direction to goal * a reasonable random magnitude
                    Vector2 dir = (goalZone.transform.position - player.transform.position).normalized;
                    Debug.Log(dir);
                    Vector2 pos = dir * Random.Range(14f, 18.0f);
                    Instantiate(obstacles[Random.Range(0, obstacles.Length)], pos, Quaternion.Euler(Vector3.forward * Random.Range(-180, 180)));

                }
            }

        }
    }
    
}
