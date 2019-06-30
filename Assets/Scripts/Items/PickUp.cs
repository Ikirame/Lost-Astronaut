using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    public enum PickUpType
    {
        MedKit,
        Oxygen,
        ShipPiece
    }

    public PickUpType type;
}
