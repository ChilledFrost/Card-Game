using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CardHolderManager))]
public class Player : MonoBehaviour
{
    public int sum;
    public CardHolderManager holderManager;
    private void Awake() => holderManager = GetComponent<CardHolderManager>();

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S) && holderManager.hasCardsSelected == true)
        {
            print("Go");
            holderManager.TurnInCards();
        }
    }
}
