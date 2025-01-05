using UnityEngine;

public class AudioFadeOut : MonoBehaviour
{
    public AudioSource audioSource;  // Ses kayna��
    public float fadeDuration = 5f;  // Sessizle�me s�resi

    private void Start()
    {
        if (audioSource != null)
        {
            audioSource.Play();  // Ses dosyas�n� �al
            StartCoroutine(FadeOut(audioSource, fadeDuration));  // FadeOut i�lemini ba�lat
        }
        else
        {
            Debug.LogError("AudioSource referans� eksik!");
        }
    }

    private System.Collections.IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.Stop();  // Ses dosyas�n� durdur
        audioSource.volume = startVolume;  // Ses seviyesini eski haline getir
    }
}
