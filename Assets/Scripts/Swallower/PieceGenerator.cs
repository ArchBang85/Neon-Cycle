using UnityEngine;
using System.Collections;

public class PieceGenerator : MonoBehaviour {

    public GameObject piece;
    public int numObjects = 10;
    float[] t = new float[3];
    public float[] timeBounds = new float[2];

	// Use this for initialization
	void Start () {


        for (int i = 0; i < t.Length; i++){
            t[i] = Random.Range(2f, 3f);
        }
     }
	
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < t.Length; i++)
        {
            t[i] -= Time.deltaTime;
            if(t[i]<0)
            {
                t[i] = Random.Range(timeBounds[0], timeBounds[1]);
                createPiece(i);
            }
        }


	}

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = 0.9f;
        return pos;
    }

    void createPiece(int c)
    {
        Vector3 center = transform.position;
        Vector3 pos = RandomCircle(center, 12.0f);
       // Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
        GameObject newPiece = (GameObject)Instantiate(piece, pos, Quaternion.identity);
        newPiece.GetComponent<PieceBehaviour>().setColour(c);
    }

}
