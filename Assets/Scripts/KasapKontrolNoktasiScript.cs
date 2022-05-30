using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KasapKontrolNoktasiScript : MonoBehaviour
{
    [Header("Acilmasi İcin İhtiyac Olan Malzemelerin Sayi Texti")]
    public Text _ihtiyacText;
    public Text _gerekliUrunSayisiText;
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
    [Header("İcerisindeki Canvas Objeleri")]
    [SerializeField] private GameObject _kapanacakCanvas;
    [SerializeField] private GameObject _acilacakCanvas;
    [Header("İcerisindeki Spawn Script")]
    [SerializeField] private KasapSpawnScript _kasapSpawnScript;


    private int _toplananMalzemeSayisi;
    private MeshRenderer _meshRenderer;
    private SirtCantasiScript _sirtCantasiScript;
    private Rigidbody _playerRigidbody;

    private float _timer;

    private bool _calisiyor;




    void Start()
    {

        _kasapObject.SetActive(false);
        _mekanikObjesi.SetActive(false);
        _malKabulObjesi.SetActive(false);
        _koyunKorumaObjesi.SetActive(false);
        _kapanacakCanvas.SetActive(true);
        _acilacakCanvas.SetActive(false);
        Invoke("KasapVarAktif", 30);

        _sirtCantasiScript = GameObject.FindGameObjectWithTag("Player").GetComponent<SirtCantasiScript>();
        _playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _ihtiyacText.text = _gerekliMalzemeSayisi.ToString();

        _calisiyor = false;
        _timer = 0;
    }

    private void KasapVarAktif()
    {
        SirtCantasiScript._kasapVar = true;
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
                        _kasapObject.SetActive(true);
                        _mekanikObjesi.SetActive(true);
                        _malKabulObjesi.SetActive(true);
                        //_malKabulObjesi.GetComponent<MeshRenderer>().enabled = true;
                        _meshRenderer.enabled = false;
                        _ihtiyacText.gameObject.SetActive(false);
                        _gerekliUrunSayisiText.text = _kasapSpawnScript._gerekliUrunSayisi.ToString();
                        _calisiyor = true;

                    }
                    else
                    {
                        if (_kasapSpawnScript._gerekliUrunSayisi < 10)
                        {
                            _timer += Time.deltaTime;

                            if (_timer > 0.1f)
                            {
                                if (_sirtCantasiScript._cantadakiSamanObjeleri.Count > 0)
                                {
                                    _sirtCantasiScript.SamanCek(_malKabulNoktasi);
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
