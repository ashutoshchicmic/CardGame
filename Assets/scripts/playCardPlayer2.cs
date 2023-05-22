using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class playCardPlayer2 : MonoBehaviour
{
    gameManager gm;
    List<Card> player2Cards;
    List<Card> playedCards;
    public TextMeshProUGUI cardNum;
    public TextMeshProUGUI previousCardNum;
    public GameObject place1;
    public GameObject place2;
    bool yes = true;
    private void Start()
    {
        Invoke("hello", 0.5f);
    }
    void hello()
    {
        gm = FindAnyObjectByType<gameManager>();
        player2Cards = gm.player2Hand;
    }
    public void PlayTurn()
    {
        place1.SetActive(true);
        place2.SetActive(false);
        Debug.Log("player 2 count cards" + player2Cards.Count);
        // Simulating player turns
        if (player2Cards.Count > 0)
        {
            Card playerCard = player2Cards[0];
            cardNum.text = playerCard.Value.ToString();
            if (yes)
            {
                Card previousCard = gm.playedCards[gm.playedCards.Count - 1];
                previousCardNum.text = previousCard.Value.ToString();
                Debug.Log("Player 2 plays: " + playerCard.ToString());
                //Debug.Log("Player 2 plays: " + player2Card.ToString());

                if (playerCard.Value == previousCard.Value)
                {
                    Debug.Log("Cards match! Player 2 wins the round.");

                    player2Cards.AddRange(gm.playedCards);
                    gm.playedCards = new List<Card>();
                    yes = false;
                    place1.SetActive(false);
                    place2.SetActive(true);
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
            player2Cards.RemoveAt(0);
            //gm.player2Hand.RemoveAt(0);
        }
        else
        {
            Debug.Log("Game over.");
        }
    }
}
