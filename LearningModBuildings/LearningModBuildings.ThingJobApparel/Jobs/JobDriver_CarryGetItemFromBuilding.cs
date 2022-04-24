using LearningModBuildings.HediffMod.Bildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using Verse.AI;

namespace LearningModBuildings.HediffMod.Jobs
{
    /*Работа по извлечению предмета из печки*/
    public class JobDriver_CarryGetItemFromBuilding : JobDriver
    {
        public Building_MyTestBilding building_MyTestBilding
            => TargetA.Thing as Building_MyTestBilding;
        // ссылка на строение (as если привдение к типу будит ошибочно не будит ошибки)


        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return pawn.Reserve(TargetA, job); //резервируем предмет
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            //Резервируем печку
            yield return Toils_Reserve.Reserve(TargetIndex.A); 
            //Подходим к этому предмету
            yield return Toils_Goto.Goto(TargetIndex.A, PathEndMode.ClosestTouch).FailOnDespawnedNullOrForbidden(TargetIndex.A);
            //ждем какое-то время
            yield return Toils_General.Wait(1000).WithProgressBarToilDelay(TargetIndex.A).FailOnDespawnedNullOrForbidden(TargetIndex.A); 

            //Создаем финальный толи
            Toil finish = new Toil();
            finish.initAction = delegate
            {
                //Если существует строение
                if (building_MyTestBilding != null)
                {
                    //првоеряем что печка не пустая
                    if(building_MyTestBilding.ContainedThing != null)
                    {
                        building_MyTestBilding.GetItem();
                    }
                }
            };
            finish.defaultCompleteMode = ToilCompleteMode.Instant; //вернем работу в начальное состояние
            yield return finish; // выполним эту работу
        }
    }
}
