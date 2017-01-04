using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    public class ActionList : IActionList
    {
        /// <summary>
        /// Lista akcji posortowana rosnąca (względem czasu uruchomienia Akcji). Przyspiesza to sprawdzanie czy wykonać daną akcję bo tylko sprawdza akcję, która
        /// ma się wykonać najwcześniej czyli pierwszą na liście.
        /// </summary>
        SortLinkedList<IAction> actions = new SortLinkedList<IAction>();

        /// <summary>
        /// Metoda, która wykonuje się za każym tickiem zegara
        /// </summary>
        /// <param name="triggerValue">Ilość ticków zegara</param>
        public void Execute(long triggerValue)
        {
            CheckActions(triggerValue);
        }

        /// <summary>
        /// Metoda dodająca akcję do listy. Przy dodawaniu zwiększa ilość ticków akcji aby zmienić ilość ticków z "Do wykonania" na "Kiedy wykonać"
        /// </summary>
        /// <param name="newAction"></param>
        public void AddAction(IAction newAction)
        {
            newAction.Update();
            actions.AddItem(newAction, InsertBySimpleMethod.Insert);          
        }

        /// <summary>
        /// Sprawdza (przy każdym ticku zegara) czy już wykonać akcję
        /// </summary>
        private void CheckActions(long triggerValue)
        {
            if(actions.List.Count > 0)
            {
                IAction copyAction;
                while (actions.List.Count > 0 && actions.List.First.Value.TriggerValue <= triggerValue)
                {
                    actions.List.First.Value.Execute();
                    if (actions.List.First.Value.ExecuteTimes == 1)
                        actions.List.RemoveFirst();
                    else
                    { 
                        copyAction = actions.List.First.Value;
                        if(copyAction.ExecuteTimes > 0)
                            copyAction.ExecuteTimes--;
                        actions.List.RemoveFirst();
                        AddAction(copyAction);
                    }                    
                }
            }           
        }
    }
}
