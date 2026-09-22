using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BotonEstados : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Sprite estadoNormal;
    [SerializeField] Sprite estadoHover;
    [SerializeField] Sprite estadoPressed;

    private Image imagen;

    void Start()
    {
        imagen = GetComponent<Image>();
        imagen.sprite = estadoNormal;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        imagen.sprite = estadoHover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        imagen.sprite = estadoNormal;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        imagen.sprite = estadoPressed;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        imagen.sprite = estadoHover;
    }
}
