using System.Collections.Generic;
using UnityEngine;

public class Ai
{
    public int mynumber;
    public List<List<GameReport>> hubos; 
    public GameReport currenstatus;
    public List<Vector2Int> selectroute = new List<Vector2Int>();

    public Ai(int i)
    {
        mynumber = i;
        if (hubos == null)
        {
            hubos = new List<List<GameReport>>();

            for (int k = 0; k < 21; k++)
            {
                hubos.Add(new List<GameReport>());
                selectroute.Add(new Vector2Int(-1, -1));
            }
        }
        else
        {
            for (int k = 0; k < 21; k++)
            {
                selectroute.Add(new Vector2Int(-1, -1));
            }
        }
    }

    public int MyTurn()
    {
        GameManager gameManager = GameManager.instance;
        int currentturn = gameManager.turn;
        if (mynumber == 1)
        {
            currenstatus = new GameReport(currentturn, GameManager.instance.player1.hand, GameManager.instance.ground, gameManager.player1.under, gameManager.player2.under);
        }
        else if (mynumber == 2)
        {
            currenstatus = new GameReport(currentturn, gameManager.player2.hand, gameManager.ground, gameManager.player2.under, gameManager.player1.under);
        }
        int output = -1;
        for (int index = 0 ;index < hubos[currentturn].Count; index++)
        {
            GameReport gameReport = hubos[currentturn][index];
            if (Same(gameReport.myhand, currenstatus.myhand) && gameReport.ground.Same(currenstatus.ground) && gameReport.myown.Same(currenstatus.myown) && gameReport.enemyown.Same(currenstatus.enemyown))
            {
                float rate = 1f;
                for(int i = 0;i< gameReport.statisticsaboutselection.Count && i<currenstatus.myhand.Count; i++)
                {
                    List<int> winlose = gameReport.statisticsaboutselection[i];
                    int numofwin = winlose[0];
                    int numoflose = winlose[1];
                    if (numofwin + numoflose != 0 && rate > (float)(numoflose/(numofwin + numoflose)))
                    {
                        output = i;
                        rate = (numoflose / (numofwin + numoflose));
                    }
                    else if(numofwin + numoflose == 0 && rate > 0.5f)
                    {
                        output = i;
                        rate = 0.5f;
                    }
                }
                selectroute[currentturn] = new Vector2Int(index, output);
            }
        }
        if(output == -1)
        {
            output = 0;
            hubos[currentturn].Add(currenstatus);
            selectroute[currentturn] = new Vector2Int(hubos[currentturn].Count-1, output);
        }
        return output;
    }

    public void GameFinish(int winorlose)
    {
        for(int i=0;i<selectroute.Count;i++)
        {
            Vector2Int choice = selectroute[i];
            if (choice != new Vector2Int(-1, -1))
            {
                hubos[i][choice.x].statisticsaboutselection[choice.y][winorlose]++;
            }
        }
    }

    public bool Same(List<Card> deck1, List<Card> deck2)
    {
        if (deck1.Count != deck2.Count) return false;
        else
        {
            for (int i = 0; i < deck1.Count; i++)
            {
                if (deck1[i].number != deck2[i].number || deck1[i].kind != deck2[i].kind) return false;
            }
            return true;
        }
    }
}
