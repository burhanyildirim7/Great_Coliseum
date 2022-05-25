using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArastirmaTezgahiScript : MonoBehaviour
{
    [Header("Cekilen Malzemenin Gidecegi Transform")]
    public Transform _malKabulNoktasi;
    [Header("Bu Levelde Toplam Kac Arastirma Var")]
    public int _arastirmaSayisi;
    [Header("Ilk Arastirma Türü")]
    public bool _ilkBinaArastirmasiVarMi;
    [Header("Ilk Arastirmada Gereken Malzemeler")]
    public int _ilkArastirmaGerekliSamanSayisi;
    public int _ilkArastirmaGerekliAltinSayisi;
    public int _ilkArastirmaGerekliDemirSayisi;
    [Header("Arastirilmasi İcin İhtiyac Olan Malzemelerin Sayi Texti")]
    public Text _samanIhtiyacText;
    public Text _altinIhtiyacText;
    public Text _demirIhtiyacText;
    [Header("Arastirinca Acilacak Bina")]
    public GameObject _ilkArastirmaAcilacakBinaObject;


    private int _ilkArastirmaToplananSamanSayisi;
    private int _ilkArastirmaToplananAltinSayisi;
    private int _ilkArastirmaToplananDemirSayisi;
    //private MeshRenderer _meshRenderer;
    private SirtCantasiScript _sirtCantasiScript;
    private Rigidbody _playerRigidbody;
    private int _tamamlananArastirmaSayisi;
    private float _timer;


    void Start()
    {


        _ilkArastirmaAcilacakBinaObject.SetActive(false);

        _sirtCantasiScript = GameObject.FindGameObjectWithTag("Player").GetComponent<SirtCantasiScript>();
        _playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        //_meshRenderer = GetComponent<MeshRenderer>();
        _samanIhtiyacText.text = _ilkArastirmaGerekliSamanSayisi.ToString();
        _altinIhtiyacText.text = _ilkArastirmaGerekliAltinSayisi.ToString();
        _demirIhtiyacText.text = _ilkArastirmaGerekliDemirSayisi.ToString();


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
                if (_ilkBinaArastirmasiVarMi)
                {
                    if (_ilkArastirmaGerekliSamanSayisi > 0 || _ilkArastirmaGerekliAltinSayisi > 0)
                    {
                        _timer += Time.deltaTime;


                        if (_timer > 0.1f)
                        {

                            if (_ilkArastirmaGerekliSamanSayisi > 0)
                            {
                                if (_sirtCantasiScript._cantadakiSamanObjeleri.Count > 0)
                                {
                                    _sirtCantasiScript.SamanCek(_malKabulNoktasi);
                                    _ilkArastirmaGerekliSamanSayisi--;
                                    _samanIhtiyacText.text = _ilkArastirmaGerekliSamanSayisi.ToString();
                                    _timer = 0;
                                }
                                else
                                {

                                }
                            }
                            else
                            {

                            }


                            if (_ilkArastirmaGerekliAltinSayisi > 0)
                            {
                                if (_sirtCantasiScript._cantadakiAltinObjeleri.Count > 0)
                                {
                                    _sirtCantasiScript.AltinCek(_malKabulNoktasi);
                                    _ilkArastirmaGerekliAltinSayisi--;
                                    _altinIhtiyacText.text = _ilkArastirmaGerekliAltinSayisi.ToString();
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
                        _ilkArastirmaAcilacakBinaObject.SetActive(true);
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
