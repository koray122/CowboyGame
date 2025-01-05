using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendTreeController : MonoBehaviour
{
    public float speed = 2f; // Varsayýlan hýz
    public float walkRotationSpeed = 120f; // Yürüyüþ sýrasýnda dönüþ hýzý
    public float runRotationSpeed = 240f; // Koþma sýrasýnda dönüþ hýzý
    public float slowWalkSpeed = 1f; // S tuþuna basýldýðýnda hýz
    public Animator animator; // Animator bileþeni
    private Rigidbody rb;
    public float jumpForce;

    public LayerMask groundLayer; // Yere temas kontrolü için LayerMask
    public float groundCheckDistance = 0.2f; // Raycast'in uzunluðu

    private float horizontalInput;
    private float verticalInput;
    private float currentRotationY = 0f; // Mevcut döndürme açýsýný saklar
    private float currentRotationSpeed; // Mevcut dönüþ hýzýný saklar

    void Start()
    {
        // Animator ve Rigidbody bileþenlerini al
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Kullanýcýdan giriþ deðerlerini al
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Hareket ve animasyon fonksiyonlarýný güncelle
        Animasyon();
        Kosma();
        Donme(); // Güncellenmiþ dönüþ fonksiyonu
        Hareket();
        Ziplama();
    }

    void Animasyon()
    {
        // Animator parametrelerini güncelle
        animator.SetFloat("YatayHareket", horizontalInput);
        animator.SetFloat("DikeyHareket", verticalInput);
    }

    void Hareket()
    {
        // Karakterin bakýþ yönü doðrultusunda hareket etmesini saðlar
        Vector3 moveDirection = transform.forward * verticalInput ;
        Vector3 movement = moveDirection.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    void Kosma()
    {
        // Shift ve W tuþlarýna ayný anda basýlý tutulduðunda hýz deðerini deðiþtir
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            speed = 4f; // Koþma hýzý
            animator.SetBool("isRun", true);
            currentRotationSpeed = runRotationSpeed; // Koþma sýrasýnda dönüþ hýzýný ayarla
        }
        else if (Input.GetKey(KeyCode.S))
        {
            speed = slowWalkSpeed; // S tuþuna basýldýðýnda daha yavaþ hýz
            animator.SetBool("isRun", false);
            currentRotationSpeed = walkRotationSpeed; // Yürüyüþ sýrasýnda dönüþ hýzýný ayarla
        }
        else
        {
            speed = 2f; // Normal yürüyüþ hýzý
            animator.SetBool("isRun", false);
            currentRotationSpeed = walkRotationSpeed; // Yürüyüþ sýrasýnda dönüþ hýzýný ayarla
        }
    }

    void Donme()
    {
        // Karakteri A ve D tuþlarýna göre adým adým döndür
        float yaw = horizontalInput * currentRotationSpeed * Time.deltaTime;
        currentRotationY += yaw;

        // Debug: Mevcut dönüþ açýsýný kontrol et
        Debug.Log("Current Rotation Y: " + currentRotationY);

        // Dönüþ açýsýný uygula
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
        // Raycast ile yer kontrolü yap
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }
}
