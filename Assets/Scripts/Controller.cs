using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f; // Ýhtiyaca göre ayarlayýn

    [SerializeField] private CharacterController controller;
    [SerializeField] private Camera mainCamera;


    [SerializeField] private Animator animatorController;
    private void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (movement.magnitude < 0.1f)
        {   

            animatorController.SetBool("isRunning" , false);
            return;
        }

        animatorController.SetBool("isRunning", true);


        // Hareket yönünü normalize edin
        Vector3 direction = movement.normalized;

        // Kameranýn yönünü de hesaba katarak hedef açýyý hesaplayýn
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;

        // Dönüþ açýsýný yumuþak bir þekilde hesaplayýn
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);

        // Dönüþü uygulayýn
        transform.rotation = Quaternion.Euler(0, angle, 0);

        // Hareketi uygulayýn
        controller.Move(direction * moveSpeed * Time.deltaTime);
    }
}
