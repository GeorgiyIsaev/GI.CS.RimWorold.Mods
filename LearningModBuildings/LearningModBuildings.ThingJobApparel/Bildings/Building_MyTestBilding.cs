using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace LearningModBuildings.HediffMod.Bildings
{
    public class Building_MyTestBilding : Building
    {
        public CompPowerTrader compPowerTrader; //указывает на наше эл-ктричество

        //флаг для проверки есть ли электричество
        public bool HasPower
        {
            get
            {
                if(compPowerTrader != null && compPowerTrader.PowerOn)
                {
                   //проверка есть ли солнечная вспышка
                    return !Map.gameConditionManager.ConditionIsActive(GameConditionDefOf.SolarFlare);
                }
                return false;
            }
        } 
        
        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);

            compPowerTrader = GetComp<CompPowerTrader>();
        }
       
        public Thing ContainedThing; //переменная в котороую мы будим складывать наш предмет

        public bool Complete; //будит проверять выполнена ли работа

        private int ticks => 10000; // сколько  нужно времени что бы предмет улучшился
        private int ticker = 0; //таймер тикера //обязательно сохранить в ExposeData

        public override void Tick()
        {
            base.Tick();
            //Если еще не выполнено движем таймер
            if (!Complete)
            {
                ticker--; 
                if(ticker <= 0)
                {              
                    ThingComplate();
                }
            }
        }
        private void ThingComplate()
        {
            ticker = ticks; // возвращаем стартовое значение тикера

            Complete = true; //работа завершилась

            /*Проверим что контейнер есть*/        
            if(ContainedThing != null)
            {    /*Умножаем количество предметов контейнера на 2*/
                ContainedThing.stackCount *= 2;
            }

        }

        /*Добавим ключ*/
        public override string GetInspectString()
        {
            return $"Building_MyTestBilding_InspectString".Translate(ticker.TicksToDays().ToString("f2"));
        }



        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_References.Look(ref ContainedThing, "ContainedThing");
            Scribe_Values.Look(ref ticker, "ticker"); //сохраняем значение
        }
     




    }
}
