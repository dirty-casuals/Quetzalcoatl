﻿using UnityEngine;
using System.Collections;

public class CardMovement : MonoBehaviour {

    [SerializeField]
    private float verticalHover;
    [SerializeField]
    private float horizontalHover;
    private Vector2 startPosition;
    private Card card;

    private void Start() {
        card = GetComponent<Card>();
        startPosition = transform.position;
    }

    public void HoverCard() {
        if ( card.state == CardState.CARD_IN_BOARD ) {
            return;
        }
        StopAllCoroutines();
        StartCoroutine( MoveCardToHoverPosition() );
    }

    public void StopHover() {
        StopAllCoroutines();
    }

    private IEnumerator MoveCardToHoverPosition() {
        Vector3 targetPosition = new Vector3( ( transform.position.x + horizontalHover ),
                                              ( transform.position.y + verticalHover ), 0.0f );
        while ( transform.position != targetPosition ) {
            transform.position = Vector3.MoveTowards( transform.position, targetPosition, 0.2f );
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator MoveCardToStartPosition() {
        card.state = CardState.CARD_IN_DECK;
        Vector3 targetPosition = new Vector3( startPosition.x, startPosition.y, 0.0f );
        while ( transform.position != targetPosition ) {
            transform.position = Vector3.MoveTowards( transform.position, targetPosition, 0.4f );
            yield return new WaitForEndOfFrame();
        }
    }
}
