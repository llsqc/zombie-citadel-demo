using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager _instance = new UIManager();
    public static UIManager Instance => _instance;

    private Dictionary<string, BasePanel> _panelDic = new Dictionary<string, BasePanel>();

    private Transform _canvasTransform;

    private UIManager()
    {
        GameObject canvas = GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas"));
        _canvasTransform = canvas.transform;

        GameObject.DontDestroyOnLoad(canvas);
    }

    public T ShowPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (_panelDic.ContainsKey(panelName))
            return _panelDic[panelName] as T;
        GameObject panelObj =
            GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName), _canvasTransform, false);

        T panel = panelObj.GetComponent<T>();
        _panelDic.Add(panelName, panel);
        panel.ShowMe();
        return panel;
    }

    public void HidePanel<T>(bool isFade = true) where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (_panelDic.ContainsKey(panelName))
        {
            if (isFade)
            {
                _panelDic[panelName].HideMe(() =>
                {
                    GameObject.Destroy(_panelDic[panelName].gameObject);
                    _panelDic.Remove(panelName);
                });
            }
            else
            {
                GameObject.Destroy(_panelDic[panelName].gameObject);
                _panelDic.Remove(panelName);
            }
        }
    }

    public T GetPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (_panelDic.ContainsKey(panelName))
            return _panelDic[panelName] as T;
        return null;
    }
}