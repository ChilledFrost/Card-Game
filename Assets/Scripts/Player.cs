using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[RequireComponent(typeof(CardHolderManager))]
public class Player : MonoBehaviour
{
    public int sum;
    public CardHolderManager holderManager;
    private void Awake() => holderManager = GetComponent<CardHolderManager>();

    private void Start()
    {
        holderManager.calculateSum.AddListener(CalculateSum);
    }

    private void CalculateSum()
    {
        sum = 0;
        for (int i = 0; i < holderManager.cardsInHolder.Count; i++)
        {
            sum += holderManager.cardsInHolder[i].GetComponent<CardManager>().associatedCard.cardValue;
        }
        print(sum);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S) && holderManager.hasCardsSelected == true)
        {
            holderManager.TurnInCards();
        }
    }
}
