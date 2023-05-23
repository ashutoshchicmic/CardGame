#region IMPORT LIBRARIES
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
#endregion

public class PlayCardPlayer2 : MonoBehaviour
{
    #region FIELDS
    GameManager gameManager;
    private List<Card> Player2Cards;
    private bool CardPresent = true;
    public TextMeshProUGUI cardNum;
    public TextMeshProUGUI previousCardNum;
    public GameObject place1;
    public GameObject place2;
    public Image cardSpriteImage;
    public Image previousCardSpriteImage;
    public Sprite noCardSprite;
    public GameObject gameOverText;
    #endregion


    #region MONOBEHAVIOUR METHODS
    private void Start()
    {
        // Wait for a short delay and then initialize the game manager
        Invoke(nameof(InitializeGameManager), 0.05f);
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
            DisplayCardSprite(playerCard,cardSpriteImage);
            
            // Add the player 2 card to the played cards list
            gameManager.playedCards.Add(playerCard);

            if (CardPresent)
            {
                // Get the previous card played by player 1
                Card previousCard = gameManager.playedCards.ElementAt(gameManager.playedCards.Count - 2);
                previousCardNum.text = GetCardValue(previousCard).ToString();
                DisplayCardSprite(previousCard, previousCardSpriteImage);
                Debug.Log("Player 2 plays: " + playerCard.ToString());
                
                // Check if the player 2 card matches the previous card
                if (GetCardValue(playerCard) == GetCardValue(previousCard))
                {
                    Debug.Log("Cards match! Player 2 wins the round.");

                    // Add the played cards to player 2's hand and clear the played cards list
                    Player2Cards.AddRange(gameManager.playedCards);
                    gameManager.playedCards = new List<Card>();
                    CardPresent = false;
                    place1.SetActive(false);
                    place2.SetActive(true);
                }
                else
                {
                    Debug.Log("Cards do not match. Next round.");
                }
            }
            else
            {
                previousCardSpriteImage.sprite = noCardSprite;
                previousCardNum.text = "0";
                CardPresent = true;
            }

            // Remove the played card from player 2's hand
            Player2Cards.RemoveAt(0);
        }
        else
        {
            Debug.Log("Game over.");
            place1.SetActive(false);
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
