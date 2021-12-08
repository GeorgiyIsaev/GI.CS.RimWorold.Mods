using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace LearningModBuildings.EventMod.Events
{
    public class GameCondition_TestCondition : GameCondition
    {
        public override void Init()
        {
            /*Инциализация осбытия**/
            base.Init();
            Log.Message($"{def.defName} execute");
        }
        public override void GameConditionTick()
        {
            base.GameConditionTick();
            //проверка когда событие было вызвано
            Log.Message($"1");
        }
        public override void End()
        {
            base.GameConditionTick();
            //Событие завершилось
            Log.Message($"END");
        }

    }
}
