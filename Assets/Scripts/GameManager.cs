using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform point;
    public int apples = 0;
    public int playersCount=0;
    public TextMeshProUGUI appleTextUI;
    public TextMeshProUGUI lifesTextUI;
    public GameObject gameOver;
    // Start is called before the first frame update
    void Start()
    {
        Player[] players = FindObjectsOfType<Player>();
        playersCount = players.Length;
        lifesTextUI.text = playersCount.ToString();
       foreach (Player player in players) {
            player.transform.position = point.position;
            player.gameManager = this;
        }
    }

    public void AddApple() {
        apples++;
        appleTextUI.text = apples.ToString();
    }

    public void RemovePlayer() {
        playersCount--;
        lifesTextUI.text = playersCount.ToString();
        if (playersCount <= 0) {
            gameOver.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void RestartGame() {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
