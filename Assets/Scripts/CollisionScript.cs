using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    [Header("Collection Settings")]
    [SerializeField] float pickupRadius = 3f;
    [SerializeField] float pickupRange = 4f;

    GameObject currentCollectible;
    
    int collCount = 0;
    int totalScore = 0;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Collectibles")
        {
            currentCollectible = collision.gameObject;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject == currentCollectible)
        {
            currentCollectible = null;
        }
    }
    
    void OnInteract()
    {
        Vector3 origin = transform.position + transform.forward * pickupRange;
        Collider[] hits = Physics.OverlapSphere(origin, pickupRadius);

        foreach (Collider hit in hits)
        {
            if (hit.gameObject.CompareTag("Collectibles"))
            {
                Collectible collectible = hit.gameObject.GetComponent<Collectible>();

                if (collectible != null)
                {
                    totalScore += collectible.score;
                    Debug.Log("Score: +" + collectible.score + " | Total: " + totalScore);
                }

                collCount++;
                Destroy(hit.gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "GoalArea" && collCount >= 7)
        {
            print("Player entered trigger zone with " +collCount+ "collectibles");
        }
    }
}

    