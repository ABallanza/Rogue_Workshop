using UnityEngine;

public class PermanentStats : MonoBehaviour
{


    public static PermanentStats Instance;

    public int life = 0;


    public void AddLife()
    {
        
    }


    private void Awake()
    {
        Instance = this;
    }


}
