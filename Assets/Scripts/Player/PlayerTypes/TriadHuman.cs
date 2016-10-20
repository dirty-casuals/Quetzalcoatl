using UnityEngine;

public class TriadHuman : TriadPlayer {

    private CardSlot currentCardSlot;

    protected override void InitializeTriadPlayer() {
    }

    protected override void UpdateTriadPlayer() {
        CardSlot cardSlot = CardUnderMouse();
        HoveringOverCard( cardSlot );
        CardDropped( cardSlot );
        DraggingCard();
    }

    protected override void UpdateTriadPlayerControls() {
        if ( Input.GetMouseButtonDown( 0 ) ) {
            draggingCard = true;
        }
        if ( Input.GetMouseButtonUp( 0 ) ) {
            draggingCard = false;
        }
    }

    private CardSlot CardUnderMouse() {
        Vector3 cameraPosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        Vector2 mousePosition = new Vector2( cameraPosition.x, cameraPosition.y );
        RaycastHit2D[] hit = Physics2D.RaycastAll( mousePosition, Vector2.zero );
        CardSlot closestCardSlot = null;
        int sortOrder = int.MinValue;

        for ( int i = 0; i < hit.Length; i++ ) {
            SpriteRenderer layer = hit[i].transform.GetComponent<SpriteRenderer>();
            CardSlot cardSlot = hit[i].transform.GetComponent<CardSlot>();

            if ( hit[i].collider != null
                 && cardSlot != null
                 && layer != null
                 && sortOrder < layer.sortingOrder ) {
                sortOrder = layer.sortingOrder;
                closestCardSlot = cardSlot;
            }
        }
        return closestCardSlot;
    }

    private void UpdateCardHover( CardSlot newCard ) {
        if ( currentCardSlot != null ) {
            currentCardSlot.GetComponent<CardSlot>().ReturnCardToSlot();
        }
        newCard.GetComponent<CardSlot>().HoverCardInSlot();
        currentCardSlot = newCard;
    }

    private void HoveringOverCard( CardSlot slot ) {
        if ( slot != null
             && slot != currentCardSlot
             && !draggingCard ) {
            UpdateCardHover( slot );
        }
    }

    private void CardDropped( CardSlot slot ) {
        if ( slot == null
             && currentCardSlot != null
             && !draggingCard ) {
            currentCardSlot.ReturnCardToSlot();
            currentCardSlot = null;
        }
    }

    private void DraggingCard() {
        if ( currentCardSlot != null && draggingCard ) {
            currentCardSlot.CardInHand();
        }
    }
}
