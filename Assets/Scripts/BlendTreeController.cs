using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendTreeController : MonoBehaviour
{
    public float speed = 2f; // Varsay�lan h�z
    public float walkRotationSpeed = 120f; // Y�r�y�� s�ras�nda d�n�� h�z�
    public float runRotationSpeed = 240f; // Ko�ma s�ras�nda d�n�� h�z�
    public float slowWalkSpeed = 1f; // S tu�una bas�ld���nda h�z
    public Animator animator; // Animator bile�eni
    private Rigidbody rb;
    public float jumpForce;

    public LayerMask groundLayer; // Yere temas kontrol� i�in LayerMask
    public float groundCheckDistance = 0.2f; // Raycast'in uzunlu�u

    private float horizontalInput;
    private float verticalInput;
    private float currentRotationY = 0f; // Mevcut d�nd�rme a��s�n� saklar
    private float currentRotationSpeed; // Mevcut d�n�� h�z�n� saklar

    void Start()
    {
        // Animator ve Rigidbody bile�enlerini al
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Kullan�c�dan giri� de�erlerini al
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Hareket ve animasyon fonksiyonlar�n� g�ncelle
        Animasyon();
        Kosma();
        Donme(); // G�ncellenmi� d�n�� fonksiyonu
        Hareket();
        Ziplama();
    }

    void Animasyon()
    {
        // Animator parametrelerini g�ncelle
        animator.SetFloat("YatayHareket", horizontalInput);
        animator.SetFloat("DikeyHareket", verticalInput);
    }

    void Hareket()
    {
        // Karakterin bak�� y�n� do�rultusunda hareket etmesini sa�lar
        Vector3 moveDirection = transform.forward * verticalInput ;
        Vector3 movement = moveDirection.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    void Kosma()
    {
        // Shift ve W tu�lar�na ayn� anda bas�l� tutuldu�unda h�z de�erini de�i�tir
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            speed = 4f; // Ko�ma h�z�
            animator.SetBool("isRun", true);
            currentRotationSpeed = runRotationSpeed; // Ko�ma s�ras�nda d�n�� h�z�n� ayarla
        }
        else if (Input.GetKey(KeyCode.S))
        {
            speed = slowWalkSpeed; // S tu�una bas�ld���nda daha yava� h�z
            animator.SetBool("isRun", false);
            currentRotationSpeed = walkRotationSpeed; // Y�r�y�� s�ras�nda d�n�� h�z�n� ayarla
        }
        else
        {
            speed = 2f; // Normal y�r�y�� h�z�
            animator.SetBool("isRun", false);
            currentRotationSpeed = walkRotationSpeed; // Y�r�y�� s�ras�nda d�n�� h�z�n� ayarla
        }
    }

    void Donme()
    {
        // Karakteri A ve D tu�lar�na g�re ad�m ad�m d�nd�r
        float yaw = horizontalInput * currentRotationSpeed * Time.deltaTime;
        currentRotationY += yaw;

        // Debug: Mevcut d�n�� a��s�n� kontrol et
        Debug.Log("Current Rotation Y: " + currentRotationY);

        // D�n�� a��s�n� uygula
        transform.rotation = Quaternion.Euler(0, currentRotationY, 0);
    }

    void Ziplama()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
            animator.SetBool("isJump", true);
        }
        else
        {
            animator.SetBool("isJump", false);
        }
    }

    bool IsGrounded()
    {
        // Raycast ile yer kontrol� yap
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }
}
