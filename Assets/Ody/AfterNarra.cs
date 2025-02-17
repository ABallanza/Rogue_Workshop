using UnityEngine;

public class AfterNarra : MonoBehaviour
{
    

    public void ChangeLevel(string levelname)
    {
        Application.LoadLevel(levelname);
    }

}
