using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public List<GameObject> leftSpikes = new List<GameObject>();
    public List<GameObject> rightSpikes = new List<GameObject>();

    public PlayerController playerController;

    public GameObject coin;

    public TextMeshPro scoreText;
    public TextMeshProUGUI playerCoinsTotalText, highScoreText;
    public bool isGameStarted;

    List<int> spikeIndex = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
    int score = 0, highestScore = 0;

    private void Awake()
    {
        isGameStarted = false;
        instance = this;
    }

    void Start()
    {
        highestScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "HIHGEST SCORE : " + highestScore.ToString();
        DeactivateSpikes();
        scoreText.text = score.ToString();
        playerCoinsTotalText.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    void DeactivateSpikes()
    {
        foreach (var spike in leftSpikes)
        {
            spike.SetActive(false);
        }

        foreach (var spike in rightSpikes)
        {
            spike.SetActive(false);
        }
    }

    public void PlayerWallHit (bool isRightWall)
    {
        DeactivateSpikes();
        score++;
        scoreText.text = score.ToString();

        if (score > highestScore)
        {
            highestScore = score;
            PlayerPrefs.SetInt("HighScore", highestScore);
        }

        int spikesCount = 2 + score / 15;
        spikesCount = Mathf.Clamp(spikesCount, 1, 5);
        Time.timeScale = 1 + ((score / 10) * 0.125f);
        if (isRightWall)
        {
            ActivateRandomSpikes(leftSpikes, Random.Range(spikesCount, spikesCount + 2));
        } else
        {
            ActivateRandomSpikes(rightSpikes, Random.Range(spikesCount, spikesCount + 2));
        }
    }

    public void ActivateRandomSpikes (List<GameObject> spikes, int spikesToActivate)
    {
        List<int> indexes = new List<int>(spikeIndex);

        for (int i = 0; i < spikesToActivate; i++)
        {
            int rnd = Random.Range(0, indexes.Count);
            spikes[indexes[rnd]].SetActive(true);
            indexes.Remove(indexes[rnd]);
        }
    }

    public void StartGame ()
    {
        isGameStarted = true;
        ActivateRandomSpikes(rightSpikes, 1);
        RandomizeCoinPosition();
        playerController.ResetPlayer();
    }

    public void EndGame ()
    {
        isGameStarted = false;
        Time.timeScale = 1f;
        DeactivateSpikes();
        score = 0;
        scoreText.text = score.ToString();
    }

    public void AddCoin (int amount)
    {
        RandomizeCoinPosition();
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + amount);
        Debug.Log("PLAYER COINS: " + PlayerPrefs.GetInt("Coins"));
        playerCoinsTotalText.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    void RandomizeCoinPosition ()
    {
        StopAllCoroutines();
        if (!coin.activeSelf)
        {
            coin.SetActive(true);
        }
        Vector2 newPos = new Vector2(Random.Range(-2.0f, 2.0f), Random.Range(-3.0f, 3.0f));
        coin.transform.position = newPos;
        StartCoroutine(CoinTimer());
    }

    IEnumerator CoinTimer ()
    {
        yield return new WaitForSeconds(5.0f);
        RandomizeCoinPosition();
    }
}
