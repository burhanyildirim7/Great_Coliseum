using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AmbarSpawnScript : MonoBehaviour
{
    [Header("Urunlerin Spawn Olacagi Nokta")]
    public Transform _urunSpawnNoktasi;
    [Header("Spawn Olacak Urun Prefabi")]
    public GameObject _urunPrefab;
    [Header("Olusan Urunlerin Dizilecegi Noktalar")]
    public List<Transform> _dizilecekTransforms = new List<Transform>();
    [Header("Urunlerin Olusma Hizi")]
    public float _ambarSpawnHizi;
    [Header("Spawn Olacak Urun Parent")]
    public GameObject _urunParent;
    [Header("Kontrol Amacli Burayi Elleme")]
    public List<GameObject> _olusanUrunler = new List<GameObject>();
    public int _kontrolTarlaSayi;


    public static int _aktifTarlaSayisi;

    private float _timer;
    public int _ambarUrunSayisi;
    private int _bosSpawnNoktasi;


    private void Start()
    {
        _aktifTarlaSayisi = 0;
    }

    void Update()
    {
        if (GameController.instance.isContinue == true)
        {
            _timer += Time.deltaTime;
            _kontrolTarlaSayi = _aktifTarlaSayisi;


            //Debug.Log(_aktifTarlaSayisi);

            if (_olusanUrunler.Count < 45)
            {
                if (_timer > _ambarSpawnHizi)
                {

                    AmbarHiziGüncelle();

                    if (_olusanUrunler.Count == _ambarUrunSayisi)
                    {
                        GameObject urun = Instantiate(_urunPrefab, _urunSpawnNoktasi.position, Quaternion.identity);
                        urun.gameObject.transform.DOMove(_dizilecekTransforms[_olusanUrunler.Count].position, 0.5f);
                        urun.gameObject.transform.DOLocalRotate(Vector3.zero, 0.5f);
                        _olusanUrunler.Add(urun);
                        urun.transform.parent = _urunParent.transform;
                        _ambarUrunSayisi++;

                        _timer = 0;
                    }
                    else
                    {

                        for (int i = 0; i < _olusanUrunler.Count; i++)
                        {


                            if (_olusanUrunler[i] == null)
                            {
                                GameObject urun = Instantiate(_urunPrefab, _urunSpawnNoktasi.position, Quaternion.identity);
                                _olusanUrunler[i] = urun;
                                _bosSpawnNoktasi = i;

                                urun.gameObject.transform.DOMove(_dizilecekTransforms[_bosSpawnNoktasi].position, 0.5f);
                                urun.gameObject.transform.DOLocalRotate(Vector3.zero, 0.5f);
                                urun.transform.parent = _urunParent.transform;

                                _ambarUrunSayisi++;

                                break;
                            }
                            else if (_olusanUrunler[i].transform.parent != _urunParent.transform)
                            {
                                GameObject urun = Instantiate(_urunPrefab, _urunSpawnNoktasi.position, Quaternion.identity);
                                _olusanUrunler[i] = urun;
                                _bosSpawnNoktasi = i;

                                urun.gameObject.transform.DOMove(_dizilecekTransforms[_bosSpawnNoktasi].position, 0.5f);
                                urun.gameObject.transform.DOLocalRotate(Vector3.zero, 0.5f);
                                urun.transform.parent = _urunParent.transform;

                                _ambarUrunSayisi++;

                                break;

                            }
                            else
                            {


                            }

                        }

                        _timer = 0;

                    }
                }
            }
            else
            {
                if (_timer > _ambarSpawnHizi)
                {

                    AmbarHiziGüncelle();

                    for (int i = 0; i < _olusanUrunler.Count; i++)
                    {


                        if (_olusanUrunler[i] == null)
                        {
                            GameObject urun = Instantiate(_urunPrefab, _urunSpawnNoktasi.position, Quaternion.identity);
                            _olusanUrunler[i] = urun;
                            _bosSpawnNoktasi = i;

                            urun.gameObject.transform.DOMove(_dizilecekTransforms[_bosSpawnNoktasi].position, 0.5f);
                            urun.gameObject.transform.DOLocalRotate(Vector3.zero, 0.5f);
                            urun.transform.parent = _urunParent.transform;

                            _ambarUrunSayisi++;

                            break;
                        }
                        else if (_olusanUrunler[i].transform.parent != _urunParent.transform)
                        {
                            GameObject urun = Instantiate(_urunPrefab, _urunSpawnNoktasi.position, Quaternion.identity);
                            _olusanUrunler[i] = urun;
                            _bosSpawnNoktasi = i;

                            urun.gameObject.transform.DOMove(_dizilecekTransforms[_bosSpawnNoktasi].position, 0.5f);
                            urun.gameObject.transform.DOLocalRotate(Vector3.zero, 0.5f);
                            urun.transform.parent = _urunParent.transform;

                            _ambarUrunSayisi++;

                            break;

                        }
                        else
                        {


                        }

                    }

                    _timer = 0;
                }
            }
        }
        else
        {

        }

    }

    private void AmbarHiziGüncelle()
    {
        if (_aktifTarlaSayisi == 1)
        {
            _ambarSpawnHizi = 2.5f;
        }
        else if (_aktifTarlaSayisi == 2)
        {
            _ambarSpawnHizi = 2f;
        }
        else if (_aktifTarlaSayisi == 3)
        {
            _ambarSpawnHizi = 1.5f;
        }
        else if (_aktifTarlaSayisi == 4)
        {
            _ambarSpawnHizi = 1f;
        }
        else if (_aktifTarlaSayisi == 5)
        {
            _ambarSpawnHizi = 0.5f;
        }
        else
        {
            _ambarSpawnHizi = 10;
        }
    }
}
