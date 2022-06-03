using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ColiseumScript : MonoBehaviour
{
    [Header("Ihtiyac Listesi")]
    public bool _samanGerekli;
    public bool _altinGerekli;
    public bool _gladyatorGerekli;

    [Header("Acilmasi İcin İhtiyac Olan Malzemelerin Sayi Texti")]
    public Text _samanIhtiyacText;
    public Text _altinIhtiyacText;
    public Text _gladyatorIhtiyacText;

    [Header("Acilmasi İcin İhtiyac Olan Malzeme Sayisi(Gerekmeyenlere 0 Yazicalak)")]
    public int _gerekliSamanSayisi;
    public int _gerekliAltinSayisi;
    public int _gerekliGladyatorSayisi;

    [Header("Cekilen Malzemenin Gidecegi Transform")]
    public Transform _malKabulNoktasi;

    [Header("Konfeti Paketi")]
    [SerializeField] private GameObject _konfetiler;

    [Header("Canvas Object")]
    [SerializeField] private GameObject _canvasObject;

    private int _toplananMalzemeSayisi;
    private MeshRenderer _meshRenderer;
    private SirtCantasiScript _sirtCantasiScript;
    private Rigidbody _playerRigidbody;

    private float _timer;


    void Start()
    {
        if (PlayerPrefs.GetInt("KolezyumGerekliSaman") > 0)
        {
            _gerekliSamanSayisi = PlayerPrefs.GetInt("KolezyumGerekliSaman");
        }
        else
        {
            PlayerPrefs.SetInt("KolezyumGerekliSaman", _gerekliSamanSayisi);
        }

        if (PlayerPrefs.GetInt("KolezyumGerekliAltin") > 0)
        {
            _gerekliAltinSayisi = PlayerPrefs.GetInt("KolezyumGerekliAltin");
        }
        else
        {
            PlayerPrefs.SetInt("KolezyumGerekliAltin", _gerekliAltinSayisi);
        }

        if (PlayerPrefs.GetInt("KolezyumGerekliGladyator") > 0)
        {
            _gerekliGladyatorSayisi = PlayerPrefs.GetInt("KolezyumGerekliGladyator");
        }
        else
        {
            PlayerPrefs.SetInt("KolezyumGerekliGladyator", _gerekliGladyatorSayisi);
        }


        _sirtCantasiScript = GameObject.FindGameObjectWithTag("Player").GetComponent<SirtCantasiScript>();
        _playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _samanIhtiyacText.text = _gerekliSamanSayisi.ToString();
        _altinIhtiyacText.text = _gerekliAltinSayisi.ToString();
        _gladyatorIhtiyacText.text = _gerekliGladyatorSayisi.ToString();
        _konfetiler.SetActive(false);

        //_canvasObject.transform.DOScale(new Vector3(_canvasObject.transform.localScale.x * 1.5f, _canvasObject.transform.localScale.y * 1.5f, _canvasObject.transform.localScale.z * 1.5f), 2f).OnComplete(() => _canvasObject.transform.DOScale(new Vector3(_canvasObject.transform.localScale.x / 1.5f, _canvasObject.transform.localScale.y / 1.5f, _canvasObject.transform.localScale.z / 1.5f), 2f));


        _timer = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameController.instance.isContinue == true)
        {
            if (other.gameObject.tag == "Gladyator")
            {
                if (_gerekliGladyatorSayisi > 0)
                {
                    _gerekliGladyatorSayisi--;
                    _gladyatorIhtiyacText.text = _gerekliGladyatorSayisi.ToString();
                    PlayerPrefs.SetInt("KolezyumGerekliGladyator", _gerekliGladyatorSayisi);
                }
                else
                {

                }

            }
            else if (other.gameObject.tag == "Player")
            {
                _canvasObject.transform.DOScale(new Vector3(_canvasObject.transform.localScale.x * 1.2f, _canvasObject.transform.localScale.y * 1.2f, _canvasObject.transform.localScale.z * 1.2f), 0.5f);

            }
            else
            {

            }
        }

        else
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _canvasObject.transform.DOScale(new Vector3(_canvasObject.transform.localScale.x / 1.2f, _canvasObject.transform.localScale.y / 1.2f, _canvasObject.transform.localScale.z / 1.2f), 0.5f);

        }
        else
        {

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (GameController.instance.isContinue == true)
        {
            if (other.gameObject.tag == "Player")
            {
                if (SirtCantasiScript._ilkTarlaAktif == true)
                {
                    if (_playerRigidbody.velocity.x == 0 || _playerRigidbody.velocity.z == 0)
                    {
                        if (_gerekliSamanSayisi > 0 || _gerekliAltinSayisi > 0 || _gerekliGladyatorSayisi > 0)
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
                                        _samanIhtiyacText.text = _gerekliSamanSayisi.ToString();
                                        _timer = 0;
                                        PlayerPrefs.SetInt("KolezyumGerekliSaman", _gerekliSamanSayisi);
                                    }
                                    else
                                    {

                                    }
                                }
                                else
                                {
                                    /*
                                    if (_sirtCantasiScript._cantadakiSamanObjeleri.Count > 0)
                                    {
                                        _sirtCantasiScript.SamanCek(_malKabulNoktasi);
                                        //_gerekliSamanSayisi--;
                                        //_samanIhtiyacText.text = _gerekliSamanSayisi.ToString();
                                        _timer = 0;
                                        //PlayerPrefs.SetInt("KolezyumGerekliSaman", _gerekliSamanSayisi);
                                    }
                                    else
                                    {

                                    }
                                    */
                                }

                                if (_gerekliAltinSayisi > 0)
                                {
                                    if (_sirtCantasiScript._cantadakiAltinObjeleri.Count > 0)
                                    {
                                        _sirtCantasiScript.AltinCek(_malKabulNoktasi);
                                        _gerekliAltinSayisi--;
                                        _altinIhtiyacText.text = _gerekliAltinSayisi.ToString();
                                        _timer = 0;
                                        PlayerPrefs.SetInt("KolezyumGerekliAltin", _gerekliAltinSayisi);
                                    }
                                    else
                                    {

                                    }
                                }
                                else
                                {
                                    /*
                                    if (_sirtCantasiScript._cantadakiAltinObjeleri.Count > 0)
                                    {
                                        _sirtCantasiScript.AltinCek(_malKabulNoktasi);
                                        //_gerekliAltinSayisi--;
                                        //_altinIhtiyacText.text = _gerekliAltinSayisi.ToString();
                                        _timer = 0;
                                        //PlayerPrefs.SetInt("KolezyumGerekliAltin", _gerekliAltinSayisi);
                                    }
                                    else
                                    {

                                    }
                                    */
                                }

                                /*
                                if (_sirtCantasiScript._cantadakiDemirObjeleri.Count > 0)
                                {
                                    _sirtCantasiScript.DemirCek(_malKabulNoktasi);
                                    //_gerekliAltinSayisi--;
                                    //_altinIhtiyacText.text = _gerekliAltinSayisi.ToString();
                                    _timer = 0;
                                    //PlayerPrefs.SetInt("KolezyumGerekliAltin", _gerekliAltinSayisi);
                                }
                                else
                                {

                                }

                                if (_sirtCantasiScript._cantadakiEtObjeleri.Count > 0)
                                {
                                    _sirtCantasiScript.EtCek(_malKabulNoktasi);
                                    //_gerekliAltinSayisi--;
                                    //_altinIhtiyacText.text = _gerekliAltinSayisi.ToString();
                                    _timer = 0;
                                    //PlayerPrefs.SetInt("KolezyumGerekliAltin", _gerekliAltinSayisi);
                                }
                                else
                                {

                                }
                                */

                            }
                            else
                            {

                            }

                        }
                        else
                        {
                            GameController.instance.isContinue = false;
                            _konfetiler.SetActive(true);
                            StartCoroutine(OyunSonuTitresim());
                            Invoke("WinAktif", 2f);
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
        else
        {

        }

    }

    private void WinAktif()
    {
        GameController.instance.SetScore(100);
        GameController.instance.ScoreCarp(1);
        UIController.instance.ActivateWinScreen();
    }

    private IEnumerator OyunSonuTitresim()
    {
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        yield return new WaitForSeconds(0.1f);
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        yield return new WaitForSeconds(0.1f);
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        yield return new WaitForSeconds(0.1f);
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        yield return new WaitForSeconds(0.1f);
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        yield return new WaitForSeconds(0.1f);
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        yield return new WaitForSeconds(0.1f);
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        yield return new WaitForSeconds(0.1f);
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        yield return new WaitForSeconds(0.1f);
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        yield return new WaitForSeconds(0.1f);
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        yield return new WaitForSeconds(0.1f);
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        yield return new WaitForSeconds(0.1f);
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        yield return new WaitForSeconds(0.1f);
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        yield return new WaitForSeconds(0.1f);
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        yield return new WaitForSeconds(0.1f);
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        yield return new WaitForSeconds(0.1f);
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        yield return new WaitForSeconds(0.1f);
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        yield return new WaitForSeconds(0.1f);
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        yield return new WaitForSeconds(0.1f);
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        yield return new WaitForSeconds(0.1f);
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        yield return new WaitForSeconds(0.1f);
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
    }
}
