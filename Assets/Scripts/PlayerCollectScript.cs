using UnityEngine;

public class PlayerCollectScript : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Capsule")
        {
            print("Item collected!");
            Destroy(gameObject);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        print("Collision ended with " + collision.gameObject.name);
    }
}
