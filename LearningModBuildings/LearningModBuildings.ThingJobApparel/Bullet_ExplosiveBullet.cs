using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace LearningModBuildings.HediffMod
{
    public class Bullet_ExplosiveBullet : Bullet
    {
        /*Переопределим логику попадания*/
        protected override void Impact(Thing hitThing)
        {
            base.Impact(hitThing); //оставляем базовую реализацию

            if(hitThing != null) //првоеряем что попали
            {
                //добавляем взрыв
                GenExplosion.DoExplosion(hitThing.Position, hitThing.Map, 
                    Rand.Range(1, 3),
                    DamageDefOf.Bomb, launcher);

            }
        }
    }
}
