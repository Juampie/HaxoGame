using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Jump : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private JumpFX _jumpFX;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _duration = 1f;
    [SerializeField] private GameObject _standsTile;
    private Animator _animator;
    private string _jumpingTrigger = "Jumping";
    private bool _isJumping = false;

    private void Start()
    {
        _animator = _player.GetComponent<Animator>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_isJumping && eventData.button == PointerEventData.InputButton.Left)
        {


            if (Nearby(eventData.pointerEnter.transform.position, _standsTile.transform.position))
            {
                if (_standsTile != eventData.pointerEnter)
                {
                    StartCoroutine(Jumping(eventData));
                    _standsTile = eventData.pointerEnter;
                }


            }


        }


    }
    IEnumerator Jumping(PointerEventData eventData)
    {
        _isJumping = true;

        _jumpFX.PlayAnimations(_player.transform, _duration, eventData.pointerEnter.transform.position);
        _animator.SetTrigger(_jumpingTrigger);

        yield return new WaitForSeconds(_duration);

        _isJumping = false;
    }



    private bool Nearby(Vector3 now, Vector3 goTo)
    {
        float xDistance = Mathf.Abs(goTo.x - now.x);
        float zDistance = Mathf.Abs(goTo.z - now.z);

        const float thresholdX = 0.88f;
        const float thresholdZ = 0.76f;

        return (xDistance <= thresholdX) && (zDistance <= thresholdZ);
    }
}
