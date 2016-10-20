using UnityEngine;
using System.Collections;

public enum CardState {
    CARD_IN_DECK,
    CARD_IN_HAND,
    CARD_IN_BOARD,
}

public class Card : MonoBehaviour {
    public CardState state = CardState.CARD_IN_DECK;
}
