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

    public int curWaipoint; //������� ��������

    public State currentState;//������� ���������
    public State runState;//��������� ����
    public State idleState;//��������� �����

    [SerializeField] float bulletSpeed = 10; //�������� ������ ����
    [SerializeField] GameObject bullet;//����
    [SerializeField] Transform spawnPt;//������� �������� ����

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

        //�������
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
    
    public void MooveToNextPoint()//������� � ���������� ��������
    {
        curWaipoint++;
        if (curWaipoint >= GameController.singleton.waipoints.Length)
        {
            GameController.singleton.WinLevel();
            return;
        }
        SetState(runState);
    }

    public void SetState(State state)//��������� �������� ��������� �����
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

    public void FinishingState()//���������� �������� ���������
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

    public void WaipointFinishing()//������� �������/�������
    {
        if (GameController.singleton.waipoints[curWaipoint].IsFinishing())
        {
            MooveToNextPoint();
        }
    }
}
