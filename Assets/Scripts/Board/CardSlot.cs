using UnityEngine;

public class CardSlot : MonoBehaviour {
    public Card card;
    private CardMovement cardMovement;

    private void Start() {
        cardMovement = card.GetComponent<CardMovement>();
    }

    public void CardInHand() {
        Vector3 cameraPosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        Vector2 mousePosition = new Vector2( cameraPosition.x, cameraPosition.y );
        card.transform.position = mousePosition;
        card.state = CardState.CARD_IN_HAND;
    }

    public void HoverCardInSlot() {
        if ( card.state == CardState.CARD_IN_HAND ) {
            return;
        }
        cardMovement.HoverCard();
    }

    public void ReturnCardToSlot() {
        if ( card.state == CardState.CARD_IN_BOARD ) {
            return;
        }
        cardMovement.StopHover();
    }
}
