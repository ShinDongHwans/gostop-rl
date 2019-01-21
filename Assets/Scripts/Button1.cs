using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Button1 : MonoBehaviour
{
    public Card card;

    public void OnClick()
    {
        if(GameManager.instance.gaming == true)
        {

            if (card != null)
            {
                GameManager.instance.turn++;
                Player player1 = GameManager.instance.player1;
                Player player2 = GameManager.instance.player2;
                if (GameManager.instance.turn % 2 == 1)
                {
                    List<Card> ground = GameManager.instance.ground;
                    Card comparecard = Compare(card, ground);
                    if (comparecard == null)
                    {
                        ground.Add(card);
                        DrawNewCardFuck(card);
                        player1.hand.Remove(card);
                    }
                    else
                    {
                        DrawNewCard(card, comparecard);
                    }
                    if (ground.Count == 0)
                    {
                        //TimerReset("쓸~~");
                        Steal(1);
                    }
                    int score = GameManager.instance.player1.CalculatePoint();
                    //GameManager.instance.ShowCurrentHand1();
                    //GameManager.instance.ShowPlayer1Ground();
                    if (score >= 7)
                    {
                        GameManager.instance.gaming = false;
                        //GameObject.Find("Win").GetComponent<Text>().text = "Player1 win!!";
                    }
                }
                else
                {
                    List<Card> ground = GameManager.instance.ground;
                    Card comparecard = Compare2(card, ground);
                    if (comparecard == null)
                    {
                        ground.Add(card);
                        DrawNewCardFuck2(card);
                        player2.hand.Remove(card);
                    }
                    else
                    {
                        DrawNewCard2(card, comparecard);
                    }
                    if (ground.Count == 0)
                    {
                        //TimerReset("쓸~~");
                        Steal(2);
                    }
                    int score = GameManager.instance.player2.CalculatePoint();
                    //GameManager.instance.ShowCurrentHand2();
                    //GameManager.instance.ShowPlayer2Ground();
                    if (score >= 7)
                    {
                        GameManager.instance.gaming = false;
                        //GameObject.Find("Win").GetComponent<Text>().text = "Player2 win!!";
                    }
                }
            }
        }
    }

    public void DrawNewCardFuck(Card card)
    {
        List<Card> shuffleddeck = GameManager.instance.shuffleddeck;
        List<Card> ground = GameManager.instance.ground;
        Player player1 = GameManager.instance.player1;
        if (shuffleddeck.Count == 0)
            Quit();
        Card newcard = shuffleddeck[shuffleddeck.Count - 1];
        shuffleddeck.RemoveAt(shuffleddeck.Count - 1);

        if(newcard.number == card.number)
        {
            //TimerReset("쪽~!");
            Steal(1);
            player1.under.Add(card);
            player1.under.Add(newcard);
            ground.Remove(card);
            ground.Remove(newcard);
        }
        else
        {
            Card hmm = Compare(newcard, ground);
            if (hmm == null)
            {
                ground.Add(newcard);
            }
        }
    }

    public void DrawNewCardFuck2(Card card)
    {
        List<Card> shuffleddeck = GameManager.instance.shuffleddeck;
        List<Card> ground = GameManager.instance.ground;
        Player player1 = GameManager.instance.player2;
        if (shuffleddeck.Count == 0)
            Quit();
        Card newcard = shuffleddeck[shuffleddeck.Count - 1];
        shuffleddeck.RemoveAt(shuffleddeck.Count - 1);

        if (newcard.number == card.number)
        {
            //TimerReset("쪽~!");
            Steal(2);
            player1.under.Add(card);
            player1.under.Add(newcard);
            ground.Remove(card);
            ground.Remove(newcard);
        }
        else
        {
            Card hmm = Compare2(newcard, ground);
            if (hmm == null)
            {
                ground.Add(newcard);
            }
        }
    }

    public void DrawNewCard(Card card, Card comparecard)
    {
        List<Card> shuffleddeck = GameManager.instance.shuffleddeck;
        List<Card> ground = GameManager.instance.ground;
        if(shuffleddeck.Count == 0)
            Quit();
        Card newcard = shuffleddeck[shuffleddeck.Count - 1];
        shuffleddeck.RemoveAt(shuffleddeck.Count-1);
        Card hmm = Compare(newcard, ground);
        if (newcard.number == card.number &&(hmm == null || hmm.number != card.number))
        {
            Player player1 = GameManager.instance.player1;
            //TimerReset("뻑!!!");
            player1.under.Remove(card);
            player1.under.Remove(comparecard);
            ground.Add(card);
            ground.Add(comparecard);
            ground.Add(newcard);
        }
        else if (newcard.number == card.number && hmm != null && hmm.number == card.number)
        {
            //TimerReset("따닥~!!");
            Steal(1);
        }
        else if(hmm == null)
        {
            ground.Add(newcard);
        }
    }

    public void DrawNewCard2(Card card, Card comparecard)
    {
        List<Card> shuffleddeck = GameManager.instance.shuffleddeck;
        List<Card> ground = GameManager.instance.ground;
        if (shuffleddeck.Count == 0)
            Quit();
        Card newcard = shuffleddeck[shuffleddeck.Count - 1];
        shuffleddeck.RemoveAt(shuffleddeck.Count - 1);
        Card hmm = Compare2(newcard, ground);
        if (newcard.number == card.number && (hmm == null || hmm.number != card.number))
        {
            Player player1 = GameManager.instance.player2;
            //TimerReset("뻑!!!");
            player1.under.Remove(card);
            player1.under.Remove(comparecard);
            ground.Add(card);
            ground.Add(comparecard);
            ground.Add(newcard);
        }
        else if(newcard.number == card.number && hmm != null && hmm.number == card.number)
        {
            //TimerReset("따닥~!!");
            Steal(2);
        }
        else if (hmm == null)
        {
            ground.Add(newcard);
        }
    }

    public Card Compare(Card mycard, List<Card> comparison)
    {
        Player player1 = GameManager.instance.player1;
        List<Card> output = new List<Card>();
        for (int i = comparison.Count - 1; i > -1; i--)
        {
            if (comparison[i].number == mycard.number)
               output.Add(comparison[i]);
        }
        if(output.Count == 3)
        {
            for(int i = 0; i<3; i++)
            {
                player1.under.Add(output[i]);
                comparison.Remove(output[i]);
            }
            player1.under.Add(mycard);
            player1.hand.Remove(mycard);
            return output[0];
        }
        else if(output.Count > 0)
        {
            player1.under.Add(mycard);
            player1.hand.Remove(mycard);
            player1.under.Add(output[0]);
            comparison.Remove(output[0]);
            return output[0];
        }
        return null;
    }

    public Card Compare2(Card mycard, List<Card> comparison)
    {
        Player player1 = GameManager.instance.player2;
        List<Card> output = new List<Card>();
        for (int i = comparison.Count - 1; i > -1; i--)
        {
            if (comparison[i].number == mycard.number)
                output.Add(comparison[i]);
        }
        if (output.Count == 3)
        {
            for (int i = 0; i < 3; i++)
            {
                player1.under.Add(output[i]);
                comparison.Remove(output[i]);
            }
            player1.under.Add(mycard);
            player1.hand.Remove(mycard);
            return output[0];
        }
        else if (output.Count > 0)
        {
            player1.under.Add(mycard);
            player1.hand.Remove(mycard);
            player1.under.Add(output[0]);
            comparison.Remove(output[0]);
            return output[0];
        }
        return null;
    }

    public void Quit()
    {
        GameManager.instance.StartNewGame();
    }

    public void Steal(int i)
    {
        Player player1 = GameManager.instance.player1;
        Player player2 = GameManager.instance.player2;
        if (i == 1)
        {
            if (player2.under.Count != 0)
            {
                foreach (Card card in player2.under)
                {
                    if (card.kind == 0 || card.kind == 1)
                    {
                        player2.under.Remove(card);
                        player1.under.Add(card);
                        return;
                    }
                }
            }
        }
        else if(i == 2)
        {
            if (player1.under.Count != 0)
            {
                foreach (Card card in player1.under)
                {
                    if (card.kind == 0 || card.kind == 1)
                    {
                        player1.under.Remove(card);
                        player2.under.Add(card);
                        return;
                    }
                }
            }
        }
    }

    public void TimerReset(string comment)
    {
        GameObject.Find("Win").GetComponent<Text>().text = comment;
        GameManager.instance.timer = 0f;
    }
}
