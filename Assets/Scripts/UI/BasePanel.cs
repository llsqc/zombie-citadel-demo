using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 基础面板类，用于管理UI面板的显示和隐藏
public abstract class BasePanel : MonoBehaviour
{
    private CanvasGroup _canvasGroup; // 用于控制面板透明度的CanvasGroup组件
    private float _alphaSpeed = 10f; // 透明度变化速度
    public bool isShow = false; // 面板是否显示的标志
    private UnityAction _hideCallback; // 隐藏完成后的回调函数

    // 初始化CanvasGroup组件
    protected virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();
    }

    // 调用Init方法进行初始化
    protected virtual void Start()
    {
        Init();
    }

    // 更新面板的透明度
    private void Update()
    {
        if (isShow && _canvasGroup.alpha < 1)
        {
            _canvasGroup.alpha += _alphaSpeed * Time.deltaTime;
            if (_canvasGroup.alpha >= 1)
                _canvasGroup.alpha = 1;
        }
        else if (!isShow && _canvasGroup.alpha > 0)
        {
            _canvasGroup.alpha -= _alphaSpeed * Time.deltaTime;
            if (_canvasGroup.alpha <= 0)
            {
                _canvasGroup.alpha = 0;
                _hideCallback?.Invoke();
            }
        }
    }

    // 子类需要实现的初始化方法
    protected abstract void Init();

    // 显示面板
    public virtual void ShowMe()
    {
        _canvasGroup.alpha = 0;
        isShow = true;
    }

    // 隐藏面板，并可设置隐藏完成后的回调
    public virtual void HideMe(UnityAction callBack)
    {
        _canvasGroup.alpha = 1;
        isShow = false;
        _hideCallback = callBack;
    }
}