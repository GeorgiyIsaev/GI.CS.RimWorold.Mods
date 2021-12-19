using LearningModBuildings.HediffMod;
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

            //Создадим выдачу болезни при событии
            //Плохой способ: Найти предмет путем перебора всех предмотов по названию
            //HediffDef hediff = DefDatabase<HediffDef>.GetNamed("TestHediff");
            //Хороший способ: Создать отдельный класс HediffDefOfLocal с атрибутом  [DefOf] который будит хранит ссылку на объект
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
                    //Находится ли пешка не под потолком И является человеком
                    if(!pawn.Position.Roofed(pawn.Map) && pawn.RaceProps.Humanlike)
                    {
                        HealthUtility.AdjustSeverity(pawn, HediffDefOfLocal.TestHediff,
                            HediffDefOfLocal.TestHediff.initialSeverity); // Подхватит из XML начальное состояне болезни
                    }                
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
