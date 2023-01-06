import UnityEngine as ue

objects = ue.Object.FindObjectsOfType(ue.GameObject)
for gameObjects in objects: 
    ue.Debug.Log(gameObjects.name)