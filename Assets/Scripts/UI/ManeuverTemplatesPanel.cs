using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class ManeuverTemplatesPanel : MonoBehaviour
    {
        // the object containing all the templates instance in the game scene
        [SerializeField] private GameObject _templatesContainer;

        public bool Locked { get; private set; } = false;

        private readonly List<ManeuverTemplatesPanelButton> _buttons = new List<ManeuverTemplatesPanelButton>();

        void Start()
        {
            var rows = GetComponentsInChildren<ManeuverTemplatesPanelRow>();
            foreach (ManeuverTemplatesPanelRow row in rows)
            {
                string speedStr = row.Speed.ToString();
                var buttons = row.GetComponentsInChildren<ManeuverTemplatesPanelButton>();
                foreach (ManeuverTemplatesPanelButton button in buttons)
                {
                    Transform template = _templatesContainer.transform.Find(speedStr + button.name);
                    if (template != null)
                    {
                        button.Initialize(this, template.GetComponent<ManeuverTemplate>());
                        _buttons.Add(button);
                    }
                }
            }

            gameObject.SetActive(false);
        }

        public void Show(ManeuverAgent vehicle)
        {
            foreach (ManeuverTemplatesPanelButton button in _buttons)
            {
                button.SetVehicle(vehicle);
            }

            gameObject.SetActive(true);
        }

        public void Lock()
        {
            Locked = true;
        }

        public void Unlock()
        {
            Locked = false;
        }

        public void ToggleLock()
        {
            if (Locked) Unlock();
            else Lock();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
