using LearningModBuildings.HediffMod.Jobs;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using Verse.AI;

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

            //Если нет энергии ничего не делаем
            if (!HasPower)
                return;

            //Если еще не выполнено движем таймер
            if (ContainedThing != null && !Complete)
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

        /*Метод для обнуления таймера сдания после выполнения работы
         принимает предмет загруженный в печку*/
        public void LoadItem(Thing item)
        {
            ContainedThing = item; //загрузим новый итем в печку
            item.DeSpawn(); //деспавнем этот итем
            ticker = ticks; //переведем таймер в начало работы
        }

        /*Извлечение предмета из печки*/
        public void GetItem()
        {
            /*Создать предмет из печки рядом*/
            GenDrop.TryDropSpawn(ContainedThing, Position, Map, ThingPlaceMode.Near, out Thing result);

        }



        /*Добавим ключ*/
        public override string GetInspectString()
        {
            return $"Building_MyTestBilding_InspectString".Translate(ticker.TicksToDays().ToString("f2"));
        }

        /*Выдача работы*/
        public override IEnumerable<FloatMenuOption> GetFloatMenuOptions(Pawn selPawn)

        {
            /*Если предмет загружен делаем извлечение*/
            if (ContainedThing != null)
            {
                yield return new FloatMenuOption("Bulding_MyTestBulding_TakeJob_UnloadItem".Translate(), delegate
                {

                });
            }
            else //если нет создаем список что загрузить
            {
                yield return new FloatMenuOption("Bulding_MyTestBulding_TakeJob_LoadItem".Translate(), delegate
                {
                    List<FloatMenuOption> option = new List<FloatMenuOption>();
                    foreach (var thing in GetThing())
                    {
                    /*Создаем опции меню из каждого добавленого предмета*/
                        option.Add(new FloatMenuOption(thing.Label, delegate
                    {
                                Job job = new Job(JobDefOfLocal.CarryIdtemBuilding, this, thing);
                                job.count = thing.stackCount; //кол-во приносимых предметов (несем весь стак)
                            job.playerForced = true;
                                selPawn.jobs.TryOpportunisticJob(job);
                            }));


                        if (option.Count == 0) return;

                    /*Формируем меню предметов*/
                        Find.WindowStack.Add(new FloatMenu(option));
                    }
                });
            }
        }

        /*Получим список всех наркотивко на карте*/
        public IEnumerable<Thing> GetThing()
        {
            return Map.listerThings.ThingsInGroup(ThingRequestGroup.Drug);
        }



        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_References.Look(ref ContainedThing, "ContainedThing");
            Scribe_Values.Look(ref ticker, "ticker"); //сохраняем значение
        }
     




    }
}
