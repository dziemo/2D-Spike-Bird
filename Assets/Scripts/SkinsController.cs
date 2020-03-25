using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsController : MonoBehaviour
{
    public static SkinsController instance;

    public List<GameObject> skinsButtons = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        char[] skins = PlayerPrefs.GetString("Skins", "10000000000000000000000000000000000").ToCharArray();
        for (int i = 0; i < skins.Length; i++)
        {
            if (skins[i] == '1')
            {
                skinsButtons[i].GetComponent<SkinsButtonController>().SetPurchased();
            }
        }
    }

    public void UnlockSkin (GameObject go)
    {
        int index = skinsButtons.IndexOf(go);
        char[] skins = PlayerPrefs.GetString("Skins", "10000000000000000000000000000000000").ToCharArray();
        skins[index] = '1';
        Debug.Log("SKINS NOW: " + new string(skins).ToString());
        PlayerPrefs.SetString("Skins", new string(skins));
    }

}
