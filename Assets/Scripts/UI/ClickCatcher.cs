using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace UI
{
    public class ClickCatcher : MonoBehaviour, IPointerClickHandler
    {
        public delegate void ClickCatcherHandler(PointerEventData eventData);
        public static event ClickCatcherHandler OnClickCaught;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClickCaught?.Invoke(eventData);
        }
    }
}
