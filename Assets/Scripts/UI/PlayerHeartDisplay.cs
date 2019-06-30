using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeartDisplay : MonoBehaviour
{
    // Slider for Oxygen bar
    public Slider oxygenSlider;

    // Full heart sprite
    public Sprite HeartSprite;

    // Empty heart sprite
    public Sprite NoHeartSprite;

    // List of heart sprites
    public List<Image> HeartImages = new List<Image>();

    public Image ReactorImage;
    public Sprite ReactorFull;

    public Image TailImage;
    public Sprite TailFull;

    public Image Wheel1Image;
    public Image Wheel2Image;
    public Sprite WheelFull;

    private void Start()
    {
        // Updates display
        HealthManager player = GetComponent<HealthManager>();
        OnChangeHeart(player.maxLives, player.lives);
    }

    public void OnChangeHeart(int totalLives, int remainingLives)
    {
        // Updates sprites
        for (var i = 0; i < totalLives; i++)
        {
            HeartImages[i].sprite = i >= remainingLives ? NoHeartSprite : HeartSprite;
        }
    }

    public void OnChangeOxygen(int oxygenValue)
    {
        oxygenSlider.value = oxygenValue;
    }

    public void OnChangeShipPart(ShipPart part)
    {
        ShipPart.PartType type = part.type;

        switch(type)
        {
            case ShipPart.PartType.Reactor:
                ReactorImage.sprite = ReactorFull;
                break;
            case ShipPart.PartType.Tail:
                TailImage.sprite = TailFull;
                break;
            case ShipPart.PartType.Wheel:
                if (Wheel1Image.sprite != WheelFull)
                {
                    Wheel1Image.sprite = WheelFull;
                } else
                {
                    Wheel2Image.sprite = WheelFull;
                }
                break;
        }
        Destroy(part.gameObject);
    }
}