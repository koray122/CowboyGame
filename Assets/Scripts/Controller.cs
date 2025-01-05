using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f; // �htiyaca g�re ayarlay�n

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


        // Hareket y�n�n� normalize edin
        Vector3 direction = movement.normalized;

        // Kameran�n y�n�n� de hesaba katarak hedef a��y� hesaplay�n
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;

        // D�n�� a��s�n� yumu�ak bir �ekilde hesaplay�n
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);

        // D�n��� uygulay�n
        transform.rotation = Quaternion.Euler(0, angle, 0);

        // Hareketi uygulay�n
        controller.Move(direction * moveSpeed * Time.deltaTime);
    }
}
