using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolderManager : MonoBehaviour
{
    // Variables 
    public GameObject cardPrefab;
    private Transform cardHolder;

    public bool isPlayer;
    public bool isTurn;

    public Card test;
    public Card test2;

    public List<CardManager> selectedCards = new List<CardManager>();
    private List<GameObject> cardsInHolder = new List<GameObject>();

    private void Awake()
    {
        cardHolder = transform;
    }
    // Testing
    private void Start()
    {
        StartCoroutine(Testf());
    }
    IEnumerator Testf()
    {
        for (int i = 0; i < 2; i++)
        {
            AddCard(test);
            AddCard(test2);
            yield return new WaitForSeconds(.25f);
        }

    }

    // Base functions of the card holder I think?
    public void AddCard(Card cardInfo)
    {
        // Instaniate the prefab and add it into the cardsInHolder shit so  I can arrange them
        GameObject addedCard = (GameObject) Instantiate(cardPrefab, cardHolder);
        cardsInHolder.Add(addedCard);
        // Get the CardManager scripts
        CardManager cardManagerOfCard = addedCard.GetComponent<CardManager>();
        // if it's player and it's clicked Remove card
        if (isPlayer)
            cardManagerOfCard.clickedEvent.AddListener(SelectOrDeselectCard);
        // Set up it's info
        cardManagerOfCard.associatedCard = cardInfo;
        cardManagerOfCard.SetUpApperance();

        ArrangeCards();
    }
    // Logic for what cards we can select cuz like theres runs, you have the same number, and shit like that.
    public void SelectOrDeselectCard(CardManager card)
    {
        // If it's 0 then obivously we can select that card cause there's nothing in yet
        if (selectedCards.Count == 0)
        {
            card.isSelected = true;
            selectedCards.Add(card);
            return;
        }
        // If it's already selected just remove it 
        if (card.isSelected)
        {
            selectedCards.Remove(card);
            card.isSelected = false;
            return;
        }
        // Check if they're the same symbol and if it's a run
        if(card.associatedCard.cardSymbol == selectedCards[0].associatedCard.cardSymbol)
        {
            if (selectedCards[selectedCards.Count - 1].associatedCard.cardValue < card.associatedCard.cardValue)
            {
                print($"selected{card.associatedCard.cardValue}");
                card.isSelected = true;
                selectedCards.Add(card);
                return;
            }
        }

        // Check if they're the same number
        if (selectedCards[selectedCards.Count - 1].associatedCard.cardValue == card.associatedCard.cardValue)
        {
            card.isSelected = true;
            selectedCards.Add(card);
            return;
        }
    }

    public void TurnInCards(CardManager[] cards)
    {
        // If it's not our turn then we can't remove the card
        if (!isTurn)
        {
            return;
        }
        foreach (CardManager card in cards)
        {
            // Remove the listener becuz bugs
            card.clickedEvent.RemoveAllListeners();
            // Remove from the holder array
            cardsInHolder.Remove(card.gameObject);
            Destroy(card.gameObject);
        }
        
        ArrangeCards();
    }
    public void ArrangeCards()
    {
        // This basically centers the cards in a way? Makes them look cooler.
        // I have no clue why this formula works. 
        // -2.15f is the distance of how far apart they are from each other
        for (int i = 0; i < cardsInHolder.Count; i++)
        {
            cardsInHolder[i].transform.localPosition = new Vector3(i * -2.15f + cardsInHolder.Count - 1, 0, 0);
        }
    }
}
