using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator; // Kap�n�n Animator'�n� referans olarak alaca��z
    public float raycastDistance = 3f; // Raycast mesafesi
    public string doorTag = "kap�"; // Kap� tag'ini belirt

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                if (hit.collider.CompareTag(doorTag))
                {
                    // Kap�n�n Animator'�n� kontrol et ve DoorOpen trigger'�n� tetikle
                    if (doorAnimator != null)
                    {
                        doorAnimator.SetTrigger("DoorOpen");
                    }
                }
            }
        }
    }
}
