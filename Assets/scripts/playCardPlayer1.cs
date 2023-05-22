using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playCardPlayer1 : MonoBehaviour
{
    gameManager gm;
    List<Card> player1Cards;
    List<Card> playedCards;
    public TextMeshProUGUI playerCardNum;
    public TextMeshProUGUI previousCardNum;
    public GameObject place1;
    public GameObject place2;
    bool yes = false;
    private void Start()
    {
        Invoke("hello", 0.5f);
        place1.SetActive(true);
        place2.SetActive(false);
    }
    void hello()
    {
        gm = FindAnyObjectByType<gameManager>();
        player1Cards = gm.player1Hand;
        //gm.playedCards.Add(player1Cards[0]);
        //player1Cards.RemoveAt(0);
    }
    public void PlayTurn()
    {
        place1.SetActive(false);
        place2.SetActive(true);
        Debug.Log("player 1 count cards"+ player1Cards.Count);
        // Simulating player turns
        if (player1Cards.Count > 0)
        {
            Card playerCard = player1Cards[0];
            playerCardNum.text = playerCard.Value.ToString();
            if (yes)
            {
                Card previousCard = gm.playedCards[gm.playedCards.Count - 1];
                previousCardNum.text = previousCard.Value.ToString();
                Debug.Log("Player 1 plays: " + playerCard.ToString());
                //Debug.Log("Player 2 plays: " + player2Card.ToString());

                if (playerCard.Value == previousCard.Value)
                {
                    Debug.Log("Cards match! Player 1 wins the round.");

                    player1Cards.AddRange(gm.playedCards);
                    gm.playedCards = new List<Card>();
                    yes = false;
                    place1.SetActive(true);
                    place2.SetActive(false);
                }
                else
                {
                    Debug.Log("Cards do not match. Next round.");
                    gm.playedCards.Add(playerCard);
                    //playedCards.Add(player2Card);
                }
            }
            else
            {
                gm.playedCards.Add(playerCard);
                yes = true;
            }
            player1Cards.RemoveAt(0);
            //gm.player2Hand.RemoveAt(0);
        }
        else
        {
            Debug.Log("Game over.");
        }
    }
}
