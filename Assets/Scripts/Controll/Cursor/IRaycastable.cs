using UnityEngine;

public interface IRaycastable
{
    CursorType GetCursorType();
    bool HandleRaycast(GameObject entity);
    void CancelRaycast();
}