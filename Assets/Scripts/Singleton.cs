using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
    public static T Instance { get; private set; }

    protected virtual void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one " + typeof(T) + " in scene!");
            Destroy(gameObject);
            return;
        }

        Instance = GetComponent<T>();
    }
}