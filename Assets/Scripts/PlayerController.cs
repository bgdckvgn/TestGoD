using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public FixedJoystick joystick = null;
    public Slider healthSlider = null;
    private float _damageToTake = 0.5f;
    private float _speed = 2f;
    private float _health = 10;
    private float _maxHealth = 10;
    private Animator _animator;

    // Start is called before the first frame update
    void Awake() {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
           _animator.SetTrigger("Walk");

            if(Mathf.Abs(joystick.Horizontal) > Mathf.Abs(joystick.Vertical))
            {
                //GetComponent<Rigidbody>().AddForce(Vector3.forward * Time.deltaTime * _speed * Mathf.Abs(joystick.Horizontal) * 200);
                transform.Translate(Vector3.forward * Time.deltaTime * _speed * Mathf.Abs(joystick.Horizontal));
            }
            else
            {
                if(Mathf.Abs(joystick.Horizontal) < Mathf.Abs(joystick.Vertical))
                {
                    //GetComponent<Rigidbody>().AddForce(Vector3.forward * Time.deltaTime * _speed * Mathf.Abs(joystick.Vertical) * 200);
                    transform.Translate(Vector3.forward * Time.deltaTime * _speed * Mathf.Abs(joystick.Vertical));
                }
            }
            transform.LookAt(transform.localPosition + new Vector3(joystick.Horizontal, 0, joystick.Vertical));
        }
    }

    void LateUpdate()
    {
        healthSlider.value = _health / _maxHealth;
    }

    public void Kick()
    {
        _animator.SetTrigger("Kick");
    }

    public void OnTriggerEnter(Collider target)
    {
        if (target.GetComponentInParent<EnemyController>() != null &&
            target.GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Kick"))
        {
            TakeDamage();
        }
    }

    public void Die() {
        FindObjectOfType<GameController>().Gameover();
    }

    private void TakeDamage()
    {
        _health -= _damageToTake;
        if (_health <= 0) {
            Die();
        }
        Debug.Log(_health);
    }
}