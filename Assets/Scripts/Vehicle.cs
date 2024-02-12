using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class Vehicle : ManeuverAgent
{
    [SerializeField] private ManeuverTemplatesPanel _maneuverPanel;

    private void OnMouseEnter()
    {
        _maneuverPanel.Show(this);
    }

    private void OnMouseUpAsButton()
    {
        _maneuverPanel.ToggleLock();
    }

    private void OnMouseExit()
    {
        if (!_maneuverPanel.Locked)
        {
            _maneuverPanel.Hide();
        }
    }
}
