using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyDamageState : MonoBehaviour
{
    public EnemyAutomata automata;

    public MeshRenderer[] mesh;
    public Material dmgMaterial;

    public List<Material> mats;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform model;

    [SerializeField] private GameObject damageEffect;

    private void OnEnable()
    {
        Instantiate(damageEffect, transform.position, transform.rotation);

        if (mats != null)
        {
            mats.Clear();
        }

        foreach (MeshRenderer m in mesh)
        {
            mats.Add(m.material);
        }

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
