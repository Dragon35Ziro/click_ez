using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Vxod : MonoBehaviour 
{
    public GameObject vxod, registrac;
    //public InputField Login;

    

    public void ToRegistrac()
    {
        vxod.SetActive(false);
        registrac.SetActive(true);
    }

    public void ToVxod()
    {
        vxod.SetActive(true);
        registrac.SetActive(false);
    }
}
