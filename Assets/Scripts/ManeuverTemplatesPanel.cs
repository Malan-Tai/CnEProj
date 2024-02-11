using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManeuverTemplatesPanel : MonoBehaviour
{
    // the object containing all the templates instance in the game scene
    [SerializeField] private GameObject _templatesContainer;
    [SerializeField] private ManeuverAgent _vehicle;

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
                    button.Initialize(template.GetComponent<ManeuverTemplate>());
                    _buttons.Add(button);
                }
            }
        }

        Show(_vehicle);
    }

    private void Show(ManeuverAgent vehicle)
    {
        foreach (ManeuverTemplatesPanelButton button in _buttons)
        {
            button.SetVehicle(vehicle);
        }
    }
}
