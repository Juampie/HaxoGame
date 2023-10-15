using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviourPunCallbacks
{
    [SerializeField] private JumpFX _jumpFX;
    [SerializeField] private float _duration = 1f;
    public GameObject _standsTile;
    private Animator _animator;
    private PhotonView _view;
    private string _jumpingTrigger = "Jumping";
    private bool _isJumping = false; 

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _jumpFX = GetComponent<JumpFX>();
        _view = GetComponent<PhotonView>();
       

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _view.IsMine)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;

                if (!_isJumping && Nearby(objectHit.position, _standsTile.transform.position))
                {
                    if (_standsTile != objectHit.gameObject && objectHit.gameObject.CompareTag("Road"))
                    {
                        StartCoroutine(Jumping(objectHit.position));
                        _standsTile = objectHit.gameObject;
                    }
                }
            }
        }
    }

    IEnumerator Jumping(Vector3 target)
    {
        _isJumping = true;

        _jumpFX.PlayAnimations(transform, _duration, target);
        PlayJumpAnimation();

        yield return new WaitForSeconds(_duration);

        _isJumping = false;
    }

    public void PlayJumpAnimation()
    {
        photonView.RPC("PlayJumpAnimationRPC", RpcTarget.All);
    }

    [PunRPC]
    private void PlayJumpAnimationRPC()
    {
        _animator.SetTrigger(_jumpingTrigger);
    }

    private bool Nearby(Vector3 now, Vector3 goTo)
    {
        float xDistance = Mathf.Abs(goTo.x - now.x);
        float zDistance = Mathf.Abs(goTo.z - now.z);

        const float thresholdX = 0.9f;
        const float thresholdZ = 0.8f;

        return (xDistance <= thresholdX) && (zDistance <= thresholdZ);
    }
}