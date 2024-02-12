using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class ManeuverTemplatesPanelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private GameObject _actualButton;

        private ManeuverTemplatesPanel _panel;
        private ManeuverTemplate _template;

        private ManeuverAgent _vehicle;

        private void Awake()
        {
            _actualButton = transform.GetChild(0).gameObject;
            _actualButton.GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _actualButton.GetComponent<Button>().onClick.RemoveAllListeners();
        }

        public void Initialize(ManeuverTemplatesPanel panel, ManeuverTemplate template)
        {
            _panel = panel;
            _template = template;
        }

        public void SetVehicle(ManeuverAgent vehicle)
        {
            _vehicle = vehicle;
        }

        public void Hide()
        {
            _actualButton.SetActive(false);
        }

        public void Show()
        {
            _actualButton.SetActive(true);
        }

        private void OnClick()
        {
            _template.MoveVehicle(_vehicle);
            _template.Hide();
            _panel.Unlock();
            _panel.Hide();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _template.SetUpAtEndOf(_vehicle);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _template.Hide();
        }
    }
}
