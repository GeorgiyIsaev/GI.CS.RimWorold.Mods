using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace LearningModBuildings.EventMod.Events
{        
    public class IncidentWorker_TestIncident : IncidentWorker 
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            /*Событие будит происходить когда у игрока больше 5 пешек*/
            Map map =(Map)parms.target; // получаем информацию о карте
            if (map.mapPawns.ColonistsSpawnedCount < 5)
            {
                Log.Message($"Colonists Count = {map.mapPawns.ColonistsSpawnedCount}"); 
                //вывдит текст события
                return false;
            }
            return true;
        }
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            /*Запуск события через гейм мод*/
            Map map = (Map)parms.target; // получаем информацию о карте
            if (map.mapPawns.ColonistsSpawnedCount < 5)
            {
                Log.Message($"Colonists Count = {map.mapPawns.ColonistsSpawnedCount}");
                //вывдит текст события
                return false;
            }
            Log.Message($"EXECUTE");
            return true;
        }
    }
}
