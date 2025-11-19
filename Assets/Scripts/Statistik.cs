using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Statistik : MonoBehaviour 
{
    static public int kol;
    public TextMeshProUGUI textkol;

    public void addNum()
    {
        kol++;
        textkol.text = kol.ToString();
        //PlayerPrefs.SetInt("kol", kol);
    }
}


