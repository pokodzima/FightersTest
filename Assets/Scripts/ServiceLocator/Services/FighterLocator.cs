using System.Collections.Generic;
using Behaviours;
using UnityEngine;

namespace ServiceLocator.Services
{
    public class FighterLocator : IGameService
    {
        private HashSet<Fighter> _fighters;

        public void Initialize()
        {
            _fighters = new HashSet<Fighter>();
        }

        public void Register(Fighter fighter)
        {
            _fighters.Add(fighter);
        }

        public void Unregister(Fighter fighter)
        {
            _fighters.Remove(fighter);
        }

        public Fighter GetNearestFighter(Fighter targetFighter)
        {
            Fighter nearestFighter = null;
            foreach (var fighter in _fighters)
            {
                if (fighter == targetFighter)
                {
                    continue;
                }

                if (nearestFighter == null)
                {
                    nearestFighter = fighter;
                }
                else
                {
                    float lastDistance = Vector3.Distance(targetFighter.transform.position,
                        nearestFighter.transform.position);
                    float currentDistance =
                        Vector3.Distance(targetFighter.transform.position, fighter.transform.position);

                    if (currentDistance < lastDistance)
                    {
                        nearestFighter = fighter;
                    }
                }
            }

            return nearestFighter;
        }
    }
}