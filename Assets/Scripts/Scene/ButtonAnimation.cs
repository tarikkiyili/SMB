using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    private Vector3 originalScale;
    private RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        originalScale = rect.localScale;

        Shadow shadow1 = gameObject.AddComponent<Shadow>();
        shadow1.effectColor = new Color(0, 0, 0, 0.3f);
        shadow1.effectDistance = new Vector2(7, -7);

        Shadow shadow2 = gameObject.AddComponent<Shadow>();
        shadow2.effectColor = new Color(0, 0, 0, 0.1f);
        shadow2.effectDistance = new Vector2(9, -9);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rect.localScale = originalScale * 0.95f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rect.localScale = originalScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var manager = FindAnyObjectByType<MakePersistent>();
        if (manager != null && manager.clickSound != null)
        {
            manager.clickSound.Play();
        }
    }
}
