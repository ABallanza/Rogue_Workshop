using UnityEngine;

public class MainMenu : MonoBehaviour
{


    private void OnEnable()
    {
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        Application.LoadLevel("Game");
    }
}
