using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Game Event", menuName = "Game Event", order = 52)] // 1
public class GameEvent<T> : ScriptableObject where T : IGameEventListener// 2
{
    protected T[] Listeners
        => FindObjectsOfType<MonoBehaviour>().OfType<T>().ToArray();


    public virtual void Raise()
    {
        //http://buravo46.hatenablog.com/entry/2015/01/07/211635
        /*for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised();
        }*/
        foreach (T listener in this.Listeners)
        {
            listener.OnEventRaised();
        }
    }
    /*
        private List<T> listeners = new List<T>(); // 3
     * 
        public void RegisterListener(T listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(T listener)
        {
            listeners.Remove(listener);
        }
        */
}

public interface IGameEventListener
{
    void OnEventRaised();
}