using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    HealthPlayer healthPlayer;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FloatingJoystick joyStick;
    [SerializeField] private Animator _animator;
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private FireController fireController;
    [Header("UI Settings")]
    [SerializeField] private GameObject beforeGamePanel;
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private RectTransform levelPanel;
    [SerializeField] private float _moveSpeed;
    private float hMove;
    private float vMove;
    public bool isShooting;
    public int animatorSpeed;
    public int isMovementForPanel;

    private void Awake()
    {
        fireController = GameObject.Find("Canvas").GetComponent<FireController>();
        healthPlayer = GetComponent<HealthPlayer>();
        weaponController = GetComponent<WeaponController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "HydrantObs")
        {
            Physics.IgnoreCollision(collision.transform.GetComponent<BoxCollider>(), GetComponent<CapsuleCollider>(), true);
        }
    }

    private void FixedUpdate()
    {
        if ((hMove != 0 || vMove != 0) && isMovementForPanel < 1)
        {
            isMovementForPanel++;
            beforeGamePanel.SetActive(false);
            inGamePanel.SetActive(true);
            levelPanel.localPosition = new Vector2(-180,895);
            levelPanel.localScale = new Vector3(1, 1, 1);
        }
        if (healthPlayer.health <= 0)
        {
            _animator.SetBool("playerDeath", true);
            return;
        }
        Vector3 tempVector = Vector3.forward * joyStick.Vertical + Vector3.right * joyStick.Horizontal;

        hMove = tempVector.x;
        vMove = tempVector.z;


        if (hMove < 0) hMove = -hMove;
        if (vMove < 0) vMove = -vMove;

        bool isMoving = (hMove != 0 || vMove != 0);


        if (isMoving == false)
        {
            isShooting = true;
        }
        else
        {
            isShooting = false;
        }

        _animator.SetBool("isRunning", isMoving);
        _animator.SetFloat("runningSpeed", animatorSpeed);
        if (isShooting) return;
        Quaternion temp = Quaternion.LookRotation(tempVector, Vector3.up);
        gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, temp, 900 * Time.deltaTime * 100);

    }
}
