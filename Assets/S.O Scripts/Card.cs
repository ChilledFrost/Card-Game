using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Card")]
// Made it a scriptable object becuz they're dope B)
public class Card : ScriptableObject
{
    // Enums so it's easier...?
    public enum CardType
    {
        Spades,
        Diamonds,
        Hearts,
        Clubs
    }

    public enum CardColor
    {
        Red,
        Green,
        Blue
    }

    // Data
    public Sprite cardSprite;
    public int cardValue;
    public CardType cardSymbol;
    public CardColor cardBackColor;
}
