using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private AudioClip cointSound;
    private bool hasScored = false;

    private void OnTriggerEnter2D(Collider2D other)
	{
		if (!hasScored && other.CompareTag("Player"))
		{
            GameAudioController.instance.ExecuteSound(cointSound, 0.7f); 
            hasScored = true;
            ScoreManager.Instance.AddPoints(20);
         
            gameObject.SetActive(false);
        }
	}
}