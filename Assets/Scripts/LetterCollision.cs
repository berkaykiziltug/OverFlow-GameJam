using UnityEngine;

public class LetterCollision : MonoBehaviour
{
    [SerializeField] private AudioClip hitClip;  
    private AudioSource audioSource;
    private bool hasHit = false; 

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasHit)
        {
            AudioManagerMenu.Instance.PlaySFXPitch(hitClip,1.2f,2.5f);
        }
        hasHit = true;
    }
}
