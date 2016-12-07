using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    public class ActionList
    {
        /// <summary>
        /// Lista akcji posortowana rosnąca (względem czasu uruchomienia Akcji). Przyspiesza to sprawdzanie czy wykonać daną akcję bo tylko sprawdza akcję, która
        /// ma się wykonać najwcześniej czyli pierwszą na liście.
        /// </summary>
        SortLinkedList<Action> actions = new SortLinkedList<Action>();

        /// <summary>
        /// Instancja klasy Game Engine
        /// </summary>
        IGameEngine gameEngine;

        /// <summary>
        /// Ustawia instację game Engine dla listy
        /// </summary>
        /// <param name="ge">Instancja klasy game engine</param>
        public void SetGameEngine(IGameEngine ge)
        {
            gameEngine = ge;
            ge.OnTick += Ge_OnTick;
        }

        /// <summary>
        /// Metoda, która wykonuje się za każym tickiem zegara
        /// </summary>
        /// <param name="Tick">Ilość ticków zegara</param>
        private void Ge_OnTick(long Tick)
        {
            CheckActions();
        }

        /// <summary>
        /// Metoda dodająca akcję do listy. Przy dodawaniu zwiększa ilość ticków akcji aby zmienić ilość ticków z "Do wykonania" na "Kiedy wykonać"
        /// </summary>
        /// <param name="newAction"></param>
        public void AddAction(Action newAction)
        {
            newAction.UpdateTick(gameEngine.Ticks);
            actions.AddItem(newAction, InsertBySimpleMethod.Insert);
        }

        /// <summary>
        /// Sprawdza (przy każdym ticku zegara) czy już wykonać akcję
        /// </summary>
        private void CheckActions()
        {
            if(actions.List.Count > 0)
            {
                Action copyAction;
                while (actions.List.Count > 0 && actions.List.First.Value.Tick <= gameEngine.Ticks)
                {
                    actions.List.First.Value.Execute();
                    if(actions.List.First.Value.ExecuteTimes > 1)
                    {
                        copyAction = actions.List.First.Value;
                        copyAction.ExecuteTimes--;
                        actions.List.RemoveFirst();
                        AddAction(copyAction);
                    }
                    else
                        actions.List.RemoveFirst();
                }
            }           
        }
    }
}
