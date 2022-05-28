using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerScript : MonoBehaviour
{

    [SerializeField] private GameObject _tarlaNoktasi;
    [SerializeField] private GameObject _ambarNoktasi;

    [SerializeField] private GameObject _bugdayDemeti;

    [SerializeField] private Animator _farmerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _bugdayDemeti.SetActive(true);
        _farmerAnimator.SetBool("EliBos", false);
        _farmerAnimator.SetBool("EliDolu", true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _ambarNoktasi)
        {
            _bugdayDemeti.SetActive(false);
            _farmerAnimator.SetBool("EliDolu", false);
            _farmerAnimator.SetBool("EliBos", true);
        }
        else if (other.gameObject == _tarlaNoktasi)
        {
            _bugdayDemeti.SetActive(true);
            _farmerAnimator.SetBool("EliBos", false);
            _farmerAnimator.SetBool("EliDolu", true);
        }
        else
        {

        }
    }
}
