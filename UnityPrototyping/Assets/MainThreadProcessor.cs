using UnityEngine;
using UdpNetworking.Client;
using System.Collections.Generic;

public class MainThreadProcessor : MonoBehaviour, IThreadProcessor {

    public Queue<AsyncThreadCallback> Queue { get; set; }
    public IThreadProcessor InterfaceInstance { get { return Instance; } }
    public static readonly MainThreadProcessor Instance;
    
    static MainThreadProcessor() {
        var name = typeof(MainThreadProcessor).Name;
        Instance = new GameObject(name).AddComponent<MainThreadProcessor>();
        Instance.Queue = new Queue<AsyncThreadCallback>();
    }

    /// <summary>
    /// Adds a message to the queue
    /// </summary>
    /// <param name="response">Message to be added.</param>
    public void Enqueue(AsyncThreadCallback response) {
        Queue.Enqueue(response);
    }

    void Update() {
        // If we have messages waiting in the queue.
        if (Queue.Count <= 0) return;
        // Process and move onto next.
        var next = Queue.Peek();
        next.RequestCallback(next.RecievedBytes);
        Queue.Dequeue();
    }

}
