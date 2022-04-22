using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace LearningModBuildings.HediffMod.Bildings
{
    /*Для быстрого получения доступа к свойству*/
    public class CompMyTestBuilding : ThingComp
    {
       /*Переводи свойства объекта в видимые значения*/
        public CompProperties_MyTestBilding Props =>
            (CompProperties_MyTestBilding)props;

        public override void CompTick()
        {
            base.CompTick();
            Log.Message("CompTick 123");
        }
    }
}
