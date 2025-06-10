using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioSource jump_Sound;
    private Rigidbody2D rb;

    public float movementSpeed = 5f;
    private float movement;

    [Header("Pozisyon Sınırları")]
    public float sagPozisyon = 10f;
    public float solPozisyon = -10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.AddForce(new Vector2(0, 15), ForceMode2D.Impulse);
    }

    private void Update()
    {
        movement = 0;

        // Dokunmatik ekran kontrolü
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            float halfScreen = Screen.width / 2f;

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                if (touch.position.x < halfScreen)
                {
                    // Sol tarafa dokunuldu
                    movement = -movementSpeed;
                }
                else
                {
                    // Sağ tarafa dokunuldu
                    movement = movementSpeed;
                }
            }
        }

        // Sınır kontrolleri
        if (transform.position.x >= sagPozisyon && movement > 0)
        {
            movement = 0;
        }

        if (transform.position.x <= solPozisyon && movement < 0)
        {
            movement = 0;
        }

        // Sprite yönünü çevir
        if (movement > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (movement < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Jump"))
        {
            jump_Sound.Play();
        }
    }

    private void FixedUpdate()
    {
        Vector2 vel = rb.linearVelocity;
        vel.x = movement;
        rb.linearVelocity = vel;
    }
}
