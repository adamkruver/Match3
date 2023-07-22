using System;
using Match3.Application.Builders;
using UnityEngine;

namespace Match3.Application
{
    public class Bootstrap : MonoBehaviour
    {
        private Game _game;
        
        private void Awake()
        {
            _game = new GameBuilder().Build();
        }

        private void Start()
        {
            _game.Run();
        }

        private void Update()
        {
            _game.Update();
        }

        private void OnDestroy()
        {
            _game.Finish();
        }
    }
}
