using UnityEngine;
// Either make observer interface or have it use monobehaviour 
namespace CzernyStudio.Utilities {
    public interface IObserver {
        IObserver Next { get; set; }
        void OnNotify(MessageBase entity);
    }
}
