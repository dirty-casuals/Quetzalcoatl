using UnityEngine;

public enum CardState {
    CARD_IN_DECK,
    CARD_IN_HAND,
    CARD_IN_BOARD,
}

public class Card : MonoBehaviour {
    public CardState state = CardState.CARD_IN_DECK;
    private BoardSection currentSection;
    private CardMovement cardMovement;

    private void Start() {
        cardMovement = GetComponent<CardMovement>();
    }

    private void Update() {
        if ( state != CardState.CARD_IN_HAND ) {
            return;
        }
        BoardSection newSection = GetClosestBoardSection();
        ChangeBoardSection( newSection );
    }

    public void HoverOverCard() {
        cardMovement.HoverCard();
    }

    public void PlayCard() {
        HighlightBoard( null );
        cardMovement.StopHover();
        if ( currentSection != null && !currentSection.hasCard ) {
            state = CardState.CARD_IN_BOARD;
            transform.position = currentSection.transform.position;
            currentSection.hasCard = true;
            return;
        }
        StartCoroutine( cardMovement.MoveCardToStartPosition() );
    }

    private BoardSection GetClosestBoardSection() {
        Vector3 cameraPosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        Vector2 cardPosition = new Vector2( cameraPosition.x, cameraPosition.y );
        RaycastHit2D[] hit = Physics2D.RaycastAll( cardPosition, Vector2.zero, 0.0f, 1 << LayerMask.NameToLayer( "Board" ) );
        BoardSection closestSection = null;
        float distance = float.MaxValue;

        for ( int i = 0; i < hit.Length; i++ ) {
            if ( hit[i].collider != null ) {
                BoardSection section = hit[i].transform.GetComponent<BoardSection>();
                if ( !section ) {
                    return null;
                }
                float sectionDistance = Vector2.Distance( transform.position, hit[i].transform.position );
                if ( sectionDistance < distance ) {
                    closestSection = section;
                    distance = sectionDistance;
                }
            }
        }
        return closestSection;
    }

    private void ChangeBoardSection( BoardSection section ) {
        if ( section == currentSection ) {
            return;
        }
        HighlightBoard( section );
        currentSection = section;
    }

    private void HighlightBoard( BoardSection section ) {
        if ( section == null ) {
            if ( currentSection != null ) {
                currentSection.GetComponent<Outline>().enabled = false;
                return;
            }
        }
        if ( section != null ) {
            section.GetComponent<Outline>().enabled = true;
        }
        if ( currentSection != null ) {
            currentSection.GetComponent<Outline>().enabled = false;
        }
    }
}
