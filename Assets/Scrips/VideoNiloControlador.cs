using System;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.InputSystem;

public class ControladorVideoNilo : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;

    [Header("Musica")]
    [SerializeField] ControladorMusica controladorMusica;

    [Header("Idle")]
    [SerializeField] VideoClip videoIdle;

    [Header("Victorias")]
    [SerializeField] VideoClip videoVictoria1;
    [SerializeField] VideoClip videoVictoria2;

    [Header("Videos validos")]
    [SerializeField] VideoClip[] videosValidos;

    [Header("Confusion")]
    [SerializeField] VideoClip videoConfusion;

    [Header("Puentes")]
    [SerializeField] VideoClip puente1;
    [SerializeField] VideoClip puente2;
    [SerializeField] VideoClip puente3;

    [Header("Puentes de victoria")]
    [SerializeField] VideoClip puenteVictoria1;
    [SerializeField] VideoClip puenteVictoria2;

    public event Action VideoTerminado;

    public void ReproducirIdle()
    {
        ReproducirLoop(videoIdle);
    }

    public void ReproducirVictoria1()
    {
        ReproducirVideo(videoVictoria1);
    }

    public void ReproducirVictoria2()
    {
        ReproducirVideo(videoVictoria2);
    }

    public void ReproducirVideoValido(int resultado)
    {
        int indice = resultado - 2;

        if (indice < 0 || indice >= videosValidos.Length)
        {
            Debug.LogError("No existe un video valido para el resultado: " + resultado);
            return;
        }

        ReproducirVideo(videosValidos[indice]);
    }

    public void ReproducirConfusion()
    {
        ReproducirVideo(videoConfusion);
    }

    public void ReproducirPuente(int numeroPuente)
    {
        switch (numeroPuente)
        {
            case 1:
                ReproducirLoop(puente1);
                break;

            case 2:
                ReproducirLoop(puente2);
                break;

            case 3:
                ReproducirLoop(puente3);
                break;

            default:
                Debug.LogError("Puente invalido: " + numeroPuente);
                break;
        }
    }

    public void ReproducirPuenteVictoria1()
    {
        ReproducirLoop(puenteVictoria1);
    }

    public void ReproducirPuenteVictoria2()
    {
        ReproducirLoop(puenteVictoria2);
    }

    void ReproducirVideo(VideoClip clip)
    {
        if (clip == null)
        {
            Debug.LogError("El VideoClip que se intenta reproducir esta vacio.");
            return;
        }

        if (controladorMusica != null)
        {
            controladorMusica.BajarVolumen();
        }

        videoPlayer.isLooping = false;
        videoPlayer.clip = clip;
        videoPlayer.Play();
    }

    void ReproducirLoop(VideoClip clip)
    {
        if (clip == null)
        {
            Debug.LogError("El VideoClip que se intenta reproducir en loop esta vacio.");
            return;
        }

        videoPlayer.isLooping = true;
        videoPlayer.clip = clip;
        videoPlayer.Play();
    }

    void Start()
    {
        videoPlayer.loopPointReached += AlTerminarVideo;
    }

    void AlTerminarVideo(VideoPlayer vp)
    {
        if (!vp.isLooping)
        {
            if (controladorMusica != null)
            {
                controladorMusica.RestaurarVolumen();
            }

            VideoTerminado?.Invoke();
        }
    }

    void Update()
    {
        if (Keyboard.current.vKey.wasPressedThisFrame)
        {
            ReproducirVictoria1();
        }

        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            ReproducirIdle();
        }
    }
}