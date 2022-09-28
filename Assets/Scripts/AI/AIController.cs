using Behaviours;
using ServiceLocator.Services;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private FighterLocator _fighterLocator;
    private Fighter _fighter;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _fighter = GetComponent<Fighter>();
        _fighterLocator = ServiceLocator.ServiceLocator.Current.Get<FighterLocator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAttackingFighter();

        if (_fighter.FighterToAttack == null)
        {
            _fighter.FighterToAttack = _fighterLocator.GetNearestFighter(_fighter);
        }
        else
        {
            Vector3 targetPosition = _fighter.FighterToAttack.transform.position;
            transform.LookAt(targetPosition);
            
            float distanceToTarget = Vector3.Distance(targetPosition, transform.position);
            if (distanceToTarget <= _fighter.AttackRadius)
            {
                _navMeshAgent.isStopped = true;
                _fighter.StartAttack();
            }
            else
            {
                Vector3 movement = targetPosition - transform.position;
                movement = movement.normalized * (_navMeshAgent.speed * Time.deltaTime);
                _navMeshAgent.Move(movement);
                
                _fighter.StopAttack();
            }
        }
    }

    private void CheckAttackingFighter()
    {
        if (_fighter.AttackingFighter != null)
        {
            _fighter.FighterToAttack = _fighter.AttackingFighter;
        }
    }
}