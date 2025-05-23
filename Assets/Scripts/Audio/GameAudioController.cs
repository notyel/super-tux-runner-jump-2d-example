using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(AudioSource))]
public class GameAudioController : MonoBehaviour
{
    public static GameAudioController instance;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject, 1f);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void ExecuteSound(AudioClip audio, float volume = 1f)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audio, volume);
        }
    }
}
