#region IMPORT LIBRARIES
using UnityEngine;
using System.Collections.Generic;
#endregion
// Enumeration for the card suits
public enum CardSuit
{
    Hearts,
    Diamonds,
    Clubs,
    Spades
}

// Class representing a card
#region CARD CLASS

public class Card
{
    public int Value { get; private set; }
    public CardSuit Suit { get; private set; }

    public Card(int value, CardSuit suit)
    {
        Value = value;
        Suit = suit;
    }

    public override string ToString()
    {
        return Value.ToString() + " of " + Suit.ToString();
    }
}
#endregion

public class GameManager : MonoBehaviour
{
    #region FIELDS

    public List<Card> deck;             // The deck of cards
    public List<Card> player1Hand;      // Player 1's hand
    public List<Card> player2Hand;      // Player 2's hand
    public List<Card> playedCards = new List<Card>();    // List of cards played in the game

    #endregion

    #region MONOBEHAVIOUR METHODS

    void Start()
    {
        deck = GenerateDeck();       // Generate a new deck of cards
        ShuffleDeck();               // Shuffle the deck
        player1Hand = new List<Card>();
        player2Hand = new List<Card>();

        // Distribute cards to players
        for (int i = 0; i < 26; i++)
        {
            player1Hand.Add(deck[i]);
            player2Hand.Add(deck[i + 26]);
        }
    }

    #endregion

    // Generate a new deck of cards
    List<Card> GenerateDeck()
    {
        List<Card> newDeck = new List<Card>();

        // Loop through the card suits and values to create all the cards
        for (int suit = 0; suit < 4; suit++)
        {
            for (int value = 1; value <= 13; value++)
            {
                Card card = new Card(value, (CardSuit)suit);
                newDeck.Add(card);
            }
        }

        return newDeck;
    }

    // Shuffle the deck of cards
    void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }
}
