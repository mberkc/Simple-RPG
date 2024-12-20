using Data;
using UnityEngine;

namespace UI.Controllers
{
    public abstract class SceneController: MonoBehaviour
    { 
        public abstract void Initialize(GameState gameState, EntityService entityService);
    }
}