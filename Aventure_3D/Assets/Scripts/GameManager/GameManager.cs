using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Ebac.StateMachine;

public class GameManager : Singleton<GameManager>
{
   public enum GamesStates
    {
        INTRO,
        GAMEPLAY,
        PAUSE,
        WIN,
        LOSE
    }

    public StateMachine<GamesStates> stateMachine;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        stateMachine = new StateMachine<GamesStates>();
        stateMachine.Init();
        stateMachine.RegisterStates(GamesStates.INTRO, new GMStateIntro());
        stateMachine.RegisterStates(GamesStates.GAMEPLAY, new StateBase());
        stateMachine.RegisterStates(GamesStates.PAUSE, new StateBase());
        stateMachine.RegisterStates(GamesStates.WIN, new StateBase());
        stateMachine.RegisterStates(GamesStates.LOSE, new StateBase());

        stateMachine.SwitchState(GamesStates.INTRO);
    }
}
