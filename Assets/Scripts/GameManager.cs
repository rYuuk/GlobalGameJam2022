using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        Menu,
        Game,
        Finish
    }

    public State state;

    public void StartGame()
    {
        state = State.Game;
    }

    public void Finish()
    {
        state = State.Finish;
    }

}