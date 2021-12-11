using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace LearningModBuildings.HediffMod
{
    //Класс обработчика
    public class HediffComp_TestHediff : HediffComp
    {
        //Создаем переменную с типом нашего свойства 
        public HeddiffCompProperties_TestHediff Props =>(HeddiffCompProperties_TestHediff)props;

        //public HediffComp_TestHediff()
        //{
        //	//В конструкторе события можно изменять параметры с теченим времени
        //	//Props.TestParam 
        //	//Это не обязательно, для параметров измения лучше использвать данные из XML 
        //	//Так же можно переобпределить методы
        //	//CompPostMake() - (при выдачи болезни) 
        //	//CompPostTick() - при каждом тике и передает все comps		
        //	//CompExposeData() - сохранение сосотояни
        //	//CompPostPostAdd(DamageInfo? dinfo) хранит информацию об уроне
        //	//CompPostMerged(Hediff other) Соединение двух болезней в одну	
        //	// bool CompDisallowVisible() - видимость болезни	
        //}

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);
            //Интервал тика можно определить через условия
            // if(Find.TickManager.TicksGame % 2500) { } ///для всего
            // if(Pawn.IsHashIntervalTick()) { } //конкретно для пешки

            if (Pawn.IsHashIntervalTick(2500))
            {
                //Будим создавать взрыв через класс GenExplosion
                GenExplosion.DoExplosion(Pawn.Position, Pawn.Map, Props.TestParam, DamageDefOf.Bomb, null);
                DomageCount++;
            }
        }

        public override void CompExposeData()
        {
            // Метод в котором мы переопределим то, как будит
            // сохраняться течение болезни при сейве
            base.CompExposeData();

            //сохраним возможность извелекать батарею после сохранения
            //Scribe_Defs.Look(ref MyItems, "MyItems");

            //сохраним колличство нанесенного урона
            Scribe_Values.Look(ref DomageCount, "DomageCount");

        }
        //public ThingDef MyItems;
        //public override void Notify_PawnDied()
        //{
        //    base.Notify_PawnDied();
        //    MyItems = ThingDefOf.Battery;
        //    //При смерти пешки из ней можно вытащить батарею
        //}

        public int DomageCount; //сколько раз пешка получила урон

    }
}
