using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TarlaScript : MonoBehaviour
{

    [Header("Levelde Acilan KAcinci Tarla?")]
    public int _kacinciTarla;
    [Header("Sadece Ilk Tarlaysa Tiklenecek")]
    public bool _ilkTarlaMi;
    [Header("Sadece Ilk Tarlaysa Ambar Objesi Konacak")]
    public GameObject _ambarObject;
    public GameObject _samanBalyalari;
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
    [Header("Icerisindeki Canvas Objesi")]
    [SerializeField] private GameObject _canvasObject;
    [Header("Icerisindeki Aclik Slider")]
    [SerializeField] private Slider _aclikSlider;
    [Header("Icerisindeki Calisan İsci Objesi")]
    [SerializeField] private GameObject _calisanIsci;
    [SerializeField] private FarmerScript _farmerScript;



    private int _toplananMalzemeSayisi;
    private MeshRenderer _meshRenderer;
    private SirtCantasiScript _sirtCantasiScript;
    private Rigidbody _playerRigidbody;

    private float _timer;

    private bool _tarlaSayisiArtirildi;

    private float _aclikTimer;

    private bool _calisiyor;

    private bool _aktifMi;


    void Start()
    {




        _sirtCantasiScript = GameObject.FindGameObjectWithTag("Player").GetComponent<SirtCantasiScript>();
        _playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();


        AktiflikSorgula();




        _timer = 0;
    }



    void Update()
    {
        if (GameController.instance.isContinue == true)
        {
            _aclikTimer += Time.deltaTime;

            if (_aclikTimer > 1)
            {
                if (SirtCantasiScript._kasapVar == true)
                {
                    _aclikSlider.value -= 0.01f;
                }
                else
                {

                }

                _aclikTimer = 0;
            }
            else
            {

            }

            if (_aclikSlider.value == 0 && _calisiyor == true)
            {
                _calisiyor = false;
                _calisanIsci.SetActive(false);
                AmbarSpawnScript._aktifTarlaSayisi--;


            }
            else if (_aclikSlider.value > 0 && _calisiyor == false)
            {
                _calisiyor = true;
                _calisanIsci.SetActive(true);
                AmbarSpawnScript._aktifTarlaSayisi++;
                _farmerScript.Resetle();


            }
        }
        else
        {

        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ToplanmisSamanBalyasi")
        {
            //other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Player")
        {
            if (_aclikSlider.value < 0.8f)
            {
                if (_sirtCantasiScript._cantadakiEtObjeleri.Count > 0)
                {
                    _sirtCantasiScript.EtCek(_malKabulNoktasi);
                    _aclikSlider.value = 1;
                }
                else
                {

                }
            }
            else
            {

            }
        }
        else if (other.gameObject.tag == "ToplanmisEt")
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

                    if (_aktifMi == false)
                    {
                        _tarlaObject.SetActive(true);
                        _meshRenderer.enabled = false;
                        _ihtiyacText.gameObject.SetActive(false);
                        _canvasObject.SetActive(false);
                        _aclikSlider.value = 1;
                        _aktifMi = true;

                        if (_kacinciTarla == 1)
                        {
                            PlayerPrefs.SetInt("BirinciTarlaAktif", 1);
                        }
                        else if (_kacinciTarla == 2)
                        {
                            PlayerPrefs.SetInt("IkinciTarlaAktif", 1);
                        }
                        else if (_kacinciTarla == 3)
                        {
                            PlayerPrefs.SetInt("UcuncuTarlaAktif", 1);
                        }
                        else if (_kacinciTarla == 4)
                        {
                            PlayerPrefs.SetInt("DorduncuTarlaAktif", 1);
                        }
                        else if (_kacinciTarla == 5)
                        {
                            PlayerPrefs.SetInt("BesinciTarlaAktif", 1);
                        }
                        else
                        {

                        }

                        if (_tarlaSayisiArtirildi == false)
                        {
                            AmbarSpawnScript._aktifTarlaSayisi++;
                            _tarlaSayisiArtirildi = true;
                        }
                        else
                        {

                        }


                        if (_sonrasindaTarlaVarMi)
                        {
                            _sonrakiTarlaObject.SetActive(true);
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
            else
            {

            }

        }
        else
        {

        }
    }

    private void AktiflikSorgula()
    {
        if (PlayerPrefs.GetInt("BirinciTarlaAktif") == 0 && PlayerPrefs.GetInt("IkinciTarlaAktif") == 0 && PlayerPrefs.GetInt("UcuncuTarlaAktif") == 0 && PlayerPrefs.GetInt("DorduncuTarlaAktif") == 0 && PlayerPrefs.GetInt("BesinciTarlaAktif") == 0)
        {

            _tarlaObject.SetActive(false);
            _canvasObject.SetActive(true);
            _tarlaSayisiArtirildi = false;
            _aktifMi = false;
            _ihtiyacText.text = _gerekliMalzemeSayisi.ToString();
            _aclikSlider.value = 1;
            _calisiyor = true;
            _calisanIsci.SetActive(true);


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

        }
        else
        {
            if (_kacinciTarla == 1)
            {
                if (PlayerPrefs.GetInt("BirinciTarlaAktif") == 1)
                {
                    if (_ilkTarlaMi)
                    {
                        _ambarObject.SetActive(true);
                        _samanBalyalari.SetActive(false);
                        Debug.Log("Ambar Acildi");

                    }
                    else
                    {
                        Debug.Log("Ambar Acilamadi");
                    }


                    _tarlaObject.SetActive(true);
                    _meshRenderer.enabled = false;
                    _ihtiyacText.gameObject.SetActive(false);
                    _canvasObject.SetActive(false);
                    _aclikSlider.value = 1;
                    _aktifMi = true;
                    _gerekliMalzemeSayisi = 0;


                    /*
                    if (_tarlaSayisiArtirildi == false)
                    {
                        AmbarSpawnScript._aktifTarlaSayisi++;
                        _tarlaSayisiArtirildi = true;
                    }
                    else
                    {

                    }
                    */

                    if (_sonrasindaTarlaVarMi)
                    {
                        _sonrakiTarlaObject.SetActive(true);
                    }
                    else
                    {

                    }
                }
                else
                {
                    _tarlaObject.SetActive(false);
                    _canvasObject.SetActive(true);
                    _tarlaSayisiArtirildi = false;
                    _aktifMi = false;
                    _ihtiyacText.text = _gerekliMalzemeSayisi.ToString();
                    _aclikSlider.value = 1;
                    _calisiyor = true;
                    _calisanIsci.SetActive(true);

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
                }
            }
            else if (_kacinciTarla == 2)
            {
                if (PlayerPrefs.GetInt("IkinciTarlaAktif") == 1)
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
                    _canvasObject.SetActive(false);
                    _aclikSlider.value = 1;
                    _aktifMi = true;
                    _gerekliMalzemeSayisi = 0;


                    /*
                    if (_tarlaSayisiArtirildi == false)
                    {
                        AmbarSpawnScript._aktifTarlaSayisi++;
                        _tarlaSayisiArtirildi = true;
                    }
                    else
                    {

                    }
                    */

                    if (_sonrasindaTarlaVarMi)
                    {
                        _sonrakiTarlaObject.SetActive(true);
                    }
                    else
                    {

                    }
                }
                else
                {
                    _tarlaObject.SetActive(false);
                    _canvasObject.SetActive(true);
                    _tarlaSayisiArtirildi = false;
                    _aktifMi = false;
                    _ihtiyacText.text = _gerekliMalzemeSayisi.ToString();
                    _aclikSlider.value = 1;
                    _calisiyor = true;
                    _calisanIsci.SetActive(true);

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
                }
            }
            else if (_kacinciTarla == 3)
            {
                if (PlayerPrefs.GetInt("UcuncuTarlaAktif") == 1)
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
                    _canvasObject.SetActive(false);
                    _aclikSlider.value = 1;
                    _aktifMi = true;
                    _gerekliMalzemeSayisi = 0;


                    /*
                    if (_tarlaSayisiArtirildi == false)
                    {
                        AmbarSpawnScript._aktifTarlaSayisi++;
                        _tarlaSayisiArtirildi = true;
                    }
                    else
                    {

                    }
                    */

                    if (_sonrasindaTarlaVarMi)
                    {
                        _sonrakiTarlaObject.SetActive(true);
                    }
                    else
                    {

                    }
                }
                else
                {
                    _tarlaObject.SetActive(false);
                    _canvasObject.SetActive(true);
                    _tarlaSayisiArtirildi = false;
                    _aktifMi = false;
                    _ihtiyacText.text = _gerekliMalzemeSayisi.ToString();
                    _aclikSlider.value = 1;
                    _calisiyor = true;
                    _calisanIsci.SetActive(true);

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
                }
            }
            else if (_kacinciTarla == 4)
            {
                if (PlayerPrefs.GetInt("DorduncuTarlaAktif") == 1)
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
                    _canvasObject.SetActive(false);
                    _aclikSlider.value = 1;
                    _aktifMi = true;
                    _gerekliMalzemeSayisi = 0;


                    /*
                    if (_tarlaSayisiArtirildi == false)
                    {
                        AmbarSpawnScript._aktifTarlaSayisi++;
                        _tarlaSayisiArtirildi = true;
                    }
                    else
                    {

                    }
                    */

                    if (_sonrasindaTarlaVarMi)
                    {
                        _sonrakiTarlaObject.SetActive(true);
                    }
                    else
                    {

                    }
                }
                else
                {
                    _tarlaObject.SetActive(false);
                    _canvasObject.SetActive(true);
                    _tarlaSayisiArtirildi = false;
                    _aktifMi = false;
                    _ihtiyacText.text = _gerekliMalzemeSayisi.ToString();
                    _aclikSlider.value = 1;
                    _calisiyor = true;
                    _calisanIsci.SetActive(true);

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
                }
            }
            else if (_kacinciTarla == 5)
            {
                if (PlayerPrefs.GetInt("BesinciTarlaAktif") == 1)
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
                    _canvasObject.SetActive(false);
                    _aclikSlider.value = 1;
                    _aktifMi = true;
                    _gerekliMalzemeSayisi = 0;


                    /*
                    if (_tarlaSayisiArtirildi == false)
                    {
                        AmbarSpawnScript._aktifTarlaSayisi++;
                        _tarlaSayisiArtirildi = true;
                    }
                    else
                    {

                    }
                    */

                    if (_sonrasindaTarlaVarMi)
                    {
                        _sonrakiTarlaObject.SetActive(true);
                    }
                    else
                    {

                    }
                }
                else
                {
                    _tarlaObject.SetActive(false);
                    _canvasObject.SetActive(true);
                    _tarlaSayisiArtirildi = false;
                    _aktifMi = false;
                    _ihtiyacText.text = _gerekliMalzemeSayisi.ToString();
                    _aclikSlider.value = 1;
                    _calisiyor = true;
                    _calisanIsci.SetActive(true);

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
                }
            }
            else
            {

            }
        }


    }
}
