using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiseumKabulNoktasiScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ToplanmisSamanBalyasi")
        {
            //other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "ToplanmisAltin")
        {
            Destroy(other.gameObject);
        }
        else
        {

        }
    }
}
