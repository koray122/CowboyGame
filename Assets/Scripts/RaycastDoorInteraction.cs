using UnityEngine;

public class RaycastDoorInteraction : MonoBehaviour
{
    public float rayDistance = 5f; // Raycast uzunlu�u
    public KeyCode interactKey = KeyCode.F; // Eylem i�in tu� (F tu�u)

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            RaycastHit hit;
            // Kameran�n bulundu�u pozisyondan ileriye do�ru bir raycast at�yoruz
            Ray ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                // �arp�lan nesnenin tag'ini kontrol ediyoruz
                if (hit.collider.CompareTag("Kap�"))
                {
                    // Kap� nesnesinin Animator bile�enini al�yoruz
                    Animator doorAnimator = hit.collider.GetComponent<Animator>();
                    if (doorAnimator != null)
                    {
                        // Kap� nesnesinin animasyonunu oynat�yoruz
                        doorAnimator.SetTrigger("OpenDoor"); // Animator'da "OpenDoor" isminde bir trigger parametresi oldu�undan emin olun
                    }
                }
            }
        }
    }
}
