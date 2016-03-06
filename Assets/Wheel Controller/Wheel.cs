using System.CodeDom;
using UnityEngine;
using System.Collections;

public class Wheel : MonoBehaviour
{
    public int id;
    public int position;

    float GetWheelSizeCount()
    {
        if (id == 0) return 296 / 20f * 3f;
        if (id == 1) return 144 / 16f * 8f;
        return 36 / 8f * 3f; // correct

    }

    void Start()
    {
        GameController.instance.RegisterWheel(this);
    }

    public void UpdatePosition(int change)
    {
        position += change;
        this.transform.eulerAngles = Vector3.forward * position;
    }

    public float getWheelTurns()
    {
        return position / GetWheelSizeCount() / 12.0f;
    }

    void Update()
    {

    }
}
