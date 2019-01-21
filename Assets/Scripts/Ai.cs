using System.Collections.Generic;
using UnityEngine;

public class Ai
{
    public int mynumber;
    public List<List<GameReport>> hubos = new List<List<GameReport>>(21);
    public GameReport currenstatus;
    public List<Vector2Int> selectroute = new List<Vector2Int>(21);

    public Ai(int i)
    {
        mynumber = i;
        for(int k = 0; k < 21; k++)
        {
            hubos.Add(new List<GameReport>());
            selectroute.Add(new Vector2Int(-1, -1));
        }
    }

    public int MyTurn()
    {
        GameManager gameManager = GameManager.instance;
        int currentturn = gameManager.turn;
        if (mynumber == 1)
        {
            currenstatus = new GameReport(currentturn, gameManager.player1.hand, gameManager.ground, gameManager.player1.under, gameManager.player2.under);
        }
        else if (mynumber == 2)
        {
            currenstatus = new GameReport(currentturn, gameManager.player2.hand, gameManager.ground, gameManager.player2.under, gameManager.player1.under);
        }
        int output = -1;

        for (int index = 0 ;index < hubos[currentturn].Count; index++)
        {
            GameReport gameReport = hubos[currentturn][index];
            if (gameReport.myhand == currenstatus.myhand && gameReport.ground == currenstatus.ground && gameReport.myown == currenstatus.myown && gameReport.enemyown == currenstatus.enemyown)
            {
                float rate = 2f;
                for(int i = 0;i< gameReport.statisticsaboutselection.Count; i++)
                {
                    List<int> winlose = gameReport.statisticsaboutselection[i];
                    int numofwin = winlose[0];
                    int numoflose = winlose[1];
                    if (numofwin + numoflose != 0 && rate > (float)(numoflose/(numofwin + numoflose)))
                    {
                        output = i;
                        rate = (numoflose / (numofwin + numoflose));
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
}
