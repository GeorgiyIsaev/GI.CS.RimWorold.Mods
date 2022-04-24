using LearningModBuildings.HediffMod.Bildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using Verse.AI;

namespace LearningModBuildings.HediffMod.Jobs
{
    public class JobDriver_CarryItemToBuilding : JobDriver
    {
        public Building_MyTestBilding building_MyTestBilding
            => TargetA.Thing as Building_MyTestBilding;
        // ссылка на строение (as если привдение к типу будит ошибочно не будит ошибки)

        public Thing Item => TargetB.Thing;

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            if(pawn.Reserve(TargetA, job)) // если удалось зарезервировать верстаак
            {
                return pawn.Reserve(TargetB, job); //резервируем предмет
            }
            return false;   
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            //Еще раз зарезрвируем предмет B
            yield return Toils_Reserve.Reserve(TargetIndex.B);
            //Подходим к этому предмету
            yield return Toils_Goto.Goto(TargetIndex.B, PathEndMode.ClosestTouch).FailOnDespawnedNullOrForbidden(TargetIndex.B);
            //взять предмет B
            yield return Toils_Haul.StartCarryThing(TargetIndex.B);
            //идем к строению А
            yield return Toils_Goto.Goto(TargetIndex.A, PathEndMode.ClosestTouch).FailOnDespawnedNullOrForbidden(TargetIndex.A); 
            //добавим прогресс бар для нашего верстака // Wait(1000) - колличество тиков
            yield return Toils_General.Wait(1000).WithProgressBarToilDelay(TargetIndex.A).FailOnDespawnedNullOrForbidden(TargetIndex.A); 

            //Создаем финальный толи
            Toil finish = new Toil();
            finish.initAction = delegate
            {
                //Если существует строение
                if (building_MyTestBilding != null)
                {
                    //Если существует предмет в строении  
                    if(building_MyTestBilding.ContainedThing != null)
                    {
                        //Сгененируем новый предмет
                        GenDrop.TryDropSpawn(building_MyTestBilding.ContainedThing, //какой предмет
                            building_MyTestBilding.Position, //куда
                            building_MyTestBilding.Map, //на какой карте
                            ThingPlaceMode.Direct, //на точку или Near если местоа не будит
                            out Thing result
                            );
                    }
                    else
                    {//Если предмета нет то выбрасить текущий, что бы избежать ошибок
                        pawn.carryTracker.TryDropCarriedThing(building_MyTestBilding.Position, ThingPlaceMode.Near, out Thing result);

                        building_MyTestBilding.LoadItem(Item); //обнуляет таймер при погрузке предмета
                        //И убераем предмет из строения
                        //building_MyTestBilding.ContainedThing = Item;
                        //Item.DeSpawn();
                    }
                    building_MyTestBilding.Complete = false; //работа завершена
                }
            };
            finish.defaultCompleteMode = ToilCompleteMode.Instant; //вернем работу в начальное состояние
            yield return finish; // выполним эту работу
        }
    }
}
