using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalPoint : MonoBehaviour
{
    public string nextSceneName = "Level1";
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
