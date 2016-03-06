using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class O_GameController : MonoBehaviour
{
    public enum states {starting, playing, end};
    public states gameState = states.starting;
    public int levelScore = 0;
    public int resources = 30;
    public int walls = 12;
    GameObject levelEnvironment;
    public float levelSpeed = 0.15f;
    public List<GameObject> shipList = new List<GameObject>();
    public GameObject otherShip;
    public GameObject debrisGameObject;
    public GameObject resourceObject;
    public GameObject wallObstacleObject;
    public LayerMask playerMask;
    float c = 2.0f;

    public GameObject[] healthBoxes;
    public Material goneHealth;
    private int health = 3;

    TextMesh scoreText;

    // Control creation of other falling ships
    void CreateObjectAtTop(GameObject newObject)
    {
        GameObject newShip = (GameObject)Instantiate(newObject, new Vector3(Random.Range(-7.5f, 7.5f), 6, -1), Quaternion.identity * Quaternion.Euler(90, 0, 0));
        newShip.transform.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1, 1), 0, 0) * 1.5f);
    }

    // create collectable items
    void CreateResources(int amount, GameObject resource, float zPos, bool randomOrientation, bool randomSize)
    {

        for (int i = 0; i < amount; i++)
        {
            Vector3 cPosition = new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(-12.0f, 1.5f), zPos);
            // reroll if overlapping with the player

            Collider[] objectsInRadius = Physics.OverlapSphere(transform.position, 1, playerMask);
            while (cPosition.x <= 0.8 && cPosition.x >= -0.8)
            {
               // Debug.Log("reroll");
                cPosition = new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(-12.0f, 1.5f), zPos);
            }

            GameObject newObject;

            if (randomOrientation)
            {
                newObject = (GameObject)Instantiate(resource, cPosition, Quaternion.identity * Quaternion.Euler(0, 0, Random.Range(0, 360)));
            }
            else
            {
               newObject = (GameObject)Instantiate(resource, cPosition, Quaternion.identity);
            }

            newObject.transform.SetParent(GameObject.Find("Obstacles").transform);
            if(randomSize)
            {
                newObject.transform.localScale = new Vector3(Random.Range(0.3f, 1.2f), Random.Range(0.1f, 0.4f), 1);
            }
        }

    }

    // Use this for initialization
    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMesh>();
        levelEnvironment = GameObject.Find("LevelEnvironment");
        // bounds set by hand

        CreateResources(walls, wallObstacleObject, -1, true, true);
        CreateResources(resources, resourceObject, -7, false, false);

        gameState = states.playing;

    }

    // Update is called once per frame
    void Update()
    {

        // Inch level environment upwards. Hitting the bottom ends the level
        levelEnvironment.transform.Translate(Vector3.up * Time.deltaTime * levelSpeed);

        c -= Time.deltaTime;
        if (c < 0)
        {

            CreateObjectAtTop(otherShip);
            // Check for end condition
            checkEnd();

            c = Random.Range(2.0f, 4.0f);



        }

        scoreText.text = levelScore.ToString();
        
       

    }

    public void RestartLevelWithDelay(float delay)
    {
        StartCoroutine("RestartLevelWithDelayIE", delay);
    }

    IEnumerator RestartLevelWithDelayIE(float delay)
    {
        yield return new WaitForSeconds(delay);
        UnityEngine.SceneManagement.SceneManager.LoadScene(Application.loadedLevel);
    }

    void checkEnd()
    {
        if (gameState != states.end)
        {
            if (health <= 0)
            {
                // level end
                gameState = states.end;


            }
        }
    }

    public void LoseHealth()
    {
        // check existing health boxes
        if(health <= 0)
        {
            return;
        } else
        {
            healthBoxes[health - 1].GetComponent<Renderer>().material = goneHealth;
            //Destroy(healthBoxes[health-1]);
            health--;
          
        }
    }


    public void ForceFeedback(int wheel, float force, float duration, bool clockwise)
    {
        // move the specified wheel with the specified force for the given time in a direction
    }
}
