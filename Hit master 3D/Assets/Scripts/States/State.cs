using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : ScriptableObject
{
    [HideInInspector] public CharacterMovement characterMovement;
    
    public virtual void Init(){ }

    public virtual void FinishState() { }
    
    public abstract void UpdateState();
}
