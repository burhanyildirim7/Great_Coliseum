using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KasapKontrolNoktasiScript : MonoBehaviour
{
    [Header("Acilmasi İcin İhtiyac Olan Malzemelerin Sayi Texti")]
    public Text _ihtiyacText;
    [Header("Acilmasi İcin İhtiyac Olan Malzeme Sayisi")]
    public int _gerekliMalzemeSayisi;
    [Header("Malzeme Tamamlaninca Acilacak Kasap Objesi")]
    public GameObject _kasapObject;
    [Header("Cekilen Malzemenin Gidecegi Transform")]
    public Transform _malKabulNoktasi;
    [Header("Icinde Kod Olan Obje")]
    public GameObject _mekanikObjesi;
    [Header("Mal Kabul Objesi")]
    public GameObject _malKabulObjesi;
    [Header("Koyun Koruma Objesi")]
    public GameObject _koyunKorumaObjesi;


    private int _toplananMalzemeSayisi;
    private MeshRenderer _meshRenderer;
    private SirtCantasiScript _sirtCantasiScript;
    private Rigidbody _playerRigidbody;

    private float _timer;


    void Start()
    {

        _kasapObject.SetActive(false);
        _mekanikObjesi.SetActive(false);
        _malKabulObjesi.SetActive(false);
        _koyunKorumaObjesi.SetActive(false);

        _sirtCantasiScript = GameObject.FindGameObjectWithTag("Player").GetComponent<SirtCantasiScript>();
        _playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _ihtiyacText.text = _gerekliMalzemeSayisi.ToString();


        _timer = 0;
    }


    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ToplanmisSamanBalyasi")
        {
            //other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(other.gameObject);
        }
        else
        {

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (_playerRigidbody.velocity.x == 0 || _playerRigidbody.velocity.z == 0)
            {
                if (_gerekliMalzemeSayisi > 0)
                {
                    _timer += Time.deltaTime;

                    if (_timer > 0.1f)
                    {
                        if (_sirtCantasiScript._cantadakiSamanObjeleri.Count > 0)
                        {
                            _sirtCantasiScript.SamanCek(_malKabulNoktasi);
                            _gerekliMalzemeSayisi--;
                            _ihtiyacText.text = _gerekliMalzemeSayisi.ToString();
                            _timer = 0;
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

                    _kasapObject.SetActive(true);
                    _mekanikObjesi.SetActive(true);
                    _malKabulObjesi.SetActive(true);
                    _koyunKorumaObjesi.SetActive(true);
                    _meshRenderer.enabled = false;
                    _ihtiyacText.gameObject.SetActive(false);

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
}
