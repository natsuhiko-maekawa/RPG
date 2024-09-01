using System;
using BattleScene.Framework.InputSystem;
using BattleScene.Framework.View;
using BattleScene.InterfaceAdapter;
using BattleScene.InterfaceAdapter.Controller;
using BattleScene.UseCases.IController;
using UnityEngine;
using VContainer;

namespace BattleScene.Framework
{
    public class Game : MonoBehaviour
    {
        // private StateMachine _stateMachine;
        //
        // [Inject]
        // public void Construct(
        //     StateMachine stateMachine)
        // {
        //     _stateMachine = stateMachine;
        // }
        //
        // private void Start()
        // {
        //     _stateMachine.Start();
        //     var battleSceneInputSystem = GetComponent<BattleSceneInputSystem>();
        //     var gridView = GetComponentInChildren<GridView>();
        //     gridView.SetSelectAction(x => _stateMachine.SelectAction(x));
        // }
    }
}