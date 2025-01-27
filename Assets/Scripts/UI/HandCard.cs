using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class HandCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private RectTransform _rectTransform;
        private float _startAnchoredY;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _startAnchoredY = _rectTransform.anchoredPosition.y;
        }

        private void ToggleHover(bool hovered)
        {
            float targetY = hovered ? _startAnchoredY + 45.0f : _startAnchoredY;
            _rectTransform.DOAnchorPosY(targetY, 0.3f);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ToggleHover(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ToggleHover(false);
        }
    }
}