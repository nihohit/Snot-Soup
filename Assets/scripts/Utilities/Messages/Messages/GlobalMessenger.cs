//https://refactoring.guru/design-patterns/observer/csharp/example reference to observer pattern desing
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This Subject class is working, but it is not without floaw.
/// One of the flaws is that the subscription is per a subscriber - observer, and not per entity - a message which is being sent
/// Thus when an observer has more that one entity it is enough for the observer to subscribe one. this means that:
/// 1. for more than one entity a code "runs" for "nothing" both on observer.Awake and on observer.OnDestroy
/// 2. if for some reason, the observer doesn't need to subscribe to an entity, but left overs of that entity are kept within the observer.OnNotify method, this entity will be all set to go if Notify with this entity is being called for another observer. 
/// 3. Entity is not such a good name because of ECS, and should be renamed to something else, like "message" perhaps.
/// </summary>
namespace CzernyStudio.Utilities {
    //This class is the Subject class
    public class GlobalMessenger
    {
        public static GlobalMessenger Instance {
            get {
                if (instance == null) {
                    instance = new GlobalMessenger();
                }
                return instance;
            }
        }

        private static GlobalMessenger instance = new GlobalMessenger();
        
        // Use only one observer here to start create a linked list
        private IObserver head = null;

        // TODO need to add observer with a specific entity, perhaps have a dictionary with a message and the observer to observe that message
        public void AddObserver(IObserver observer) {
            if (observer == head) {
                return;
            }
            observer.Next = head;
            head = observer;
        }

        public void RemoveObserver(IObserver observer) {
            if (head == observer) {
                head = observer.Next;
                observer.Next = null;
            }

            var current = head;

            while (current != null) {
                if (current.Next == observer) {
                    current.Next = observer.Next;
                    observer.Next = null;
                    return;
                }
                current = current.Next;
            }
        }     
        
        //TODO when notifying notify an observer with the message so it will be Notify(MessageBase entity, Observer observer)
        public void Notify(MessageBase entity){
            var observer = head;
            while (observer != null) {
                observer.OnNotify(entity);
                observer = observer.Next;
            }
        }
    }
}
