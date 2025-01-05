using UnityEngine;

public class AudioFadeOut : MonoBehaviour
{
    public AudioSource audioSource;  // Ses kaynaðý
    public float fadeDuration = 5f;  // Sessizleþme süresi

    private void Start()
    {
        if (audioSource != null)
        {
            audioSource.Play();  // Ses dosyasýný çal
            StartCoroutine(FadeOut(audioSource, fadeDuration));  // FadeOut iþlemini baþlat
        }
        else
        {
            Debug.LogError("AudioSource referansý eksik!");
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

        audioSource.Stop();  // Ses dosyasýný durdur
        audioSource.volume = startVolume;  // Ses seviyesini eski haline getir
    }
}
