using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraAnimator : MonoBehaviour
{
    private Animator _animator;
    
    private UnityAction _overAction;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void TurnLeft(UnityAction action)
    {
        _animator.SetTrigger("Left");
        _overAction = action;
    }
    
    public void TurnRight(UnityAction action)
    {
        _animator.SetTrigger("Right");
        _overAction = action;
    }

    public void AnimationPlayOver()
    {
        _overAction?.Invoke();
        _overAction = null;
    }
}
