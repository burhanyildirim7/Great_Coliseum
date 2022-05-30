using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GladyatorAgentScript : MonoBehaviour
{
    private GameObject _kolezyumNoktasi;
    private GameObject _kilicNoktasi;

    [SerializeField] private GameObject _kilic;

    [SerializeField] private NavMeshAgent _agent;

    private Transform _point;

    // Start is called before the first frame update

    void Start()
    {
        _kilic.SetActive(false);

        //_agent.SetDestination(_ambarNoktasi.transform.position);

        _kolezyumNoktasi = GameObject.FindGameObjectWithTag("GladyatorNokta1");
        _kilicNoktasi = GameObject.FindGameObjectWithTag("GladyatorNokta2");

        if (SirtCantasiScript._kilicVar == true)
        {
            _point = _kilicNoktasi.transform;
        }
        else
        {
            _point = _kolezyumNoktasi.transform;
        }

    }

    private void Update()
    {
        if (GameController.instance.isContinue == true)
        {
            SetDestination(_point);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _kilicNoktasi)
        {
            _kilic.SetActive(true);

            _point = _kolezyumNoktasi.transform;
            //SetDestination(_tarlaNoktasi.transform);
            //_agent.SetDestination(_tarlaNoktasi.transform.position);
            //Debug.Log("Ambarda");
        }
        else if (other.gameObject.tag == "GladyatorKontrol")
        {
            if (GameObject.FindGameObjectWithTag("Demirci") != null)
            {
                DemirciSpawnScript _altinMadeniSpawnScript = GameObject.FindGameObjectWithTag("Demirci").GetComponent<DemirciSpawnScript>();

                if (_altinMadeniSpawnScript._ambarUrunSayisi > 0)
                {
                    _altinMadeniSpawnScript._ambarUrunSayisi--;

                    for (int i = 0; i < _altinMadeniSpawnScript._olusanUrunler.Count; i++)
                    {
                        if (_altinMadeniSpawnScript._olusanUrunler[i] != null)
                        {
                            Destroy(_altinMadeniSpawnScript._olusanUrunler[i].gameObject);
                        }
                        else
                        {

                        }
                    }


                }
                else
                {

                }
            }
            else
            {

            }
        }
        else
        {

        }
    }


    private void SetDestination(Transform point)
    {
        _agent.SetDestination(point.position);
    }
}
