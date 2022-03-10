using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waipoint : MonoBehaviour
{
    public Enemy[] enemys;
    public bool complete;

    private void Start()
    {
        foreach (Enemy enemy in enemys)
        {
            enemy.waipoint = this;
        }
    }

    public bool CheckAllDie()//проверка живых врагов в текущем вейпойнте
    {
        bool allDie = true;
        foreach (Enemy enemy in enemys)
        {
            if (!enemy.die)
            {
                allDie = false;
            }
        }
        return allDie;
    }

    public void EnemyDie()
    {
        complete = CheckAllDie();
        if (complete)
        {
            CharacterMovement.singleton.WaipointFinishing();
        }
    }

    public bool IsFinishing()
    {
        if(complete || enemys.Length == 0)
        {
            return true;
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}
