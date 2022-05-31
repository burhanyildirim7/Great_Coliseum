using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MadenKontrolNoktasiScript : MonoBehaviour
{

    [Header("Acilmasi İcin İhtiyac Olan Malzemelerin Sayi Texti")]
    public Text _ihtiyacText;
    public Text _gerekliUrunSayisiText;
    [Header("Acilmasi İcin İhtiyac Olan Malzeme Sayisi")]
    public int _gerekliMalzemeSayisi;
    [Header("Malzeme Tamamlaninca Acilacak Maden Objesi")]
    public GameObject _madenObject;
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
    [SerializeField] private AltinMadeniSpawnScript _altinMadeniSpawnScript;


    private int _toplananMalzemeSayisi;
    private MeshRenderer _meshRenderer;
    private SirtCantasiScript _sirtCantasiScript;
    private Rigidbody _playerRigidbody;

    private float _timer;

    private bool _calisiyor;


    void Start()
    {

        if (PlayerPrefs.GetInt("AltinMadeniAktif") == 1)
        {
            _sirtCantasiScript = GameObject.FindGameObjectWithTag("Player").GetComponent<SirtCantasiScript>();
            _playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
            _meshRenderer = GetComponent<MeshRenderer>();

            _kapanacakCanvas.SetActive(false);
            _acilacakCanvas.SetActive(true);
            _madenObject.SetActive(true);
            _mekanikObjesi.SetActive(true);
            _malKabulObjesi.GetComponent<MeshRenderer>().enabled = true;
            _meshRenderer.enabled = false;
            _ihtiyacText.gameObject.SetActive(false);
            _gerekliUrunSayisiText.text = _altinMadeniSpawnScript._gerekliUrunSayisi.ToString();
            _calisiyor = true;

            _gerekliMalzemeSayisi = 0;

            _timer = 0;
        }
        else
        {
            _madenObject.SetActive(false);
            _mekanikObjesi.SetActive(false);
            _malKabulObjesi.GetComponent<MeshRenderer>().enabled = false;
            _kapanacakCanvas.SetActive(true);
            _acilacakCanvas.SetActive(false);
            _calisiyor = false;

            _sirtCantasiScript = GameObject.FindGameObjectWithTag("Player").GetComponent<SirtCantasiScript>();
            _playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _ihtiyacText.text = _gerekliMalzemeSayisi.ToString();

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
                    if (_calisiyor == false)
                    {
                        _kapanacakCanvas.SetActive(false);
                        _acilacakCanvas.SetActive(true);
                        _madenObject.SetActive(true);
                        _mekanikObjesi.SetActive(true);
                        _malKabulObjesi.GetComponent<MeshRenderer>().enabled = true;
                        _meshRenderer.enabled = false;
                        _ihtiyacText.gameObject.SetActive(false);
                        _gerekliUrunSayisiText.text = _altinMadeniSpawnScript._gerekliUrunSayisi.ToString();
                        _calisiyor = true;
                        PlayerPrefs.SetInt("AltinMadeniAktif", 1);
                    }
                    else
                    {
                        if (_altinMadeniSpawnScript._gerekliUrunSayisi < 10)
                        {
                            _timer += Time.deltaTime;

                            if (_timer > 0.1f)
                            {
                                if (_sirtCantasiScript._cantadakiSamanObjeleri.Count > 0)
                                {
                                    _sirtCantasiScript.SamanCek(_malKabulNoktasi);
                                    _altinMadeniSpawnScript._gerekliUrunSayisi++;
                                    _gerekliUrunSayisiText.text = _altinMadeniSpawnScript._gerekliUrunSayisi.ToString();
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
