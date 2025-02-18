using System.Collections;
using System.Security.Cryptography;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimationEvent : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    


    public void Push(float force)
    {
        rb.linearVelocity = PlayerManager.Instance.model.right * force;
    }

    public void Attack(GameObject GO)
    {
        Instantiate(GO, transform.position, transform.rotation);
    }

    public Image biteSlider;
    public float timeBite = 2;

    public IEnumerator BiteRelaod()
    {
        float elapsedTime = 0;

        while (elapsedTime < timeBite)
        {
            biteSlider.fillAmount = 1 - (elapsedTime / timeBite); // Reduce fill amount over time
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        biteSlider.fillAmount = 0; // Ensure it's fully depleted
    }


}
