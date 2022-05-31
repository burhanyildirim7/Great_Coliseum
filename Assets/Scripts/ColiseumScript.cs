using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            else
            {

            }
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

                            }


                        }
                        else
                        {

                        }

                    }
                    else
                    {
                        GameController.instance.isContinue = false;
                        GameController.instance.SetScore(100);
                        GameController.instance.ScoreCarp(1);
                        UIController.instance.ActivateWinScreen();
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
}
