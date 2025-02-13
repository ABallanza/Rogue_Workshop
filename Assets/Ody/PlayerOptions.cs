using System.Threading;
using UnityEngine;

public class PlayerOptions : MonoBehaviour
{
    public bool openned = false;

    public GameObject optionsCanvas;

    public PlayerInput playerInput;

    public GameObject[] canvases;
    private int currentCanvasIndex = 0;

    private void OnEnable()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();

        playerInput.Player.Options.performed += ctx => Options();
        playerInput.Player.Next.performed += ctx => ChangeCanvas("Next");
        playerInput.Player.Previous.performed += ctx => ChangeCanvas("Previous");
    }

    private void OnDisable()
    {
        playerInput.Disable();

        playerInput.Player.Options.performed -= ctx => Options();
        playerInput.Player.Next.performed -= ctx => ChangeCanvas("Next");
        playerInput.Player.Previous.performed -= ctx => ChangeCanvas("Previous");
    }


    public void ChangeCanvas(string what)
    {
        if (canvases.Length == 0) return;

        foreach (GameObject canvas in canvases)
        {
            canvas.SetActive(false);
        }

        if (what == "Next")
        {
            currentCanvasIndex = (currentCanvasIndex + 1) % canvases.Length;
        }
        else if (what == "Previous")
        {
            currentCanvasIndex = (currentCanvasIndex - 1 + canvases.Length) % canvases.Length;
        }
        canvases[currentCanvasIndex].SetActive(true);
    }


    void Options()
    {
        if(!openned)
        {
            openned = true;
            optionsCanvas.SetActive(true);
            Time.timeScale = 0;
            return;
        }
        else
        {
            openned = false;
            optionsCanvas.SetActive(false);
            Time.timeScale = 1;
            return;
        }
    }

    public void Back()
    {
        openned = false;
        optionsCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void Menu()
    {
        Application.LoadLevel("MainMenu");
    }

}
