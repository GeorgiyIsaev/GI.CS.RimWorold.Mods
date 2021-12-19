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
        private CompMyTestBuilding compMyTestBuilding;
        
        
        /*Метод для генирации предмета*/
        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {            
            base.SpawnSetup(map, respawningAfterLoad);
            compMyTestBuilding = this.GetComp<CompMyTestBuilding>();
            Log.Message("SpawnSetup");
        }

        /*Изменение предмета при кажом тике*/
        public override void Tick()
        {
            base.Tick();
            Log.Message("TICK");
        }

        /*Выпадающий список из предмета*/
        public override IEnumerable<FloatMenuOption> GetFloatMenuOptions(Pawn selPawn)
        {
            /*При выборе данной опции наносит урон пешке*/
            yield return new FloatMenuOption("Option 1", delegate
            {
                // selPawn.TakeDamage(new DamageInfo(DamageDefOf.Bite, 20));
                /*Зазадим урон уже с помощью устанавливаемого компса*/
                selPawn.TakeDamage(new DamageInfo(DamageDefOf.Bite, 
                    compMyTestBuilding.Props.Damege));

            });

            /*Возврат втрого менб*/
            yield return GetOption();
        }
        private FloatMenuOption GetOption()
        {
            return new FloatMenuOption("Option2",delegate 
            {
                Log.Message("Option2"); ;
            });
            
        }

        /*Меню придмета при его выборе (Гизма)*/
        /*Будит переметывать время на целый сезон*/
        public override IEnumerable<Gizmo> GetGizmos()
        {
            yield return new Command_Action()
            {
                defaultLabel = "Default Labael",
                defaultDesc = "Default desc 123",
                icon = def.uiIcon,
                action = delegate
                {
                    Find.TickManager.DebugSetTicksGame
                    (
                        Find.TickManager.TicksGame + 12000000
                    );
                }
            };
        }
    }
}
