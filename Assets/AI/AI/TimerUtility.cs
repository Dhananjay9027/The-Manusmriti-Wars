
using System.Collections;
using UnityEngine;
public class TimerUtility : MonoBehaviour
{
    private static TimerUtility instance;

    public static TimerUtility Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject timerObject = new GameObject("TimerUtility");
                instance = timerObject.AddComponent<TimerUtility>();
                DontDestroyOnLoad(timerObject); // Prevent destruction across scenes
            }
            return instance;
        }
    }

    public void DelayAction(System.Action action, float delay)
    {
        StartCoroutine(DelayedCoroutine(action, delay));
    }

    private IEnumerator DelayedCoroutine(System.Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
}