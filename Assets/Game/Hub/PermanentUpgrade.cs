using UnityEngine;
using UnityEngine.UI;

public class PermanentUpgrade : MonoBehaviour
{

    public Text stat;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Life"))
        {
            PlayerPrefs.SetInt("Life", 0);
        }
    }

    private void Update()
    {
        stat.text = PlayerPrefs.GetInt("Life").ToString();
    }


    public void AddLife()
    {
        if(PlayerPrefs.GetInt("Gem") >= 5)
        {
            PlayerPrefs.SetInt("Life", PlayerPrefs.GetInt("Life") + 4);
            PlayerPrefs.SetInt("Gem", PlayerPrefs.GetInt("Gem") - 4);
        }
    }


}
