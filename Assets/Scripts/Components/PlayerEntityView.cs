﻿
using UnityEngine;
using UselessFrame.NewRuntime.ECS;

namespace TestGame
{
    [EntityOf(typeof(PlayerEntity))]
    public class PlayerEntityView : EntityView
    {
        protected override void OnInit()
        {
            base.OnInit();
            GameObject.name = $"Player-{Entity.Id}";
        }
    }
}
