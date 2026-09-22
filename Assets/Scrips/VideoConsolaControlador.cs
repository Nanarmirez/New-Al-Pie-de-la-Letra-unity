using UnityEngine;
using UnityEngine.Video;

public class ControladorVideoConsola : MonoBehaviour
{
    [Header("Video Player")]
    [SerializeField] VideoPlayer videoPlayer;

    [Header("Videos")]
    [SerializeField] VideoClip videoIdle;
    [SerializeField] VideoClip videoAnalizando;
    [SerializeField] VideoClip videoEsperando;
    [SerializeField] VideoClip videoVictoria;
    [SerializeField] VideoClip videoConfusion;
    [SerializeField] VideoClip videoError;

    public void ReproducirIdle()
    {
        ReproducirLoop(videoIdle);
    }

    public void ReproducirAnalizando()
    {
        ReproducirLoop(videoAnalizando);
    }

    public void ReproducirEsperando()
    {
        ReproducirLoop(videoEsperando);
    }

    public void ReproducirVictoria()
    {
        ReproducirLoop(videoVictoria);
    }

    public void ReproducirConfusion()
    {
        ReproducirLoop(videoConfusion);
    }

    public void ReproducirError()
    {
        ReproducirLoop(videoError);
    }

    void ReproducirLoop(VideoClip clip)
    {
        if (clip == null)
        {
            Debug.LogError("ControladorVideoConsola: falta asignar un VideoClip.");
            return;
        }

        videoPlayer.isLooping = true;
        videoPlayer.clip = clip;
        videoPlayer.Play();
    }
}