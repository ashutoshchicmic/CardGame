#region IMPORT LIBRARIES
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
#endregion

public class PlayCardPlayer2 : MonoBehaviour
{
    #region FIELDS
    GameManager gameManager;
    private List<Card> Player2Cards;
    private bool IsCardMatch = true;
    public TextMeshProUGUI cardNum;
    public TextMeshProUGUI previousCardNum;
    public GameObject place1;
    public GameObject place2;
    #endregion


    #region MONOBEHAVIOUR METHODS
    private void Start()
    {
        // Wait for a short delay and then initialize the game manager
        Invoke(nameof(InitializeGameManager), 0.5f);
    }
    #endregion


    // Initialize the game manager and player 2 cards
    private void InitializeGameManager()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        Player2Cards = gameManager.player2Hand;
    }

    // Call the PlayTurn() function when the "PLACE" button is clicked
    #region "PLACE" BUTTON CLICKED

    public void PlayTurn()
    {
        place1.SetActive(true);
        place2.SetActive(false);
        Debug.Log("Player 2 card count: " + Player2Cards.Count);

        // Check if player 2 has any cards left to play
        if (Player2Cards.Count > 0)
        {
            Card playerCard = Player2Cards[0];
            cardNum.text = GetCardValue(playerCard).ToString();

            if (IsCardMatch)
            {
                // Get the previous card played by player 1
                Card previousCard = gameManager.playedCards.Last();
                previousCardNum.text = GetCardValue(previousCard).ToString();
                Debug.Log("Player 2 plays: " + playerCard.ToString());

                // Check if the player 2 card matches the previous card
                if (GetCardValue(playerCard) == GetCardValue(previousCard))
                {
                    Debug.Log("Cards match! Player 2 wins the round.");

                    // Add the played cards to player 2's hand and clear the played cards list
                    Player2Cards.AddRange(gameManager.playedCards);
                    gameManager.playedCards = new List<Card>();
                    IsCardMatch = false;
                    place1.SetActive(false);
                    place2.SetActive(true);
                }
                else
                {
                    Debug.Log("Cards do not match. Next round.");

                    // Add the player 2 card to the played cards list
                    gameManager.playedCards.Add(playerCard);
                }
            }
            else
            {
                // Add the player 2 card to the played cards list
                gameManager.playedCards.Add(playerCard);
                IsCardMatch = true;
            }

            // Remove the played card from player 2's hand
            Player2Cards.RemoveAt(0);
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
