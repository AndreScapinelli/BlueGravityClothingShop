using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAndCallNewArea : MonoBehaviour
{
    public Transform output;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with tag: " + collision.transform.tag);
        if (collision.transform.tag == "Player")
        {
            Debug.Log("Player want to exit!");
            GamePlayUI.CallTeleportToNewArea(output, collision.transform);
        }
    }
}
