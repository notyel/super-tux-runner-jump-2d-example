using UnityEngine;

public class AfterImageFade : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private float fadeSpeed = 2f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Color color = sr.color;
        color.a -= fadeSpeed * Time.deltaTime;
        sr.color = color;

        if (color.a <= 0f)
        {
            Destroy(gameObject);
        }
    }

}
