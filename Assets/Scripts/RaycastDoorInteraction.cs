using UnityEngine;

public class RaycastDoorInteraction : MonoBehaviour
{
    public float rayDistance = 5f; // Raycast uzunluðu
    public KeyCode interactKey = KeyCode.F; // Eylem için tuþ (F tuþu)

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            RaycastHit hit;
            // Kameranýn bulunduðu pozisyondan ileriye doðru bir raycast atýyoruz
            Ray ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                // Çarpýlan nesnenin tag'ini kontrol ediyoruz
                if (hit.collider.CompareTag("Kapý"))
                {
                    // Kapý nesnesinin Animator bileþenini alýyoruz
                    Animator doorAnimator = hit.collider.GetComponent<Animator>();
                    if (doorAnimator != null)
                    {
                        // Kapý nesnesinin animasyonunu oynatýyoruz
                        doorAnimator.SetTrigger("OpenDoor"); // Animator'da "OpenDoor" isminde bir trigger parametresi olduðundan emin olun
                    }
                }
            }
        }
    }
}
