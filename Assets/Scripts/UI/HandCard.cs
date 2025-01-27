using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class HandCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler,
        IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Canvas _canvas;
        private RectTransform _canvasRectTransform;
        private RectTransform _rectTransform;
        private Vector2 _startAnchoredPos;

        private Canvas _tempCanvas;
        private bool _ignoreHover = false;
        private bool _isFollowingPointer = false;
        private Vector2 _followOffset;

        private void Start()
        {
            _canvas = GetComponentInParent<Canvas>();
            _canvasRectTransform = _canvas.transform as RectTransform;
            _rectTransform = GetComponent<RectTransform>();
            _startAnchoredPos = _rectTransform.anchoredPosition;
        }

        private void Update()
        {
            if (_isFollowingPointer)
            {
                Vector2 target = GetAnchoredMousePos() - _followOffset;
                _rectTransform.DOAnchorPos(target, 0.5f);
            }
        }

        private Vector2 GetAnchoredMousePos()
        {
            Vector2 canvasSizeDelta = _canvasRectTransform.sizeDelta;
            Vector2 factors = new Vector2(
                canvasSizeDelta.x / Screen.width,
                canvasSizeDelta.y / Screen.height
            );
            return Input.mousePosition * factors;
        }

        private void ToggleHover(bool hovered)
        {
            if (_ignoreHover) return;

            float targetY = hovered ? _startAnchoredPos.y + 45.0f : _startAnchoredPos.y;
            _rectTransform.DOAnchorPosY(targetY, 0.3f);
        }

        private void MoveToFront()
        {
            // used to display in front of everything else without changing hierarchy
            _tempCanvas = gameObject.AddComponent<Canvas>();
            _tempCanvas.overrideSorting = true;
            _tempCanvas.sortingOrder = 100;
        }

        private void GoBackToHand()
        {
            Destroy(_tempCanvas);
            _rectTransform.DOAnchorPos(_startAnchoredPos, 0.5f).OnComplete(() => _ignoreHover = false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ToggleHover(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ToggleHover(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isFollowingPointer)
            {
                _isFollowingPointer = false;
                GoBackToHand();
            }
            else
            {
                _isFollowingPointer = true;
                _ignoreHover = true;
                _followOffset = GetAnchoredMousePos() - _rectTransform.anchoredPosition;
                MoveToFront();
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            DOTween.Kill(_rectTransform);
            _ignoreHover = true;

            MoveToFront();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            GoBackToHand();
        }
    }
}