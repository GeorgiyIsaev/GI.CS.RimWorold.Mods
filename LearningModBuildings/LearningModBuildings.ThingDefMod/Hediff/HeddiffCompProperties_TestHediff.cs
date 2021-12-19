using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

//HEalthDIFFerence (from normal state)
//Различие в состоянии здоровья

namespace LearningModBuildings.HediffMod
{
    //Класс свойств
    public class HeddiffCompProperties_TestHediff : HediffCompProperties
    {
        //Можно сразу же задать настройки 
        public float TestParam;
        //Теперь в XML в списке comps можно сразу определить его использвоание
        /*  <li Class="LearningModBuildings.HediffMod.HeddiffCompProperties_TestHediff">
            <TestParam>2</TestParam>
      </li>
         */

        public HeddiffCompProperties_TestHediff()
        {
            //В конструкторе необходимо указать тип нашего обрабочика
            // compClass = typeof();
            //Можно использовать готовое событие или создать свое
            compClass = typeof(HediffComp_TestHediff);

        }
    }
}
