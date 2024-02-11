using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManeuverTemplate : ManeuverAgent
{
    public void Hide()
    {
        transform.localPosition = Vector3.zero;
    }

    public void MoveVehicle(ManeuverAgent vehicle)
    {
        SetUpAtEndOf(vehicle);
        vehicle.SetUpAtEndOf(this);
    }
}
