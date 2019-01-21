using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status
{
    public List<List<int>> status;

    public Status(List<Card> carddeck)
    {
        this.status = new List<List<int>>();
        for (int i = 0;i < 12; i++)
        {
            this.status.Add(new List<int> { 0,0,0,0,0,0,0,0});
        }
        foreach(Card card in carddeck)
        {
            this.status[card.number-1][card.kind]++;
        }
    }
}
