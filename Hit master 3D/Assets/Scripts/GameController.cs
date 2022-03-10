using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameController : MonoBehaviour
{
    public static GameController singleton { get; private set; }

    public bool pause = true;
    public LayerMask deadLayer;
    public Waipoint[] waipoints;

    [SerializeField]
    private GameObject characterPrefab;
    [SerializeField]
    CinemachineVirtualCamera cvCamera;

    private void Awake()
    {
            singleton = this;
    }

    private void Start()
    {
        pause = true;
        GameObject character = Instantiate(characterPrefab, waipoints[0].transform.position, Quaternion.identity);
        cvCamera.Follow = character.transform;
    }

    public void WinLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) & pause)//первый тап -герой начинает двигаться
        {
            pause = false;
            CharacterMovement.singleton.MooveToNextPoint();
        }
#endif

#if UNITY_ANDROID
        if (Input.touchCount > 0 & pause)
        {
            pause = false;
            CharacterMovement.singleton.MooveToNextPoint();
        }
#endif
    }
}
