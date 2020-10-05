using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_ApperanceManager : MonoBehaviour
{
    // Manages the appearance of a card.
    // Variables
    public SpriteRenderer front, back;
    public Sprite[] cardBackColorSprites = new Sprite[3];
    public void MatchCardAppearance(Card cardInfo)
    {
        // Put the front sprite the sprite it's given
        front.sprite = cardInfo.cardSprite;
        // See what color it is and give it it's apporiate back color
        switch (cardInfo.cardBackColor)
        {
            case Card.CardColor.Red:
                back.sprite = cardBackColorSprites[0];
                break;
            case Card.CardColor.Green:
                back.sprite = cardBackColorSprites[1];
                break;
            case Card.CardColor.Blue:
                back.sprite = cardBackColorSprites[2];
                break;
            default:
                print("Error in matching appearence of the back!");
                break;
        }
    }

    public void SelectColorChange() => front.color = front.color = new Color32(255, 141, 134, 255);
    public void DeselectColorChange() => front.color = new Color32(255, 255, 255, 255);
}
