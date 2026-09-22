using UnityEngine;

public class LimpiarOracion : MonoBehaviour
{
    [SerializeField] BancoPalabras banco;
    [SerializeField] PalabraItem[] palabras;

    [Header("Reinicio de prueba")]
    [SerializeField] ControladorAnalisis controladorAnalisis;

    [Header("Texto de consola")]
    [SerializeField] TextoConsolaControlador textoConsola;

    public void Limpiar()
    {
        // Primero sacamos todas las palabras de sus slots
        foreach (PalabraItem palabra in palabras)
        {
            if (palabra.slotActual != null)
            {
                palabra.slotActual.LiberarSlot();
                palabra.slotActual = null;
            }
        }

        // Después devolvemos todas las palabras al banco
        for (int i = 0; i < palabras.Length; i++)
        {
            PalabraItem palabra = palabras[i];

            palabra.transform.SetParent(
                banco.transform,
                false
            );

            palabra.transform.SetSiblingIndex(i);
        }

        // Reiniciamos el estado de la prueba
        if (controladorAnalisis != null)
        {
            controladorAnalisis.ReiniciarPrueba();
        }

        // Registramos el reinicio
        if (textoConsola != null)
        {
            textoConsola.RegistrarReinicio();
        }
        else
        {
            Debug.LogError(
                "LimpiarOracion: no se asigno TextoConsolaControlador."
            );
        }
    }
}