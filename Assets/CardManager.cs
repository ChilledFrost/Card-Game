using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(C_ApperanceManager))]
public class CardManager : MonoBehaviour
{
    //Event to know when clicked
    [HideInInspector]
    public UnityEvent<CardManager> clickedEvent;
    //Variables
    public Card associatedCard;
    private C_ApperanceManager apperanceManager;
    public bool isSelected = false;
    // Just for management yk make shit easier hopefully?
    private void Awake()
    {
        apperanceManager = GetComponent<C_ApperanceManager>();
    }

    public void SetUpApperance() => apperanceManager.MatchCardAppearance(associatedCard);
    private void OnMouseUp()
    {
        clickedEvent?.Invoke(this);
        Clicked();
    }
    private void Clicked()
    {
        switch(isSelected)
        {
            case true:
                apperanceManager.SelectColorChange();
                break;
            case false:
                apperanceManager.DeselectColorChange();
                break;
        }
    }
}
