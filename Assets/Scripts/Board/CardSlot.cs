using UnityEngine;

public class CardSlot : MonoBehaviour {

    public GameObject cardInSlot;
    private CardMovement cardMovement;

    private void Start() {
        cardMovement = cardInSlot.GetComponent<CardMovement>();
    }

    public void CardInHand() {
        Vector3 cameraPosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        Vector2 mousePosition = new Vector2( cameraPosition.x, cameraPosition.y );
        cardInSlot.transform.position = mousePosition;
        cardMovement.cardInHand = true;
    }

    public void HoverCardInSlot() {
        if ( cardMovement.cardInHand ) {
            return;
        }
        cardMovement.HoverCard();
    }

    public void ReturnCardToSlot() {
        cardMovement.StopHover();
    }
}
