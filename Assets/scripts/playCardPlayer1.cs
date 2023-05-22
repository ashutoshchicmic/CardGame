#region IMPORT LIBRARIES
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
#endregion

public class PlayCardPlayer1 : MonoBehaviour
{
    #region FIELDS

    GameManager gameManager;
    private List<Card> player1Cards;
    public TextMeshProUGUI playerCardNum;
    public TextMeshProUGUI previousCardNum;
    public GameObject place1;
    public GameObject place2;
    bool isCardMatch = false;
    #endregion

    #region MONOBEHAVIOUR METHODS

    private void Start()
    {
        // Wait for a short delay and then initialize the game manager
        Invoke(nameof(InitializeGameManager), 0.5f);
        place1.SetActive(true);
        place2.SetActive(false);
    }

    #endregion

    // Initialize the game manager and player 1 cards
    private void InitializeGameManager()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        player1Cards = gameManager.player1Hand;
    }

    // Call the PlayTurn() function when the "PLACE" button is clicked
    #region "PLACED" BUTTON CLICKED

    public void PlayTurn()
    {
        place1.SetActive(false);
        place2.SetActive(true);
        Debug.Log("Player 1 card count: " + player1Cards.Count);

        // Check if player 1 has any cards left to play
        if (player1Cards.Count > 0)
        {
            Card playerCard = player1Cards[0];
            playerCardNum.text = GetCardValue(playerCard).ToString();

            if (isCardMatch)
            {
                // Get the previous card played by player 2
                Card previousCard = gameManager.playedCards.Last();
                previousCardNum.text = GetCardValue(previousCard).ToString();
                Debug.Log("Player 1 plays: " + playerCard.ToString());

                // Check if the player 1 card matches the previous card
                if (GetCardValue(playerCard) == GetCardValue(previousCard))
                {
                    Debug.Log("Cards match! Player 1 wins the round.");

                    // Add the played cards to player 1's hand and clear the played cards list
                    player1Cards.AddRange(gameManager.playedCards);
                    gameManager.playedCards = new List<Card>();
                    isCardMatch = false;
                    place1.SetActive(true);
                    place2.SetActive(false);
                }
                else
                {
                    Debug.Log("Cards do not match. Next round.");

                    // Add the player 1 card to the played cards list
                    gameManager.playedCards.Add(playerCard);
                }
            }
            else
            {
                // Add the player 1 card to the played cards list
                gameManager.playedCards.Add(playerCard);
                isCardMatch = true;
            }

            // Remove the played card from player 1's hand
            player1Cards.RemoveAt(0);
        }
        else
        {
            Debug.Log("Game over.");
        }
    }
    #endregion

    // Get the value of a card
    private int GetCardValue(Card card)
    {
        return card.Value;
    }
}
