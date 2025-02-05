using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.Android.AndroidGame;

public class DamageState : MonoBehaviour
{
    public EnemyAutomata automata;

    public MeshRenderer[] mesh;
    public Material dmgMaterial;

    public List<Material> mats;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform model;

    private void OnEnable()
    {
        if (mats != null)
        {
            mats.Clear();
        }

        foreach (MeshRenderer m in mesh)
        {
            mats.Add(m.material);
        }

        rb.linearVelocity = model.transform.right * 5f;    

        StartCoroutine(TakeDamage_C());
    }

    private void OnDisable()
    {
        for (int i = 0; i < mats.Count; i++)
        {
            mesh[i].material = mats[i];
        }
    }

    IEnumerator TakeDamage_C()
    {
        foreach (MeshRenderer m in mesh)
        {
            m.material = dmgMaterial;
        }
        yield return new WaitForSeconds(0.1f);

        automata.ChangeState("MovementState");
    }

}
