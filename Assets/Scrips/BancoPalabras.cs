using UnityEngine;

public class BancoPalabras : MonoBehaviour
{
    public void InsertarPalabra(PalabraItem palabra, Vector2 posicionMouse)
    {
        int nuevaPosicion = ObtenerPosicion(posicionMouse);

        // Metemos la palabra nuevamente en el Content
        palabra.transform.SetParent(transform, false);

        // La colocamos en el orden correspondiente
        palabra.transform.SetSiblingIndex(nuevaPosicion);
    }

    int ObtenerPosicion(Vector2 posicionMouse)
    {
        RectTransform contenido = GetComponent<RectTransform>();

        // Convertimos la posición del mouse a la posición
        // dentro del Content.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            contenido,
            posicionMouse,
            null,
            out Vector2 posicionLocal
        );

        // Revisamos cada palabra del banco
        for (int i = 0; i < transform.childCount; i++)
        {
            RectTransform hijo = transform.GetChild(i) as RectTransform;

            // Posición local del centro de esta palabra
            float posicionY = hijo.localPosition.y;

            // Si el mouse está por encima de esta palabra,
            // colocamos la nueva antes de ella.
            if (posicionLocal.y > posicionY)
            {
                return i;
            }
        }

        // Si el mouse está debajo de todas,
        // la ponemos al final.
        return transform.childCount;
    }
}