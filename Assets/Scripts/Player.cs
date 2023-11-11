using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header ("Atributos")]
    public float speed = 5.5f;
    public float jumpForce =5F;
    public int life =5;

    [Header("Componentes")]
    public Rigidbody2D rig;
    public Animator anim;
    public SpriteRenderer sprite;
    [Header("UI")]
    public SpriteRenderer playerHealthRenderer;
    public Sprite[] healthImages;
    

    private Vector2 direction;
    private bool isGrounded = true;
    private bool recovery = false;
    public GameManager gameManager;

    void Start()
    {
        UpdateHealthUI();
        Time.timeScale = 1f;

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        direction = new Vector2 (Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        Jump();
        PlayAnim();
    }

    private void FixedUpdate() {
        Movement();
    }

    void Movement() {
        rig.velocity = new Vector2(direction.x * speed,rig.velocity.y);
    }

    void Jump() {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            anim.SetInteger("transition", 2);
            rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void Death() {
        if (life <= 0) {
            gameManager.RemovePlayer();
            Destroy(gameObject);
        }
    }

    void PlayAnim() {
        
        if (direction.x > 0) {
            if (isGrounded) {
                anim.SetInteger("transition", 1);
            }
            transform.eulerAngles = Vector2.zero;
        }
        if (direction.x < 0) {
            if (isGrounded) {
                anim.SetInteger("transition", 1);
            }
            transform.eulerAngles = new Vector2(0,180);
        }
        if (direction.x == 0 && isGrounded) {
            anim.SetInteger("transition", 0);
        }
    }

    public void Hit() {
        if (!recovery) {
            StartCoroutine(Flick());
        }
    }

    IEnumerator Flick() {
        life -= 1;
        Death();
        UpdateHealthUI();
        recovery = true;
        for (int i = 0; i < 3; i++) {
            sprite.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(.2f);
            sprite.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(.2f);
        }
        recovery = false;
    }

    public void IncreaseScore() {
        gameManager.AddApple();
    }

    private void UpdateHealthUI() {
        int maxHealthImageSize = life >= healthImages.Length ? healthImages.Length : life;
        playerHealthRenderer.sprite = healthImages[life];
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer== 6)
        {
            isGrounded = true;
        }
    }
}
