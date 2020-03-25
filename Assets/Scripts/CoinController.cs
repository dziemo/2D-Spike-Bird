using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public int coinsAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.instance.PlayClip(AudioType.CollectCoin);
            GameController.instance.AddCoin(coinsAmount);
        }
    }
}
