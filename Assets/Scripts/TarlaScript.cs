using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TarlaScript : MonoBehaviour
{
    [Header("Sadece Ilk Tarlaysa Tiklenecek")]
    public bool _ilkTarlaMi;
    [Header("Sadece Ilk Tarlaysa Ambar Objesi Konacak")]
    public GameObject _ambarObject;
    [Header("Acilmasi İcin İhtiyac Olan Malzemelerin Sayi Texti")]
    public Text _ihtiyacText;
    [Header("Acilmasi İcin İhtiyac Olan Malzeme Sayisi")]
    public int _gerekliMalzemeSayisi;
    [Header("Malzeme Tamamlaninca Acilacak Tarla Objesi")]
    public GameObject _tarlaObject;
    [Header("Cekilen Malzemenin Gidecegi Transform")]
    public Transform _malKabulNoktasi;
    [Header("Tamamlandiktan Sonra Acilacak Tarla Varsa Tiklenecek")]
    public bool _sonrasindaTarlaVarMi;
    [Header("Sonrasinda Tarla Varsa Tarla Objesi")]
    public GameObject _sonrakiTarlaObject;



    private int _toplananMalzemeSayisi;
    private MeshRenderer _meshRenderer;
    private SirtCantasiScript _sirtCantasiScript;
    private Rigidbody _playerRigidbody;

    private float _timer;


    void Start()
    {


        _tarlaObject.SetActive(false);

        _sirtCantasiScript = GameObject.FindGameObjectWithTag("Player").GetComponent<SirtCantasiScript>();
        _playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _ihtiyacText.text = _gerekliMalzemeSayisi.ToString();

        if (_sonrasindaTarlaVarMi)
        {
            _sonrakiTarlaObject.SetActive(false);
        }
        else
        {

        }

        if (_ilkTarlaMi)
        {
            _ambarObject.SetActive(false);
        }
        else
        {

        }
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
                    if (_ilkTarlaMi)
                    {
                        _ambarObject.SetActive(true);
                        Debug.Log("Ambar Acildi");

                    }
                    else
                    {
                        Debug.Log("Ambar Acilamadi");
                    }

                    _tarlaObject.SetActive(true);
                    _meshRenderer.enabled = false;
                    _ihtiyacText.gameObject.SetActive(false);

                    if (_sonrasindaTarlaVarMi)
                    {
                        _sonrakiTarlaObject.SetActive(true);
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
}
