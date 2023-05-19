using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPrefabs : MonoBehaviour
{
    public GameObject content;
    public GameObject data;


    public void Start()
    {
        GameObject g = Instantiate(data, content.transform);
        g.transform.GetChild(4).GetComponent<Text>().text = "Candy Cue";
    }
}
