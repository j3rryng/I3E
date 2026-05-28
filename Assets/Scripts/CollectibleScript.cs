using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    public int collectibleScore = 0; // Store the score value of this collectible, editable from the Unity Inspector. (this allows different collectibles to be worth different amounts of points)

    AudioSource collectibleAudio;

    AudioClip collectibleAudioClip;

    void start()
    {
        collectibleAudio = GetComponent<AudioSource>(); //
    }

    public void Collect() // Custom method to handle the collection of this item, called from the PlayerScript when the player interacts with it
    {
        // if(collectibleAudio != null)
        // {
        //     collectibleAudio.Play();
        // }
        // SOLUTION 1
        //Destroy(gameObject, collectibleAudio.clip.length);
        
        // SOLUTION 2
        AudioSource.PlayClipAtPoint(collectibleAudioClip, transform.position);
        Destroy(gameObject);
    }
}