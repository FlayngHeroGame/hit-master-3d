using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyPooler;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ObjectPooler.Instance.ReturnToPool("", gameObject);
    }

    
}
