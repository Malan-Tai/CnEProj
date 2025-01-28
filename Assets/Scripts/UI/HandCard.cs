using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class HandCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler,
        IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public delegate void CardFocusHandler(GameObject card, bool focused);
        public static event CardFocusHandler OnCardFocused;

        
        private Canvas _rootCanvas;
        private Canvas _cardCanvas;
        private RectTransform _rootCanvasRectTransform;
        private RectTransform _rectTransform;
        private LayoutElement _layoutElement;
        private Vector2 _startAnchoredPos;

        private bool _ignoreInput = false;
        private bool _ignoreHover = false;
        private bool _isFollowingPointer = false;
        private bool _isDragging = false;
        private Vector2 _followOffset;

        private void Start()
        {
            OnCardFocused += OnOtherCardFocused;
            ClickCatcher.OnClickCaught += OnClickCaught;

            _cardCanvas = GetComponentInParent<Canvas>();
            _rootCanvas = GetComponentInParent<Canvas>().rootCanvas;
            _rootCanvasRectTransform = _rootCanvas.transform as RectTransform;
            _rectTransform = GetComponent<RectTransform>();
            _layoutElement = GetComponentInParent<LayoutElement>();
            _startAnchoredPos = _rectTransform.anchoredPosition;
        }

        private void OnDestroy()
        {
            OnCardFocused -= OnOtherCardFocused;
            ClickCatcher.OnClickCaught -= OnClickCaught;
        }

        private void Update()
        {
            if (_isFollowingPointer)
            {
                Vector2 target = GetAnchoredMousePos() - _followOffset;
                _rectTransform.DOAnchorPos(target, 0.5f);
            }
        }

        private void OnOtherCardFocused(GameObject card, bool focused)
        {
            if (card != gameObject)
            {
                _ignoreInput = focused;
            }
        }

        private void OnClickCaught(PointerEventData eventData)
        {
            if (_isFollowingPointer)
            {
                _isFollowingPointer = false;
                GoBackToHand();
            }
        }

        private Vector2 GetAnchoredMousePos()
        {
            Vector2 canvasSizeDelta = _rootCanvasRectTransform.sizeDelta;
            Vector2 factors = new Vector2(
                canvasSizeDelta.x / Screen.width,
                canvasSizeDelta.y / Screen.height
            );
            return Input.mousePosition * factors;
        }

        private void ToggleHover(bool hovered)
        {
            if (_ignoreHover || _ignoreInput) return;

            float targetY = hovered ? _startAnchoredPos.y + 45.0f : _startAnchoredPos.y;
            _rectTransform.DOAnchorPosY(targetY, 0.3f);
        }

        private void MoveToFront()
        {
            _layoutElement.ignoreLayout = true;

            _cardCanvas.overrideSorting = true;
            _cardCanvas.sortingOrder = 100;
            
            OnCardFocused?.Invoke(gameObject, true);
        }

        private void GoBackToHand()
        {
            _layoutElement.ignoreLayout = false;

            _cardCanvas.overrideSorting = false;
            _cardCanvas.sortingOrder = 0;
            _rectTransform.DOAnchorPos(_startAnchoredPos, 0.5f).OnComplete(() =>
            {
                _ignoreHover = false;
                DOTween.Kill(_rectTransform);
            });
            
            OnCardFocused?.Invoke(gameObject, false);
        }

        public void DoSpawn()
        {
            Vector2 target = _startAnchoredPos;
            _rectTransform.anchoredPosition = new Vector2(target.x, target.y + 45.0f);
            _rectTransform.DOAnchorPos(target, 0.8f);
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
            if (_isDragging || _ignoreInput) return;
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
            if (_isFollowingPointer || _ignoreInput) return;

            DOTween.Kill(_rectTransform);
            _ignoreHover = true;
            _isDragging = true;

            MoveToFront();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isFollowingPointer || _ignoreInput) return;

            _rectTransform.anchoredPosition += eventData.delta / _rootCanvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_isFollowingPointer || _ignoreInput) return;

            _isDragging = false;
            GoBackToHand();
        }
    }
}