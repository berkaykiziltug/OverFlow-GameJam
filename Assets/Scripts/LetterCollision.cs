using UnityEngine;

public class LetterCollision : MonoBehaviour
{
    [SerializeField] private AudioClip hitClip;  // assign your sound
    private AudioSource audioSource;
    private bool hasHitBowl = false;  // ensures the sound plays only once

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasHitBowl) return;

        // Check if the hit object is part of the bowl
        if (collision.gameObject.CompareTag("Bowl") || 
            collision.gameObject.transform.parent.CompareTag("Bowl"))
        {
            audioSource.PlayOneShot(hitClip);
            hasHitBowl = true;
        }
    }
}
