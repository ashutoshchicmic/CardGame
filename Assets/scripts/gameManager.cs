using UnityEngine;
using System.Collections.Generic;

public class gameManager : MonoBehaviour
{
    public List<Card> deck;
    public List<Card> player1Hand;
    public List<Card> player2Hand;
    public List<Card> playedCards=new List<Card>();
    
    void Start()
    {
        deck = GenerateDeck();
        ShuffleDeck();
        player1Hand = new List<Card>();
        player2Hand = new List<Card>();
        for (int i = 0; i < 26; i++)
        {
            player1Hand.Add(deck[i]);
            player2Hand.Add(deck[i + 26]);
        }
    }

    List<Card> GenerateDeck()
    {
        List<Card> newDeck = new List<Card>();

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

public enum CardSuit
{
    Hearts,
    Diamonds,
    Clubs,
    Spades
}

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
