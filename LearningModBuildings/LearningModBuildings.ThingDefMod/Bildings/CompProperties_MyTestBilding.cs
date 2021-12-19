using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace LearningModBuildings.HediffMod.Bildings
{
    /*Класс с пользовательским компсом*/
    public class CompProperties_MyTestBilding : CompProperties
    {
        public float Damege;
        public CompProperties_MyTestBilding()
        {
            compClass = typeof(CompMyTestBuilding);
        }
    }
}
