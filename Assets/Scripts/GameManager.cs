using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player player1 = new Player(1);
    public Player player2 = new Player(2);
    public List<Card> carddeck;
    public List<Card> ground;
    public List<Card> shuffleddeck;
    public List<int> countnumber;
    public int turn;
    public float timer;
    int waitingTime;
    public bool gaming;
    public int gamenumber = 0;

    public void Initializing()
    {
        carddeck = new List<Card>();
        ground = new List<Card>();
        shuffleddeck = new List<Card>();
        countnumber = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        turn = 0;
        gaming = true;
    }

    public void MakeDeck()
    {
        foreach(Card card in Card.deck)
        {
            carddeck.Add(card);
        }
    }

    public void Shuffle()
    {
        while(carddeck.Count != 0)
        {
            int x = Random.Range(0, carddeck.Count);
            shuffleddeck.Add(carddeck[x]);
            carddeck.RemoveAt(x);
        }
    }

    public bool SpreadtheCard(int num)
    {
        for(int i = 0; i<num; i++)
        {
            if(shuffleddeck.Count == 0)
            {
                return false;
            }
            ground.Add(shuffleddeck[shuffleddeck.Count - 1]);
            shuffleddeck.RemoveAt(shuffleddeck.Count-1);
        }
        return true;
    }

    public void InitialCard(int num)
    {
        for (int i = 0; i < num; i++)
        {
            player1.hand.Add(shuffleddeck[0]);
            shuffleddeck.RemoveAt(0);
        }
        for (int i = 0; i < num; i++)
        {
            player2.hand.Add(shuffleddeck[0]);
            shuffleddeck.RemoveAt(0);
        }
    }

    public void ShowCurrentHand1()
    {
        int i = 0;
        GameObject.Find("Score1").GetComponent<Text>().text = "점수1: " + player1.CalculatePoint();
        GameObject.Find("Turn").GetComponent<Text>().text = "1st";
        foreach (Card card in player1.hand)
        {
            GameObject suittoshow = GameObject.Find("Card1" + i);
            suittoshow.GetComponent<Image>().sprite = player1.hand[i].sprite;
            suittoshow.GetComponent<Button1>().card = player1.hand[i];
            i++;
        }
        while (i != 10)
        {
            GameObject suittoshow = GameObject.Find("Card1" + i);
            suittoshow.GetComponent<Image>().sprite = Card.backimage;
            suittoshow.GetComponent<Button1>().card = null;
            i++;
        }
    }

    public void ShowCurrentHand2()
    {
        int i = 0;
        GameObject.Find("Score2").GetComponent<Text>().text = "점수2: " + player2.CalculatePoint();
        GameObject.Find("Turn").GetComponent<Text>().text = "2nd";
        foreach (Card card in player2.hand)
        {
            GameObject suittoshow = GameObject.Find("Card1" + i);
            suittoshow.GetComponent<Image>().sprite = player2.hand[i].sprite;
            suittoshow.GetComponent<Button1>().card = player2.hand[i];
            i++;
        }
        while (i != 10)
        {
            GameObject suittoshow = GameObject.Find("Card1" + i);
            suittoshow.GetComponent<Image>().sprite = Card.backimage;
            suittoshow.GetComponent<Button1>().card = null;
            i++;
        }
    }

    public void Sort()
    {
        List<Card> change1 = new List<Card>();
        List<Card> hand1 = player1.hand;
        for (int k = 0; k < 10; k++)
        {
            Card hubo = hand1[0];
            for (int i = 0; i < hand1.Count; i++)
            {
                if(i != 0)
                {
                    Card card = hand1[i];
                    if (hubo.number > card.number)
                        hubo = card;
                    else if (hubo.number == card.number && hubo.kind > card.kind)
                        hubo = card;
                }
            }
            change1.Add(hubo);
            hand1.Remove(hubo);
        }
        player1.hand = change1;

        List<Card> change2 = new List<Card>();
        List<Card> hand2 = player2.hand;
        for (int k = 0; k < 10; k++)
        {
            Card hubo = hand2[0];
            for (int i = 0; i < hand2.Count; i++)
            {
                if (i != 0)
                {
                    Card card = hand2[i];
                    if (hubo.number > card.number)
                        hubo = card;
                    else if (hubo.number == card.number && hubo.kind > card.kind)
                        hubo = card;
                }
            }
            change2.Add(hubo);
            hand2.Remove(hubo);
        }
        player2.hand = change2;
    }

    public void ShowCurrentGround()
    {
        for(int i = 0; i< 12; i++)
        {
            countnumber[i] = 0;
        }
        for(int i = 1; i< 13; i++)
        {
            GameObject.Find("Image" + i + 0).GetComponent<Image>().sprite = Card.defaultimage;
            GameObject.Find("Image" + i + 1).GetComponent<Image>().sprite = Card.defaultimage;
            GameObject.Find("Image" + i + 2).GetComponent<Image>().sprite = Card.defaultimage;
        }
        for(int i = ground.Count-1; i>-1; i--)
        {
            Card currentcard = ground[i];
            int number = currentcard.number;
            GameObject.Find("Image" + number+(2 - countnumber[number-1])).GetComponent<Image>().sprite = currentcard.sprite;
            countnumber[number-1]++;
        }
    }

    public void ShowPlayer1Ground()
    {
        List<int> countnumber1 = new List<int> { 0, 0, 0, 0, 0, 0};

        foreach(Card card in player1.under)
        {
            int number = card.kind;
            if(number == 0 || number == 1)
            {
                if(countnumber1[0] != 10)
                {
                    GameObject.Find("Eat10" + countnumber1[0]).GetComponent<Image>().sprite = card.sprite;
                    countnumber1[0]++;
                }
                else
                {
                    GameObject.Find("Eat11" + countnumber1[1]).GetComponent<Image>().sprite = card.sprite;
                    countnumber1[1]++;
                }
            }
            else if (number < 5)
            {
                GameObject.Find("Eat1" + number + countnumber1[number]).GetComponent<Image>().sprite = card.sprite;
                countnumber1[number]++;
            }
            else
            {
                GameObject.Find("Eat15" + countnumber1[5]).GetComponent<Image>().sprite = card.sprite;
                countnumber1[5]++;
            }
        }
    }

    public void ShowPlayer2Ground()
    {
        List<int> countnumber2 = new List<int> { 0, 0, 0, 0, 0, 0 };

        foreach (Card card in player2.under)
        {
            int number = card.kind;
            if (number == 0 || number == 1)
            {
                if (countnumber2[0] != 10)
                {
                    GameObject.Find("Eat10" + countnumber2[0] + " (1)").GetComponent<Image>().sprite = card.sprite;
                    countnumber2[0]++;
                }
                else
                {
                    GameObject.Find("Eat11" + countnumber2[1] + " (1)").GetComponent<Image>().sprite = card.sprite;
                    countnumber2[1]++;
                }
            }
            else if (number < 5)
            {
                GameObject.Find("Eat1" + number + countnumber2[number] + " (1)").GetComponent<Image>().sprite = card.sprite;
                countnumber2[number]++;
            }
            else
            {
                GameObject.Find("Eat15" + countnumber2[5] + " (1)").GetComponent<Image>().sprite = card.sprite;
                countnumber2[5]++;
            }
        }
    }

    public void StartAI() {
        Ai ai1 = player1.ai;
        Ai ai2 = player2.ai;
        player1.point = 0;
        player2.point = 0;
        while(turn < 20 && player1.point <7 && player2.point < 7)
        {
            if(turn%2 == 0)
            {
                int card = ai1.MyTurn();
                SelectCard(player1.hand[card]);
            }

            else
            {
                int card = ai2.MyTurn();
                SelectCard(player2.hand[card]);
            }
        }
        if (player1.point >= 7)
        {
            ai1.GameFinish(0);
            ai2.GameFinish(1);
        }
        else if(player2.point >= 7)
        {
            ai2.GameFinish(0);
            ai1.GameFinish(1);
        }
        else
        {
            ai1.GameFinish(0);
            ai1.GameFinish(1);
            ai2.GameFinish(0);
            ai2.GameFinish(1);
        }
        /*
        GameObject.Find("Score1").GetComponent<Text>().text = "점수1: " + player1.CalculatePoint();
        GameObject.Find("Score2").GetComponent<Text>().text = "점수2: " + player2.CalculatePoint();
        ShowPlayer1Ground();
        ShowPlayer2Ground();
        */
        Debug.Log("player1 score is " + player1.point);
        Debug.Log("player2 score is " + player2.point);
        gaming = false;
        if(gamenumber < 1000)
        {
            StartNewGame();
        }
    }
    
    public void StartNewGame()
    {
        print("gamenumber is " + gamenumber);
        gamenumber++;
        Initializing();
        player1.Initializing();
        player2.Initializing();
        MakeDeck();
        Shuffle();
        InitialCard(10);
        SpreadtheCard(8);
        Sort();
        StartAI();
    }

    public void SelectCard(Card card)
    {
        if (gaming == true)
        {

            if (card != null)
            {
                turn++;
                if (turn % 2 == 1)
                {
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
                    player1.point = player1.CalculatePoint();
                }
                else
                {
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
                    player2.point = player2.CalculatePoint();
                }
            }
        }
    }


    public void DrawNewCardFuck(Card card)
    {
        if (shuffleddeck.Count == 0)
            Quit();
        Card newcard = shuffleddeck[shuffleddeck.Count - 1];
        shuffleddeck.RemoveAt(shuffleddeck.Count - 1);

        if (newcard.number == card.number)
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
        if (shuffleddeck.Count == 0)
            Quit();
        Card newcard = shuffleddeck[shuffleddeck.Count - 1];
        shuffleddeck.RemoveAt(shuffleddeck.Count - 1);

        if (newcard.number == card.number)
        {
            //TimerReset("쪽~!");
            Steal(2);
            player2.under.Add(card);
            player2.under.Add(newcard);
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
        if (shuffleddeck.Count == 0)
            Quit();
        Card newcard = shuffleddeck[shuffleddeck.Count - 1];
        shuffleddeck.RemoveAt(shuffleddeck.Count - 1);
        Card hmm = Compare(newcard, ground);
        if (newcard.number == card.number && (hmm == null || hmm.number != card.number))
        {
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
        else if (hmm == null)
        {
            ground.Add(newcard);
        }
    }

    public void DrawNewCard2(Card card, Card comparecard)
    {
        if (shuffleddeck.Count == 0)
            Quit();
        Card newcard = shuffleddeck[shuffleddeck.Count - 1];
        shuffleddeck.RemoveAt(shuffleddeck.Count - 1);
        Card hmm = Compare2(newcard, ground);
        if (newcard.number == card.number && (hmm == null || hmm.number != card.number))
        {
            //TimerReset("뻑!!!");
            player2.under.Remove(card);
            player2.under.Remove(comparecard);
            ground.Add(card);
            ground.Add(comparecard);
            ground.Add(newcard);
        }
        else if (newcard.number == card.number && hmm != null && hmm.number == card.number)
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

    public Card Compare2(Card mycard, List<Card> comparison)
    {
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
                player2.under.Add(output[i]);
                comparison.Remove(output[i]);
            }
            player2.under.Add(mycard);
            player2.hand.Remove(mycard);
            return output[0];
        }
        else if (output.Count > 0)
        {
            player2.under.Add(mycard);
            player2.hand.Remove(mycard);
            player2.under.Add(output[0]);
            comparison.Remove(output[0]);
            return output[0];
        }
        return null;
    }

    public void Quit()
    {
        StartNewGame();
    }

    public void Steal(int i)
    {
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
        else if (i == 2)
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


    /* ---------------Methods-----------------*/
    public GameManager() : base()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    void Start()
    {
        timer = 0f;
        waitingTime = 3;
        StartNewGame();
        /*
        ShowCurrentHand1();
        ShowCurrentGround();
        */
    }

    
    // Update is called once per frame
    void Update()
    {
        /*
        if (gaming == true)
        {
            timer += Time.deltaTime;
            ShowCurrentGround();

            GameObject.Find("Win").GetComponent<Text>().text = "";
            if (timer > waitingTime)
            {
                timer = 0;
            }
            if (turn % 2 == 0)
                ShowCurrentHand1();
            else
                ShowCurrentHand2();
        }
        */
    }
}
