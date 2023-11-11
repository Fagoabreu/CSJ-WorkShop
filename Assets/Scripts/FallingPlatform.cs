using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallingTime=2f;
    public BoxCollider2D boxCollider;
    public TargetJoint2D joint;
    public Rigidbody2D rb;
    private Vector2 startPosition;
    private bool isFalling = false;

    private void Start() {
        startPosition = transform.position;
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (isFalling) return;
        if (collision.transform.CompareTag("Player")) {
            StartCoroutine(Falling());
        }
    }

    IEnumerator Falling() {
        isFalling=true;
        yield return new WaitForSeconds(fallingTime);
        boxCollider.enabled = false;
        joint.enabled = false;
        yield return new WaitForSeconds(4f);
        transform.position = startPosition;
        boxCollider.enabled = true;
        joint.enabled = true;
        isFalling = false;
    }
}
