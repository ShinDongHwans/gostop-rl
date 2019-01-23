using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReport
{
    public int turn;
    public List<Card> myhand;
    public Status ground;
    public Status myown;
    public Status enemyown;
    public List<GameReport> nextturnrepots;
    public List<List<int>> statisticsaboutselection;

    public GameReport(int turn, List<Card> hand, List<Card> ground, List<Card> own, List<Card> oppositeown)
    {
        
        this.turn = turn;
        myhand = new List<Card>();
        CopyThem(hand, this.myhand);
        this.ground = new Status(ground);
        this.myown = new Status(own);
        this.enemyown = new Status(oppositeown);
        this.statisticsaboutselection = new List<List<int>>();
        // 첫 번째는 승리 수, 두 번째는 패배 수
        for (int i = 0; i < 10; i++)
            statisticsaboutselection.Add(new List<int> {0, 0});
    }


    void CopyThem(List<Card> input, List<Card> output)
    {
        foreach (Card card in input)
        {
            output.Add(card);
        }
    }
}
