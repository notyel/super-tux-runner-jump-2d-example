using UnityEngine;

public class ObstaclePassed : MonoBehaviour
{
    private bool hasScored = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasScored && other.CompareTag("Player"))
        {
            ScoreManager.Instance.AddPoints(100);
            hasScored = true;

            Destroy(gameObject, 1f);
        }
    }
}
