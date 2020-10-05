using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class CardHolderManager : MonoBehaviour
{
    // Variables 
    public GameObject cardPrefab;
    private Transform cardHolder;
    // Selection Logic Bools
    private bool isRun;  
    private bool isMultiple;
    // Throw area
    public BoxCollider throwArea;
    // Bools 
    public bool isPlayer;
    public bool isTurn;
    // Floats
    public float tweenTime = .25f;

    // Testing
    public Card test;
    public Card test2;
    public Card test3;

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
        AddCard(test);
        AddCard(test2);
        AddCard(test3);
        AddCard(test);
        AddCard(test3);
        yield return null;
    }

    // Base functions of the card holder I think?
    public void AddCard(Card cardInfo)
    {
        GameObject addedCard = (GameObject) Instantiate(cardPrefab, cardHolder);
        cardsInHolder.Add(addedCard);
        CardManager cardManagerOfCard = addedCard.GetComponent<CardManager>();
        // if it's player and it's clicked Remove card
        if (isPlayer)
            cardManagerOfCard.clickedEvent.AddListener(SelectOrDeselectCard);
        // Set up it's info
        cardManagerOfCard.associatedCard = cardInfo;
        cardManagerOfCard.SetUpApperance();

        ArrangeCards();
    }
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
            if (isMultiple && selectedCards.Count == 1) // If it's currently a multiple and there's only one card then it's not a multiple anymroe
            {
                isMultiple = false;
            }
            if (isRun) // If it's a run and one card gets deselected, just deselect them all for simplicity.
            {
                for (int i = 0; i < selectedCards.Count; i++)
                {
                    selectedCards[i].isSelected = false;
                    selectedCards[i].Clicked(); // Simulate click 
                }
                selectedCards = new List<CardManager>();
                isRun = false;
            }
            return;
        }
        // Check if they're the same symbol and if it's a run
        if(!isMultiple)
        {
            if (card.associatedCard.cardSymbol == selectedCards[0].associatedCard.cardSymbol)
            {
                if (selectedCards[selectedCards.Count - 1].associatedCard.cardValue - card.associatedCard.cardValue == -1)
                {
                    card.isSelected = true;
                    selectedCards.Add(card);
                    isRun = true;
                    print(isRun);
                    return;
                }
            }
        }

        // Check if they're the same number
        if (!isRun)
        {
            if (selectedCards[selectedCards.Count - 1].associatedCard.cardValue == card.associatedCard.cardValue)
            {
                card.isSelected = true;
                selectedCards.Add(card);
                isMultiple = true;
                return;
            }
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
            card.transform.parent = throwArea.gameObject.transform;
            card.transform.DOMove(new Vector3(Random.Range(throwArea.bounds.min.x, throwArea.bounds.max.x), 2, Random.Range(throwArea.bounds.min.z, throwArea.bounds.max.z)), tweenTime);
            card.transform.DORotate(new Vector3(270, 0, 0), tweenTime);
        }
        
        ArrangeCards();
    }
    public void ArrangeCards()
    {
        // -2.15f is the distance of how far apart they are from each other
        for (int i = 0; i < cardsInHolder.Count; i++)
        {
            cardsInHolder[i].transform.DOLocalMoveX(i * -2.15f + cardsInHolder.Count - 1, tweenTime);
        }
    }
}