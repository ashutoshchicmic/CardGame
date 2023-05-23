#region IMPORT LIBRARIES
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
#endregion
// Enumeration for the card suits
public enum CardSuit
{
    Spades,
    Hearts,
    Clubs,
    Diamonds
    
}

// Class representing a card
#region CARD CLASS

public class Card
{
    public int Value { get; private set; }
    public CardSuit Suit { get; private set; }
    public Sprite Sprite { get; private set; }

    public Card(int value, CardSuit suit, Sprite sprite)
    {
        Value = value;
        Suit = suit;
        Sprite = sprite;
    }

    public override string ToString()
    {
        return Value.ToString() + " of " + Suit.ToString();
    }
}
#endregion

public class CreatingCards : MonoBehaviour
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
        player1Hand = new List<Card>();
        player2Hand = new List<Card>();
        deck = GenerateDeck();       // Generate a new deck of cards
        ShuffleDeck();               // Shuffle the deck
        

        // Distribute cards to players
        for (int i = 0; i < 26; i++)
        {
            player1Hand.Add(deck[i]);
            player2Hand.Add(deck[i + 26]);
        }
    }

    #endregion

    // Generate a new deck of cards

    #region CREATING CARDS
    List<Card> GenerateDeck()
    {
        List<Card> newDeck = new List<Card>();
        string spriteName = "cardSprite";
        int spriteNumber=0;
        Sprite[] sprite = Resources.LoadAll<Sprite>(spriteName);
        // Loop through the card suits and values to create all the cards
        for (int suit = 0; suit < 4; suit++)
        {
            for (int value = 1; value <= 13; value++)
            {
                Card card = new Card(value, (CardSuit)suit, sprite[spriteNumber]);
                spriteNumber++;
                newDeck.Add(card);
            }
        }
        print(spriteNumber);
        return newDeck;
    }

    #endregion

    // Shuffle the deck of cards
    #region SHUFFLING THE DECK
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

    #endregion
}
