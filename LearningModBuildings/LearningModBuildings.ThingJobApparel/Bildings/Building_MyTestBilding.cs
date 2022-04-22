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
        public Thing ContainedThing; //переменная в котороую мы будим складывать наш предмет


        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);

            compPowerTrader = GetComp<CompPowerTrader>();
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_References.Look(ref ContainedThing, "ContainedThing");
        }
     




    }
}
