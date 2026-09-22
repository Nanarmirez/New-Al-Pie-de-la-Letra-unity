using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PalabraItem : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [SerializeField] Sprite estadoNormal;
    [SerializeField] Sprite estadoHover;
    [SerializeField] Sprite estadoArrastrando;

    [SerializeField] public string palabra;

    [HideInInspector] public SlotRespuesta slotActual = null;

    private Vector3 originalPos;
    private Transform originalParent;
    private int originalSiblingIndex;

    // Posición que la palabra tiene originalmente en el banco
    private int posicionOriginalBanco;

    private Canvas canvas;
    private Image imagen;
    private LayoutElement layoutElement;

    void Start()
    {
        imagen = GetComponent<Image>();
        canvas = GetComponentInParent<Canvas>();
        layoutElement = GetComponent<LayoutElement>();

        originalPos = transform.position;
        originalParent = transform.parent;
        originalSiblingIndex = transform.GetSiblingIndex();

        // Guardamos para siempre la posición inicial de esta palabra
        posicionOriginalBanco = transform.GetSiblingIndex();

        TMP_Text tmp = GetComponentInChildren<TMP_Text>();

        if (tmp != null)
        {
            palabra = tmp.text;
        }
        else
        {
            Text t = GetComponentInChildren<Text>();

            if (t != null)
                palabra = t.text;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (slotActual == null)
            imagen.sprite = estadoHover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (slotActual == null)
            imagen.sprite = estadoNormal;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // -----------------------------------
        // Si estaba en un SLOT
        // -----------------------------------

        if (slotActual != null)
        {
            // Liberamos el slot.
            slotActual.LiberarSlot();
            slotActual = null;

            // Su lugar de regreso será el BANCO,
            // usando su posición original.
            BancoPalabras banco =
                FindAnyObjectByType<BancoPalabras>();

            if (banco != null)
            {
                originalParent = banco.transform;
                originalSiblingIndex = posicionOriginalBanco;
            }
        }
        else
        {
            // -----------------------------------
            // Si estaba en el BANCO
            // -----------------------------------

            originalPos = transform.position;
            originalParent = transform.parent;
            originalSiblingIndex = transform.GetSiblingIndex();
        }

        if (layoutElement != null)
            layoutElement.ignoreLayout = true;

        imagen.raycastTarget = false;
        imagen.sprite = estadoArrastrando;

        transform.SetParent(canvas.transform, true);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvas.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out Vector3 worldPos
        );

        transform.position = worldPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (layoutElement != null)
            layoutElement.ignoreLayout = false;

        imagen.raycastTarget = true;
        imagen.sprite = estadoNormal;

        PointerEventData pointerData = eventData;

        List<RaycastResult> resultados =
            new List<RaycastResult>();

        EventSystem.current.RaycastAll(
            pointerData,
            resultados
        );

        // -----------------------------------
        // 1. ¿Cayó en un SLOT?
        // -----------------------------------

        foreach (RaycastResult resultado in resultados)
        {
            SlotRespuesta slot =
                resultado.gameObject.GetComponentInParent<SlotRespuesta>();

            if (slot != null)
            {
                slot.OnDrop(eventData);
                return;
            }
        }

        // -----------------------------------
        // 2. ¿Cayó en el BANCO?
        // -----------------------------------

        foreach (RaycastResult resultado in resultados)
        {
            BancoPalabras banco =
                resultado.gameObject.GetComponentInParent<BancoPalabras>();

            if (banco != null)
            {
                banco.InsertarPalabra(
                    this,
                    eventData.position
                );

                return;
            }
        }

        // -----------------------------------
        // 3. No cayó en ningún lugar válido
        // -----------------------------------
        //
        // Si venía del banco:
        // vuelve a su posición anterior.
        //
        // Si venía de un slot:
        // vuelve a su posición original del banco.
        // -----------------------------------

        transform.SetParent(
            originalParent,
            false
        );

        transform.SetSiblingIndex(
            originalSiblingIndex
        );

        transform.position = originalPos;
    }

    public void ColocarEnSlot(SlotRespuesta slot)
    {
        slotActual = slot;

        transform.SetParent(
            slot.transform,
            false
        );

        transform.localPosition = Vector3.zero;

        imagen.sprite = estadoNormal;
    }

    public void VolverAlBanco()
    {
        slotActual = null;

        BancoPalabras banco =
            FindAnyObjectByType<BancoPalabras>();

        if (banco != null)
        {
            transform.SetParent(
                banco.transform,
                false
            );

            // Al sacar una palabra de un slot mediante
            // otra palabra que ocupa ese slot,
            // vuelve a su posición original.
            transform.SetSiblingIndex(
                posicionOriginalBanco
            );
        }

        imagen.sprite = estadoNormal;
    }
}