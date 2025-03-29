using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 originalScale;
    private RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        originalScale = rect.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rect.localScale = originalScale * 0.95f; // biraz küçülerek "basılıyor" hissi
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rect.localScale = originalScale;
    }
}
