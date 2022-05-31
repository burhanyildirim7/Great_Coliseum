using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KislaKontrolNoktasiScript : MonoBehaviour
{
    [Header("Acilmasi İcin İhtiyac Olan Malzemelerin Sayi Texti")]
    public Text _ihtiyacSamanText;
    public Text _ihtiyacAltinText;
    public Text _gerekliUrunSayisiText;
    [Header("Acilmasi İcin İhtiyac Olan Malzeme Sayisi")]
    public int _gerekliSamanSayisi;
    public int _gerekliAltinSayisi;
    [Header("Malzeme Tamamlaninca Acilacak Kisla Objesi")]
    public GameObject _kislaObject;
    [Header("Cekilen Malzemenin Gidecegi Transform")]
    public Transform _malKabulNoktasi;
    [Header("Icinde Kod Olan Obje")]
    public GameObject _mekanikObjesi;
    [Header("Mal Kabul Objesi")]
    public GameObject _malKabulObjesi;
    [Header("İcerisindeki Canvas Objeleri")]
    [SerializeField] private GameObject _kapanacakCanvas;
    [SerializeField] private GameObject _acilacakCanvas;
    [Header("İcerisindeki Spawn Script")]
    [SerializeField] private KislaSpawnScript _kasapSpawnScript;


    private int _toplananMalzemeSayisi;
    private MeshRenderer _meshRenderer;
    private SirtCantasiScript _sirtCantasiScript;
    private Rigidbody _playerRigidbody;

    private float _timer;

    private bool _calisiyor;

    void Start()
    {
        if (PlayerPrefs.GetInt("KislaAktif") == 1)
        {
            _sirtCantasiScript = GameObject.FindGameObjectWithTag("Player").GetComponent<SirtCantasiScript>();
            _playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
            _meshRenderer = GetComponent<MeshRenderer>();

            _kislaObject.SetActive(true);
            _mekanikObjesi.SetActive(true);
            //_malKabulObjesi.GetComponent<MeshRenderer>().enabled = true;
            _meshRenderer.enabled = false;
            _ihtiyacSamanText.gameObject.SetActive(false);
            _ihtiyacAltinText.gameObject.SetActive(false);
            _kapanacakCanvas.SetActive(false);
            _acilacakCanvas.SetActive(true);
            _gerekliUrunSayisiText.text = _kasapSpawnScript._gerekliUrunSayisi.ToString();
            _calisiyor = true;

            _gerekliSamanSayisi = 0;
            _gerekliAltinSayisi = 0;

            _timer = 0;
        }
        else
        {
            _kislaObject.SetActive(false);
            _mekanikObjesi.SetActive(false);
            _malKabulObjesi.GetComponent<MeshRenderer>().enabled = false;
            _kapanacakCanvas.SetActive(true);
            _acilacakCanvas.SetActive(false);

            _sirtCantasiScript = GameObject.FindGameObjectWithTag("Player").GetComponent<SirtCantasiScript>();
            _playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _ihtiyacSamanText.text = _gerekliSamanSayisi.ToString();
            _ihtiyacAltinText.text = _gerekliAltinSayisi.ToString();

            _calisiyor = false;
            _timer = 0;
        }

    }


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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (_playerRigidbody.velocity.x == 0 || _playerRigidbody.velocity.z == 0)
            {
                if (_gerekliSamanSayisi > 0 || _gerekliAltinSayisi > 0)
                {
                    _timer += Time.deltaTime;

                    if (_timer > 0.1f)
                    {
                        if (_gerekliSamanSayisi > 0)
                        {
                            if (_sirtCantasiScript._cantadakiSamanObjeleri.Count > 0)
                            {
                                _sirtCantasiScript.SamanCek(_malKabulNoktasi);
                                _gerekliSamanSayisi--;
                                _ihtiyacSamanText.text = _gerekliSamanSayisi.ToString();
                                _timer = 0;
                            }
                            else
                            {

                            }
                        }
                        else
                        {

                        }

                        if (_gerekliAltinSayisi > 0)
                        {
                            if (_sirtCantasiScript._cantadakiAltinObjeleri.Count > 0)
                            {
                                _sirtCantasiScript.AltinCek(_malKabulNoktasi);
                                _gerekliAltinSayisi--;
                                _ihtiyacAltinText.text = _gerekliAltinSayisi.ToString();
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

                    }

                }
                else
                {
                    if (_calisiyor == false)
                    {
                        _kislaObject.SetActive(true);
                        _mekanikObjesi.SetActive(true);
                        //_malKabulObjesi.GetComponent<MeshRenderer>().enabled = true;
                        _meshRenderer.enabled = false;
                        _ihtiyacSamanText.gameObject.SetActive(false);
                        _ihtiyacAltinText.gameObject.SetActive(false);
                        _kapanacakCanvas.SetActive(false);
                        _acilacakCanvas.SetActive(true);
                        _gerekliUrunSayisiText.text = _kasapSpawnScript._gerekliUrunSayisi.ToString();
                        _calisiyor = true;
                        PlayerPrefs.SetInt("KislaAktif", 1);
                    }
                    else
                    {
                        if (_kasapSpawnScript._gerekliUrunSayisi < 10)
                        {
                            _timer += Time.deltaTime;

                            if (_timer > 0.1f)
                            {
                                if (_sirtCantasiScript._cantadakiAltinObjeleri.Count > 0)
                                {
                                    _sirtCantasiScript.AltinCek(_malKabulNoktasi);
                                    _kasapSpawnScript._gerekliUrunSayisi++;
                                    _gerekliUrunSayisiText.text = _kasapSpawnScript._gerekliUrunSayisi.ToString();
                                    //_ihtiyacText.text = _gerekliMalzemeSayisi.ToString();
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

                        }
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
