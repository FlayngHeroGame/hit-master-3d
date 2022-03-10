using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 1;
    public bool die;
    private Animator animator;
    [SerializeField]
    private HealthBar healthBar;

    [HideInInspector]
    public Waipoint waipoint;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (healthBar)
        {
            healthBar.MaxHP = hp;
            healthBar.CurHP = hp;
        }
    }

    public void Die()
    {
        die = true;
        waipoint.EnemyDie();
        gameObject.layer = (int)Mathf.Log(GameController.singleton.deadLayer.value, 2);
        //GetComponent<Collider>().enabled = false;

        animator.enabled = false;
        SetRagdoll(true);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            hp--;
            SetCanvasHP();
            if (hp <= 0)
            {
                Die();
            }
        }
    }

    private void SetRagdoll(bool stateRagdoll)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody rb in rigidbodies)
        {
           rb.isKinematic = !stateRagdoll;
        }

        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = stateRagdoll;
        }

        foreach (Transform tr in GetComponentsInChildren<Transform>())
        {
            tr.gameObject.layer = (int)Mathf.Log(GameController.singleton.deadLayer.value, 2);
        }

        GetComponent<Collider>().enabled = false;
    }

    private void SetCanvasHP()
    {
        if (healthBar)
        {
            healthBar.CurHP = hp;
        }
    }
}

