using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    class ActionList
    {
        SortLinkedList<Action> actions = new SortLinkedList<Action>();
        GameEngine gameEngine;

        public ActionList(GameEngine ge)
        {
            gameEngine = ge;
            ge.OnTick += Ge_OnTick;
        }

        private void Ge_OnTick(long Tick)
        {
            CheckActions();
        }

        public void AddAction(Action newAction)
        {
            newAction.UpdateTick(gameEngine.Ticks);
            actions.AddItem(newAction, InsertBySimpleMethod.Insert);
        }

        private void CheckActions()
        {
            if(actions.List.Count > 0)
            while (actions.List.Count > 0 && actions.List.First.Value.Tick <= gameEngine.Ticks)
            {
                actions.List.First.Value.Execute();
                actions.List.RemoveFirst();
            }
        }
    }
}
