using UnityEngine;

public class VillagerTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource _hmSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        { 
            int soundIndex = PlayerPrefs.GetInt("soundIndex");
           if(soundIndex != 1) PlayHmSound();
        }
    }
    
    void PlayHmSound() => _hmSound.Play();
}
