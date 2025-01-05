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

    // Karakter hýzýný kontrol etmek için public bir deðiþken ekleyin
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

        // Baþlangýçta NavMeshAgent'in hýzýný ayarla
        agent.speed = speed;
    }

    void Update()
    {
        roam();
    }

    void roam()
    {
        if (PathPoints.Length == 0) return; // Yol noktasý yoksa iþleme devam etme

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

        // Hedefe gitmek için NavMeshAgent'i ayarla
        agent.SetDestination(PathPoints[index].position);

        // Animatörün "vertical" parametresini hýzla güncelle
        animator.SetFloat("vertical", agent.velocity.magnitude > 0.1f ? 1 : 0);
    }
}
