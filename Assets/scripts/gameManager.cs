#region IMPORT LIBRARIES
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
#endregion

public class GameManager : MonoBehaviour
{
    #region PRIVATE FIELDS

    private CreatingCards creatingCards;
    private bool cardPresent = false; // This will tell whether card is present on the table or not
    private List<Card> currentPlayerDeck;
    private List<Card> player1Cards;
    private List<Card> player2Cards;
    private List<Card> playedCards;

    #endregion

    #region SERIALIZED FIELDS

    [SerializeField] private TextMeshProUGUI player1CardsCountText;
    [SerializeField] private TextMeshProUGUI player2CardsCountText;
    [SerializeField] private TextMeshProUGUI playedCardsCountText;
    [SerializeField] private GameObject place1Button;
    [SerializeField] private GameObject place2Button;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private Image cardSpriteImage; // Reference to the UI element for displaying card sprite
    [SerializeField] private Image previousCardSpriteImage;  // Reference to the UI element for displaying previous card sprite
    [SerializeField] private Sprite noCardSprite;

    #endregion

    #region MONOBEHAVIOUR METHODS

    private void Start()
    {
        
        // Wait for a short delay and then initialize the game manager
        Invoke(nameof(InitializeCreatingCards), 0.05f);
        playedCards = new();
        place1Button.SetActive(true);
        place2Button.SetActive(false);
    }

    #endregion

    // Initialize the game manager and player 1 cards
    private void InitializeCreatingCards()
    {
        creatingCards = FindAnyObjectByType<CreatingCards>();
        player1Cards = creatingCards.player1Hand;
        player2Cards = creatingCards.player2Hand;
        player1CardsCountText.text = $"{player1Cards.Count}";
        player2CardsCountText.text = $"{player2Cards.Count}";
    }

    // Call the PlayTurn() function when the "PLACE" button is clicked
    #region "PLACED" BUTTON CLICKED

    public void PlayTurn(bool currentPlayer)
    {
        #region CHECKING WHICH PLAYER CLICKED THE "PLACE" BUTTON

        if (currentPlayer)
        {
            currentPlayerDeck = player1Cards;
        }
        else
        {
            currentPlayerDeck = player2Cards;
        }
        #endregion

        place1Button.SetActive(!currentPlayer);
        place2Button.SetActive(currentPlayer);
        Debug.Log("Player card count: " + currentPlayerDeck.Count);

        // Check if the current player has any cards left to play
        if (currentPlayerDeck.Count > 0)
        {
            Card currentPlayerCard = currentPlayerDeck[0];

            // Display the card sprite for the current player's card
            DisplayCardSprite(currentPlayerCard, cardSpriteImage);

            // Add the current player's card to the played cards list
            playedCards.Add(currentPlayerCard);

            if (cardPresent)
            {
                // Get the previous card played by the other player
                Card previousCard = playedCards.ElementAt(playedCards.Count - 2);

                Debug.Log("Player 1 plays: " + currentPlayerCard.ToString());

                DisplayCardSprite(previousCard, previousCardSpriteImage);

                // Check if the current player's card matches the previous card
                if (GetCardValue(currentPlayerCard) == GetCardValue(previousCard))
                {
                    Debug.Log("Cards match! Player wins the round.");

                    // Add the played cards to the current player's hand and clear the played cards list
                    currentPlayerDeck.AddRange(playedCards);
                    playedCards.Clear();
                    cardPresent = false;

                    place1Button.SetActive(currentPlayer);
                    place2Button.SetActive(!currentPlayer);
                }
            }

            else
            {
                cardPresent = true;
                previousCardSpriteImage.sprite = noCardSprite;
            }

            playedCardsCountText.text = $"{playedCards.Count}";

            // Remove the played card from the current player's hand
            currentPlayerDeck.RemoveAt(0);

            //Updating the Count text
            player1CardsCountText.text = $"{player1Cards.Count}";
            player2CardsCountText.text = $"{player2Cards.Count}";
        }

        else
        {
            Debug.Log("Game over.");
            place2Button.SetActive(false);
            place1Button.SetActive(false);
            gameOverText.SetActive(true);
        }
    }
    #endregion

    #region DISPLAY CARD FUNCTION
    void DisplayCardSprite(Card card, Image image)
    {
        if (image != null)
        {
            image.sprite = card.Sprite;
        }
    }
    #endregion

    // Get the value of a card
    private int GetCardValue(Card card)
    {
        return card.Value;
    }

}
