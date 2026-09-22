using System.Collections;
using UnityEngine;

public class ControladorMusica : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [Header("Volumen durante videos")]
    [SerializeField, Range(0f, 1f)] float multiplicadorDuranteVideo = 0.35f;

    [Header("Fade")]
    [SerializeField] float duracionFade = 1f;

    private float volumenJugador = 0.5f;
    private bool volumenReducido = false;
    private Coroutine fadeCoroutine;

    const string CLAVE_VOLUMEN = "volumenAudio";

    void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource == null)
        {
            Debug.LogError("ControladorMusica: no se encontro el AudioSource.");
            return;
        }

        volumenJugador = PlayerPrefs.GetFloat(CLAVE_VOLUMEN, 0.5f);

        audioSource.volume = volumenJugador;
    }

    public void ReproducirMusica()
    {
        if (audioSource == null)
            return;

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        if (!volumenReducido)
        {
            audioSource.volume = volumenJugador;
        }
    }

    public void CambiarVolumen(float valor)
    {
        volumenJugador = Mathf.Clamp01(valor);

        PlayerPrefs.SetFloat(CLAVE_VOLUMEN, volumenJugador);
        PlayerPrefs.Save();

        if (!volumenReducido)
        {
            audioSource.volume = volumenJugador;
        }
    }

    public void BajarVolumen()
    {
        if (audioSource == null)
            return;

        volumenReducido = true;

        IniciarFade(volumenJugador * multiplicadorDuranteVideo);
    }

    public void RestaurarVolumen()
    {
        if (audioSource == null)
            return;

        volumenReducido = false;

        IniciarFade(volumenJugador);
    }

    void IniciarFade(float volumenObjetivo)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(FadeVolumen(volumenObjetivo));
    }

    IEnumerator FadeVolumen(float volumenObjetivo)
    {
        float volumenInicial = audioSource.volume;
        float tiempo = 0f;

        while (tiempo < duracionFade)
        {
            tiempo += Time.deltaTime;

            float progreso = tiempo / duracionFade;

            audioSource.volume = Mathf.Lerp(
                volumenInicial,
                volumenObjetivo,
                progreso
            );

            yield return null;
        }

        audioSource.volume = volumenObjetivo;

        fadeCoroutine = null;
    }

    public float ObtenerVolumen()
    {
        return volumenJugador;
    }
}