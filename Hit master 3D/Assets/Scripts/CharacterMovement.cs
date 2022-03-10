using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MyPooler;

public class CharacterMovement : MonoBehaviour
{
    public static CharacterMovement singleton {get; private set;}

    private Animator animator;
    private Camera mainCamera;
    [HideInInspector]
    public NavMeshAgent agent;

    public int curWaipoint; //текущий вейпойнт

    public State currentState;//текущее состояние
    public State runState;//состояние бега
    public State idleState;//состояние покоя

    [SerializeField] float bulletSpeed = 10; //скорость полета пули
    [SerializeField] GameObject bullet;//пуля
    [SerializeField] Transform spawnPt;//позиция создания пули

    private void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;

        SetState(idleState);
    }

    void Update()
    {
        currentState.UpdateState();

        //Выстрел
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo))
            {
                GameObject projectile = ObjectPooler.Instance.GetFromPool("", spawnPt.transform.position, Quaternion.identity);
                projectile.transform.LookAt(hitInfo.point);
                projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * bulletSpeed;
            }
        }
#endif

#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject projectile = ObjectPooler.Instance.GetFromPool("", spawnPt.transform.position, Quaternion.identity);
                projectile.transform.LookAt(hitInfo.point);
                projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * bulletSpeed;
            }
        }
#endif
    }
    
    public void MooveToNextPoint()//переход к следующему вейпойнт
    {
        curWaipoint++;
        if (curWaipoint >= GameController.singleton.waipoints.Length)
        {
            GameController.singleton.WinLevel();
            return;
        }
        SetState(runState);
    }

    public void SetState(State state)//установка текущего состояния героя
    {
        currentState = Instantiate(state);
        currentState.characterMovement = this;
        currentState.Init();
    }

    public void SetAnimation(string anim)
    {
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Run");
        animator.SetTrigger(anim);
    }

    public void FinishingState()//завершение текущего состояния
    {
        if (GameController.singleton.waipoints[curWaipoint].IsFinishing())
        {
            MooveToNextPoint();
        }
        else
        {
            SetState(idleState);
        }
    }

    public void WaipointFinishing()//вейпонт зачищен/пройден
    {
        if (GameController.singleton.waipoints[curWaipoint].IsFinishing())
        {
            MooveToNextPoint();
        }
    }
}
