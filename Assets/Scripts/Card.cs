﻿using System.Collections.Generic;
using UnityEngine;

public class Card
{
    //1~12월
    public int number;
    // 0 - 피, 1 - 쌍피, 2 - 광, 3 - 고도리, 4 - 열끝, 5 - 홍단, 6 - 청단, 7 - 그냥 단
    public int kind;
    public Sprite sprite;
    public static Card[] deck = new Card[] { new Card(1, 2, Resources.Load<Sprite>("Sprites/11")),
                                      new Card(1, 5, Resources.Load<Sprite>("Sprites/12")),
                                      new Card(1, 0, Resources.Load<Sprite>("Sprites/13")),
                                      new Card(1, 0, Resources.Load<Sprite>("Sprites/14")),
                                      new Card(2, 3, Resources.Load<Sprite>("Sprites/21")),
                                      new Card(2, 5, Resources.Load<Sprite>("Sprites/22")),
                                      new Card(2, 0, Resources.Load<Sprite>("Sprites/23")),
                                      new Card(2, 0, Resources.Load<Sprite>("Sprites/24")),
                                      new Card(3, 2, Resources.Load<Sprite>("Sprites/31")),
                                      new Card(3, 5, Resources.Load<Sprite>("Sprites/32")),
                                      new Card(3, 0, Resources.Load<Sprite>("Sprites/33")),
                                      new Card(3, 0, Resources.Load<Sprite>("Sprites/34")),
                                      new Card(4, 3, Resources.Load<Sprite>("Sprites/41")),
                                      new Card(4, 7, Resources.Load<Sprite>("Sprites/42")),
                                      new Card(4, 0, Resources.Load<Sprite>("Sprites/43")),
                                      new Card(4, 0, Resources.Load<Sprite>("Sprites/44")),
                                      new Card(5, 4, Resources.Load<Sprite>("Sprites/51")),
                                      new Card(5, 7, Resources.Load<Sprite>("Sprites/52")),
                                      new Card(5, 0, Resources.Load<Sprite>("Sprites/53")),
                                      new Card(5, 0, Resources.Load<Sprite>("Sprites/54")),
                                      new Card(6, 4, Resources.Load<Sprite>("Sprites/61")),
                                      new Card(6, 6, Resources.Load<Sprite>("Sprites/62")),
                                      new Card(6, 0, Resources.Load<Sprite>("Sprites/63")),
                                      new Card(6, 0, Resources.Load<Sprite>("Sprites/64")),
                                      new Card(7, 4, Resources.Load<Sprite>("Sprites/71")),
                                      new Card(7, 7, Resources.Load<Sprite>("Sprites/72")),
                                      new Card(7, 0, Resources.Load<Sprite>("Sprites/73")),
                                      new Card(7, 0, Resources.Load<Sprite>("Sprites/74")),
                                      new Card(8, 2, Resources.Load<Sprite>("Sprites/81")),
                                      new Card(8, 3, Resources.Load<Sprite>("Sprites/82")),
                                      new Card(8, 0, Resources.Load<Sprite>("Sprites/83")),
                                      new Card(8, 0, Resources.Load<Sprite>("Sprites/84")),
                                      new Card(9, 1, Resources.Load<Sprite>("Sprites/91")),
                                      new Card(9, 6, Resources.Load<Sprite>("Sprites/92")),
                                      new Card(9, 0, Resources.Load<Sprite>("Sprites/93")),
                                      new Card(9, 0, Resources.Load<Sprite>("Sprites/94")),
                                      new Card(10, 4, Resources.Load<Sprite>("Sprites/101")),
                                      new Card(10, 6, Resources.Load<Sprite>("Sprites/102")),
                                      new Card(10, 0, Resources.Load<Sprite>("Sprites/103")),
                                      new Card(10, 0, Resources.Load<Sprite>("Sprites/104")),
                                      new Card(11, 2, Resources.Load<Sprite>("Sprites/111")),
                                      new Card(11, 4, Resources.Load<Sprite>("Sprites/112")),
                                      new Card(11, 7, Resources.Load<Sprite>("Sprites/113")),
                                      new Card(11, 1, Resources.Load<Sprite>("Sprites/114")),
                                      new Card(12, 2, Resources.Load<Sprite>("Sprites/121")),
                                      new Card(12, 1, Resources.Load<Sprite>("Sprites/122")),
                                      new Card(12, 0, Resources.Load<Sprite>("Sprites/123")),
                                      new Card(12, 0, Resources.Load<Sprite>("Sprites/124"))};

    public static Sprite backimage = Resources.Load<Sprite>("Sprites/backimage");
    public static Sprite backimage2 = backimage;
    public static Sprite defaultimage = Resources.Load<Sprite>("Sprites/defaultimage");
    public static Sprite defaultimage2 = backimage;

    public Card(int number, int kind, Sprite sprite)
    {
        this.number = number;
        this.kind = kind;
        this.sprite = sprite;
    }
}
