using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float JumpForce = 15f;
    public float GravityScale = 5f;
    public float RotateSpeed = 5f;

    public Animator Animator;
    public Transform Pivot;

    // variables for KnockBack effect
    public float KnockBackForce = 20f;
    public float KnockBackTime = 0.5f;
    private float _knockBackCounter;

    private GameObject _model;
	[HideInInspector] public CharacterController _characterController;
    private Vector3 _moveDirection;

    public bool CanMove;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();

        CanMove = true;

        _model = Animator.gameObject;
    }

    private void Update()
    {
        if (!CanMove)
            return;

        var xAxis = Input.GetAxis("Horizontal") * MoveSpeed;
        var zAxis = Input.GetAxis("Vertical") * MoveSpeed;

        if (_knockBackCounter <= 0)
        {
            //_moveDirection = new Vector3(xAxis, _moveDirection.y, zAxis);
            var yStore = _moveDirection.y;

            _moveDirection = (transform.forward * zAxis) + (transform.right * xAxis);
            _moveDirection = _moveDirection.normalized * MoveSpeed;
            _moveDirection.y = yStore;

            if (_characterController.isGrounded)
            {
                _moveDirection.y = 0f;
                if (Input.GetButtonDown("Jump"))
                {
                    Animator.SetTrigger("Jump");
                    _moveDirection.y = JumpForce;
                }
            }
        }
        else
        {
            _knockBackCounter -= Time.deltaTime;
        }

        _moveDirection.y += Physics.gravity.y * GravityScale * Time.deltaTime;
        _characterController.Move(_moveDirection * Time.deltaTime);

        // Move the player in different direction based on camera look direction
        if (xAxis != 0 ||zAxis != 0)
        {
            transform.rotation = Quaternion.Euler(0f, Pivot.rotation.eulerAngles.y, 0f);
            var newRotation = Quaternion.LookRotation(new Vector3(_moveDirection.x, 0f, _moveDirection.z));
            _model.transform.rotation =
                Quaternion.Slerp(_model.transform.rotation, newRotation, RotateSpeed * Time.deltaTime);
        }

       
        Animator.SetFloat("Speed", Mathf.Abs(zAxis) + Mathf.Abs(xAxis));
    }

    public void KnockBack(Vector3 direction)
    {
        _knockBackCounter = KnockBackTime;

        _moveDirection = direction * KnockBackForce;
    }

    public IEnumerator Death()
    {
        CameraZoom camera = FindObjectOfType<CameraZoom>();

        CanMove = false;
        camera.DeathZoom();
        Animator.SetBool("Death", true);

        yield return new WaitForSeconds(1.5f);

        FindObjectOfType<EndGame>().GameOverScene();
    }

}