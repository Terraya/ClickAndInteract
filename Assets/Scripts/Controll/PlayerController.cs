using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(PlayerInteraction))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] CursorMapping[] cursorMappings = null;
    [SerializeField] float raycastRadius = 1f;
    
    private Mover _mover;
    private PlayerInteraction _playerInteraction;
    private Camera _camera;

    [Serializable]
    struct CursorMapping
    {
        public CursorType type;
        public Texture2D texture;
        public Vector2 hotspot;
    }

    private void Awake()
    {
        gameObject.tag = "Player";
        _mover = GetComponent<Mover>();
        _playerInteraction = GetComponent<PlayerInteraction>();
        _camera = Camera.main;
    }

    private void Update()
    {
        if (IsInteractingWithUI()) return;
        if (InteractWithComponent()) return;
        if (InteractWithMovement()) return;
        
        SetCursor(CursorType.None);
    }

    private bool IsInteractingWithUI()
    {
        //Logic
        return false;
    }

    private bool InteractWithMovement()
    {
        Vector3 target;
        var hasHit = RayCastNavMesh(out target);
        if (!hasHit) return false;
        if (!_mover.CanMoveTo(target)) return false;
        if (Input.GetMouseButton(0))
        {
            _mover.StartMoveAction(target, 1f);
            return true;
        }

        return false;
    }

    private bool RayCastNavMesh(out Vector3 target)
    {
        target = new Vector3();
        
        RaycastHit hit;
        var hasHit = Physics.Raycast(GetMouseRay(), out hit);
        if (!hasHit) return false;

        NavMeshHit navMeshHit;
        var hasCastToNavMesh = NavMesh.SamplePosition(
            hit.point, out navMeshHit, 1f, NavMesh.AllAreas);
        if (!hasCastToNavMesh) return false;

        target = navMeshHit.position;

        return true;
    }

    private bool InteractWithComponent()
    {
        var hits = RaycastAllSorted();
        foreach (RaycastHit hit in hits)
        {
            var raycastables = hit.transform.GetComponents<IRaycastable>();
            foreach (var raycastable in raycastables)
            {
                SetCursor(raycastable.GetCursorType());
                _playerInteraction.InteractWithTarget(raycastable);
                return true;
            }
        }
        
        return false;
    }

    private RaycastHit[] RaycastAllSorted()
    {
        RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(), raycastRadius);
        float[] distances = new float[hits.Length];
        for (int i = 0; i < hits.Length; i++)
        {
            distances[i] = hits[i].distance;
        }

        Array.Sort(distances, hits);
        return hits;
    }

    private void SetCursor(CursorType type)
    {
        CursorMapping mapping = GetCursorMapping(type);
        Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
    }

    private CursorMapping GetCursorMapping(CursorType type)
    {
        foreach (CursorMapping mapping in cursorMappings)
        {
            if (mapping.type == type)
            {
                return mapping;
            }
        }

        return cursorMappings[0];
    }

    private Ray GetMouseRay()
        => _camera.ScreenPointToRay(Input.mousePosition);

}