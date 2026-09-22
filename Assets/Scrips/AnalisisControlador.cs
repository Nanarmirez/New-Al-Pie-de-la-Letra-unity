using System.Collections;
using UnityEngine;

public class ControladorAnalisis : MonoBehaviour
{
    [SerializeField] VerificadorNivel verificador;
    [SerializeField] ControladorVideoConsola consola;
    [SerializeField] ControladorVideoNilo nilo;
    [SerializeField] TextoConsolaControlador textoConsola;
    [SerializeField] float tiempoAnalisis = 4f;

    private bool analizando = false;
    private int puentePendiente = 0;

    void OnEnable()
    {
        if (nilo != null)
        {
            nilo.VideoTerminado += ReproducirPuentePendiente;
        }
    }

    void OnDisable()
    {
        if (nilo != null)
        {
            nilo.VideoTerminado -= ReproducirPuentePendiente;
        }
    }

    public void IniciarAnalisis()
    {
        if (analizando)
            return;

        StartCoroutine(ProcesoAnalisis());
    }

    IEnumerator ProcesoAnalisis()
    {
        analizando = true;
        puentePendiente = 0;

        consola.ReproducirAnalizando();

        textoConsola.MostrarFraseAnalisis();

        yield return new WaitForSeconds(tiempoAnalisis);

        int resultado = verificador.RevisarSlots();

        if (resultado == 0)
        {
            puentePendiente = 4;

            nilo.ReproducirVictoria1();
            consola.ReproducirVictoria();
            textoConsola.MostrarVictoria();
        }
        else if (resultado == 1)
        {
            puentePendiente = 5;

            nilo.ReproducirVictoria2();
            consola.ReproducirVictoria();
            textoConsola.MostrarVictoria();
        }
        else if (resultado >= 2)
        {
            puentePendiente = ObtenerPuenteValido(resultado);

            nilo.ReproducirVideoValido(resultado);
            consola.ReproducirConfusion();
            textoConsola.MostrarValido();
        }
        else
        {
            puentePendiente = 0;

            nilo.ReproducirConfusion();
            consola.ReproducirConfusion();
            textoConsola.MostrarDerrota();
        }

        analizando = false;
    }

    int ObtenerPuenteValido(int resultado)
    {
        switch (resultado)
        {
            case 2:
            case 3:
            case 4:
            case 6:
                return 2;

            case 5:
            case 11:
                return 3;

            case 7:
            case 8:
            case 9:
            case 10:
                return 1;

            default:
                return 0;
        }
    }

    void ReproducirPuentePendiente()
    {
        if (puentePendiente == 0)
            return;

        if (puentePendiente == 4)
        {
            nilo.ReproducirPuenteVictoria1();
        }
        else if (puentePendiente == 5)
        {
            nilo.ReproducirPuenteVictoria2();
        }
        else
        {
            nilo.ReproducirPuente(puentePendiente);
        }

        puentePendiente = 0;
    }

    public void ReiniciarPrueba()
    {
        analizando = false;
        puentePendiente = 0;

        nilo.ReproducirIdle();
        consola.ReproducirIdle();
    }
}