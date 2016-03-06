using UnityEngine;
using System.Collections;

public class PistonPusher : MonoBehaviour {

    // Impart force on stool
    public float pushPower = 0.6F;
    void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.gameObject.tag == "Stool")
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            if (body == null || body.isKinematic)
                return;

            if (hit.moveDirection.y < -0.3F)
                return;

            Vector3 pushDir = new Vector3(hit.moveDirection.x, hit.moveDirection.y, 0);
            body.velocity = pushDir * pushPower;
        }

    }

}
