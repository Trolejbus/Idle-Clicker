using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    class BuildingAction : Action
    {
        public long LevelToExecute { private set; get; }
        private Building buildingOwner;

        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        /// <param name="tickToExecute">Wartość, która mówi, że "za ile ticków ma się wykonać zdarzenie"</param>
        /// <param name="executeTimes">Wartość, która mówi "ile razy ma się wykonać zdarzenie"</param>
        /// <param name="frequencyTicks">Wartość, która mówi "co ile ma się wykonywać zdarzenie"</param>
        public BuildingAction(long ticksToExecute = 0, int executeTimes = 1, long frequencyTicks = 1) 
            :base(0,executeTimes,frequencyTicks)
        {
            LevelToExecute = ticksToExecute;
        }

        public void SetBuilding(Building building)
        {
            buildingOwner = building;
        }

        /// <summary>
        /// Przy dodawaniu zdarzenia do listy ta metoda zwiększa ilość ticków w tym zdarzeniu o ilość ticków w silniku gry
        /// </summary>
        /// <param name="gameEngineTicks">Tiki zegara gry</param>
        public override void Update()
        {
            TriggerValue = buildingOwner.Level + LevelToExecute;

            // gdy wstawię akcję w liście akcji już nie muszę czekać na rozpoczęcie wykonywania
            LevelToExecute = FrequencyValue;
            return;
        }
    }
}
