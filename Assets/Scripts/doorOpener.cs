using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator; // Kapýnýn Animator'ýný referans olarak alacaðýz
    public float raycastDistance = 3f; // Raycast mesafesi
    public string doorTag = "kapý"; // Kapý tag'ini belirt

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
                    // Kapýnýn Animator'ýný kontrol et ve DoorOpen trigger'ýný tetikle
                    if (doorAnimator != null)
                    {
                        doorAnimator.SetTrigger("DoorOpen");
                    }
                }
            }
        }
    }
}
