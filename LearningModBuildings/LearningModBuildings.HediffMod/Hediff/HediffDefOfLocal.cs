using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace LearningModBuildings.HediffMod
{
    //Создадим класс для быстрого обращения к предмету нашей болезни
    [DefOf]
    public static class HediffDefOfLocal
    {
        public static HediffDef TestHediff; //задем имя нашей переменой
        //В данную переменую при запуске игры будит записана информация о HediffDef имя которого в XML указанно как TestHediff
    }
}
