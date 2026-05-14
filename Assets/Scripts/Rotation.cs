using UnityEngine;
public class NewComponent : MonoBehaviour
{

    Vector3 velocity = new Vector3(0.05f, 0.05f, 0.05f);
    Vector3 rotationSpeed = new Vector3(45f, 90f, 30f); // degrees per second
    float boundary = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        
        print(transform.position.x);
        print(transform.position.y);
        print(transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position += newPosition;
        // Check X boundary
        if (transform.position.x > boundary || transform.position.x < -boundary)
            velocity.x *= -1;

        // Check Y boundary
        if (transform.position.y > boundary || transform.position.y < -boundary)
            velocity.y *= -1;

        // Check Z boundary
        if (transform.position.z > boundary || transform.position.z < -boundary)
            velocity.z *= -1;

        // Move the object
        transform.position += velocity;

        // --- Rotate the object ---
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
