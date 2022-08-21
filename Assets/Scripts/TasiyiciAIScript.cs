using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TasiyiciAIScript : MonoBehaviour
{
    [SerializeField] private GameObject _takipNoktasi;

    [SerializeField] private NavMeshAgent _agent;

    [SerializeField] private Animator _animator;

    private Transform _point;

    // Start is called before the first frame update
    private void Awake()
    {
        _point = _takipNoktasi.transform;
    }
    void Start()
    {
        _point = _takipNoktasi.transform;
    }

    private void Update()
    {
        if (GameController.instance.isContinue == true)
        {
            SetDestination(_point);

            if (_agent.velocity.sqrMagnitude == 0f)
            {
                _animator.SetBool("Walk", false);
            }
            else
            {
                _animator.SetBool("Walk", true);
            }
        }
    }



    private void SetDestination(Transform point)
    {
        _agent.SetDestination(point.position);
    }
}
