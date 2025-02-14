using UnityEngine;
using UnityEngine.UIElements;

public class BossAnimsEvent : MonoBehaviour
{
    public GameObject impact;

    public Transform mainDroite;
    public Transform mainGauche;


    public void ImpactDroit()
    {
        Instantiate(impact, mainDroite.position, Quaternion.identity);
    }

    public void ImpactGauche()
    {
        Instantiate(impact, mainGauche.position, Quaternion.identity);
    }
}
