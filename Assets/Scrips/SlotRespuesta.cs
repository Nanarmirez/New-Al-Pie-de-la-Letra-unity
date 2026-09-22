using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotRespuesta : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Sprite estadoInactivo;
    [SerializeField] Sprite estadoActivo;

    // Número que identifica este slot
    [SerializeField] int posicion;

    private PalabraItem itemActual = null;
    private Image imagen;

    void Start()
    {
        imagen = GetComponent<Image>();
        imagen.sprite = estadoInactivo;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemActual == null && eventData.pointerDrag != null)
            imagen.sprite = estadoActivo;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (itemActual == null)
            imagen.sprite = estadoInactivo;
    }

    public void OnDrop(PointerEventData eventData)
    {
        PalabraItem item = eventData.pointerDrag.GetComponent<PalabraItem>();

        if (item != null)
        {
            if (itemActual != null)
                itemActual.VolverAlBanco();

            itemActual = item;
            item.ColocarEnSlot(this);
            imagen.sprite = estadoActivo;
        }
    }

    public void LiberarSlot()
    {
        itemActual = null;
        imagen.sprite = estadoInactivo;
    }

    // Nos dice qué palabra tiene este slot
    public string ObtenerPalabra()
    {
        return itemActual != null ? itemActual.palabra : "";
    }

    // Nos dice qué número de posición tiene este slot
    public int ObtenerPosicion()
    {
        return posicion;
    }

    // Para comprobar visualmente qué tiene cada slot
    public void MostrarInformacion()
    {
        string palabra = ObtenerPalabra();

        Debug.Log("Soy el slot " + posicion + " y tengo: " + palabra);
    }
}