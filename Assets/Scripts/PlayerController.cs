using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public GameObject playerObject;

    SpriteRenderer playerRenderer;
    Rigidbody2D rb;
    int dir = 1;
    bool isDead;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerRenderer = playerObject.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        Vector3 tempScale = playerObject.transform.localScale;
        tempScale.x = dir;
        playerObject.transform.localScale = tempScale;
        isDead = true;
    }
    
    void Update()
    {
        if (!isDead)
        {
            if (GameController.instance.isGameStarted)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    SoundManager.instance.PlayClip(AudioType.Fly);
                    rb.velocity = new Vector2(1 * dir, 1f) * 2.5f;
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    SoundManager.instance.PlayClip(AudioType.Fly);
                    rb.isKinematic = false;
                    rb.gravityScale = 0.4f;
                    GetComponent<Collider2D>().enabled = true;
                    rb.velocity = new Vector2(1 * dir, 1f) * 2.5f;
                }
            }
        }
        
        if (transform.position.y < -5)
        {
            Vector3 tempPos = transform.position;
            tempPos.y = -10f;
            transform.position = tempPos;
        }
    }

    public void ResetPlayer ()
    {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        transform.position = new Vector3(0, 0, 0);
        dir = 1;
        Vector3 tempScale = playerObject.transform.localScale;
        tempScale.x = dir;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        playerObject.transform.localScale = tempScale;
        GameController.instance.EndGame();
        isDead = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Wall"))
        {
            SoundManager.instance.PlayClip(AudioType.WallHit);
            if (dir > 0)
            {
                GameController.instance.PlayerWallHit(true);
            }
            else
            {
                GameController.instance.PlayerWallHit(false);
            }
            dir *= -1;
        } else if (collision.collider.gameObject.CompareTag("Spike"))
        {
            //DEATH
            Die(collision.GetContact(0).point);
        }

        Vector3 tempScale = playerObject.transform.localScale;
        tempScale.x = dir;
        playerObject.transform.localScale = tempScale;
    }

    void Die (Vector2 collisionPos)
    {
        SoundManager.instance.PlayClip(AudioType.Death);
        isDead = true;
        GetComponent<Collider2D>().enabled = false;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddTorque(2f, ForceMode2D.Impulse);
        rb.AddForce(((Vector2)transform.position - collisionPos) * 3.0f, ForceMode2D.Impulse);
        rb.gravityScale = 1.0f;
        MenuController.instance.OpenMenu(MenuType.Death);
    }

    public void ChangeSkin (Sprite skin)
    {
        playerRenderer.sprite = skin;
    }
}
