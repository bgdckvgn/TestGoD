using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Slider healthSlider = null;
    public EnemyPoolController enemyPoolController;
    private PlayerController _player = null;
    private float _speed = 1.5f;
    private float _horizontalKickDistance = 0.85f;
    private float _verticalKickDistance = 1.7f;
    private float _health = 10f;
    private float _maxHealth = 10f;
    private float _damageToTake = 1.7f;
    private bool _isEffectFinished = false;
    // Start is called before the first frame update
    void Awake()
    {
        enemyPoolController = FindObjectOfType<EnemyPoolController>();
    }
    //При старте всегда вызывается OnEnable, данный подход нужен для вызова метода при восстановлении из пула.
    void OnEnable() {
        StartCoroutine(SetEffectFinished());
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isEffectFinished) {
            transform.LookAt(_player.transform);
            if ((Math.Abs(transform.position.z - _player.transform.position.z) < _horizontalKickDistance) &&
                (Math.Abs(transform.position.x - _player.transform.position.x) < _verticalKickDistance))
            {
                GetComponent<Animator>().SetTrigger("Kick");
            }
            else
            {
                GetComponent<Animator>().SetTrigger("Walk");
                transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            }
        }
    }

    void LateUpdate()
    {
        healthSlider.value = _health / _maxHealth;
    }

    private IEnumerator SetEffectFinished() {
        yield return new WaitForSeconds(3);
        _isEffectFinished = true;
    }

    public void OnTriggerEnter(Collider target)
    {
        if (target.GetComponentInParent<PlayerController>() != null &&
            target.GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Kick"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        _health -= _damageToTake;
        if (_health <= 0) {
            Die();
        }
    }

    public void Init() {
        gameObject.SetActive(true);
        transform.parent = null;
        transform.position = new Vector3(UnityEngine.Random.Range(-19f, 19f), 0, UnityEngine.Random.Range(-4f, 4f));
        _player = FindObjectOfType<PlayerController>();
        _health = 10;
    }

    public void Die()
    {
        gameObject.SetActive(false);
        enemyPoolController.Add(this);
        transform.parent = enemyPoolController.transform;
        _isEffectFinished = false;
    }
}
