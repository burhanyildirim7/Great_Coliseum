using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arastirmaBinasiBaslangicScripti : MonoBehaviour
{
    [SerializeField] GameObject duranAdam, arastirmaMasasi,tezgahCanvasi;
    // Start is called before the first frame update
    void Start()
    {
        arastirmaMasasi.SetActive(false);
        duranAdam.SetActive(false);
        tezgahCanvasi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward*Time.deltaTime*2.5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="ArastirmaBinasi")
        {
            arastirmaMasasi.SetActive(true);
            duranAdam.SetActive(true);
            tezgahCanvasi.SetActive(true);
            transform.gameObject.SetActive(false);
        }
    }
}
