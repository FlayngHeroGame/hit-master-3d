using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    private Camera mainCamera;
    
    private int maxHP;
    public int MaxHP {
        get {
            return maxHP;
        }
        set { 
            maxHP = value;
            slider.maxValue = maxHP;
        } 
    }

    private int curHP;
    public int CurHP
    {
        get
        {
            return curHP;
        }
        set
        {
            curHP = value;
            slider.value = curHP;
        }
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        transform.LookAt(mainCamera.transform);
    }
}
