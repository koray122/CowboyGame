using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npsController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public GameObject PATH;

    private Transform[] PathPoints;
    public int index = 0;
    public float minDistance = 10f;

    // Karakter h�z�n� kontrol etmek i�in public bir de�i�ken ekleyin
    public float speed = 3.5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        PathPoints = new Transform[PATH.transform.childCount];

        for (int i = 0; i < PathPoints.Length; i++)
        {
            PathPoints[i] = PATH.transform.GetChild(i);
        }

        // Ba�lang��ta NavMeshAgent'in h�z�n� ayarla
        agent.speed = speed;
    }

    void Update()
    {
        roam();
    }

    void roam()
    {
        if (PathPoints.Length == 0) return; // Yol noktas� yoksa i�leme devam etme

        if (Vector3.Distance(transform.position, PathPoints[index].position) < minDistance)
        {
            if (index < PathPoints.Length - 1)
            {
                index += 1;
            }
            else
            {
                index = 0;
            }
        }

        // Hedefe gitmek i�in NavMeshAgent'i ayarla
        agent.SetDestination(PathPoints[index].position);

        // Animat�r�n "vertical" parametresini h�zla g�ncelle
        animator.SetFloat("vertical", agent.velocity.magnitude > 0.1f ? 1 : 0);
    }
}
