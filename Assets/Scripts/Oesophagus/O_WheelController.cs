using UnityEngine;
using System.Collections;

public class O_WheelController : MonoBehaviour {
    public float speed = 5.0f;
    public float moveSpeed = 0.5f;
    private int scoreFromEating = 5;
    public GameObject shieldWheel;
    public GameObject lightWheel;

    private bool canTakeDamage = true; 
    GameObject shieldParticleEffect;
    O_GameController gameController;
    public GameObject damageEffect;

    Rigidbody rigidbody;
    Camera viewCamera;

    // Use this for initialization
    void Start() {
        rigidbody = GetComponent<Rigidbody>();
        viewCamera = Camera.main;
        gameController = viewCamera.GetComponent<O_GameController>();
        StartCoroutine("EnforceRotationWithDelay", (0.3f));
        shieldParticleEffect = GameObject.Find("ShieldParticleEffect");

        // initial effect: expand lights
        StartCoroutine("ActivateLights");
        transform.GetComponent<Rigidbody>().AddForce(-Vector3.up * 5);
    }

    // todo

    // movemement with rigidbodies
    // eating units
    // functioning shield
    // scorekeeping
    // transition screen effect

    // Brighten lights
    IEnumerator ActivateLights()
    {
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(0.01f);
            lightWheel.transform.GetComponent<FieldOfView>().viewRadius += 0.22f;
        }


    }

    // Enforce 0 rotation on two axes
    IEnumerator EnforceRotationWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            EnforceRotation();
        }
    }

    void EnforceRotation()
    {
        transform.localEulerAngles = (new Vector3(0, 0, transform.eulerAngles.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.gameState == O_GameController.states.playing)
        {
            transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
        }
        if (gameController.gameState == O_GameController.states.end)
        {
            transform.GetComponent<Rigidbody>().mass = 10000;
            // set player to a different layer so they can fall off the bottom when they've been sufficiently damaged
            this.gameObject.layer = 13;

            // switch alert light on
            GameObject.Find("AlertLight").GetComponent<Light>().enabled = true;
        }
            // CONTROLS //

            // This gameobject for big wheel and direction
            // Shield wheel for middle wheel control
            // Light wheel for small wheel control



            // KEYBORAD CONTROLS
            // (1) main wheel
            if (Input.GetKey(KeyCode.Q))
        {
            this.transform.Rotate(Vector3.forward, Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Rotate(Vector3.forward, -Time.deltaTime * speed);
        }



        // (2) shield wheel
        if (Input.GetKey(KeyCode.A))
        {
            shieldWheel.transform.Rotate(Vector3.forward, Time.deltaTime * speed);

        }
        if (Input.GetKey(KeyCode.S))
        {
            shieldWheel.transform.Rotate(Vector3.forward, -Time.deltaTime * speed);

        }

        // (3) light wheel 
        if (Input.GetKey(KeyCode.Z))
        {
            lightWheel.transform.Rotate(Vector3.forward, Time.deltaTime * speed);

        }
        if (Input.GetKey(KeyCode.X))
        {
            lightWheel.transform.Rotate(Vector3.forward, -Time.deltaTime * speed);

        }


        // Rotate shield particle effects to shield wheel rotation
        //Debug.Log("shield rotation" + shieldWheel.transform.localEulerAngles.z);
       // shieldParticleEffect.GetComponent<ParticleSystem>().startRotation = shieldWheel.transform.localEulerAngles.z;

        



    }
    void FixedUpdate()
    {

        rigidbody.AddForce(Vector3.forward * Time.deltaTime * moveSpeed);

    }


    // What happens when objects collide with the player?
    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "consumable")
        {
            // consume resource and add points
            Destroy(collision.gameObject);
            gameController.levelScore += scoreFromEating;

        }

        // Lose health if hitting an obstacle without the shield (also trigger cooldown)
        if (collision.transform.tag == "obstacle")
        {
            if (canTakeDamage)
            {

                // instantiate damage effect
                GameObject damageEffectObject = (GameObject)Instantiate(damageEffect, collision.transform.position, Quaternion.identity);
                Destroy(damageEffectObject, 1.4f);
                
                // destroy obstacle
                Destroy(collision.gameObject, 0.1f);

                gameController.LoseHealth();
                canTakeDamage = false;
                StartCoroutine("DamageCooldown", 1.0f);
            }
        }
    }

    IEnumerator DamageCooldown(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        canTakeDamage = true;
    }
}
