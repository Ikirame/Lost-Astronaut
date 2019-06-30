using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartManager : MonoBehaviour {

    private bool reactorPart = false;
    private bool tailPart = false;
    private bool wheelPart1 = false;
    private bool wheelPart2 = false;

    private PlayerHeartDisplay display;

	// Use this for initialization
	void Start () {

        display = GetComponent<PlayerHeartDisplay>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PickUpPart(ShipPart part)
    {
        ShipPart.PartType type = part.type;

        if (type == ShipPart.PartType.Reactor)
        {
            reactorPart = true;
        }
        if (type == ShipPart.PartType.Tail)
        {
            tailPart = true;
        }
        if (type == ShipPart.PartType.Wheel)
        {
            if (wheelPart1 == false)
                wheelPart1 = true;
            else
                wheelPart2 = true;
        }
        display.OnChangeShipPart(part);
    }

    public bool HasAllShipParts()
    {
        return (reactorPart && tailPart && wheelPart1 && wheelPart2);
    }
}
