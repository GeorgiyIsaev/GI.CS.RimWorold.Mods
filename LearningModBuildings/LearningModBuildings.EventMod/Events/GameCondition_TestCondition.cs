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
            //Что будит проиходть каждый тик
           
            /*Обаботаем все карты через некоторое время*/
            if(Find.TickManager.TicksGame % 5000 == 0)
            {
                //2500 тиков == 1 час игрового времени
                DoEffectTick();
            }
        }
        private void DoEffectTick()
        {
            Log.Message($"Tick");
            foreach (var map in AffectedMaps) {
                foreach (var pawn in map.mapPawns.AllPawns)
                {
                    //Наносить пешке 5 урона
                    pawn.TakeDamage(new DamageInfo(DamageDefOf.Stab, 5f));

                }
            }
        }

        public override void End()
        {
            base.GameConditionTick();
            //Событие завершилось
            Log.Message($"END");
        }

    }
}
