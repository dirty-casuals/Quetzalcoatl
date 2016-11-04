using UnityEngine;

public class CardSlot : MonoBehaviour {
    private Card _card;

    private void Start() {
        _card = GetComponentInChildren<Card>();
    }

    public Card card {
        get {
            return _card;
        }
    }

    public void CardInHand() {
        Vector3 cameraPosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        Vector2 mousePosition = new Vector2( cameraPosition.x, cameraPosition.y );
        _card.transform.position = mousePosition;
        _card.state = CardState.CARD_IN_HAND;
    }

    public void HoverCardInSlot() {
        if ( _card.state == CardState.CARD_IN_HAND ) {
            return;
        }
        _card.HoverOverCard();
    }

    public void ReturnCardToSlot() {
        if ( _card.state == CardState.CARD_IN_BOARD ) {
            return;
        }
        _card.PlayCard();
    }
}
