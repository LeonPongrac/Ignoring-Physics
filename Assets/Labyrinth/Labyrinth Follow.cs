using System.Collections;
using UnityEngine;

public class LabyrinthFollow : MonoBehaviour
{
    public Transform playerCamera; // Referenca na kameru igraca
    public float teleportRadius = 10f; // Radijus za teleportiranje objekta
    public LayerMask obstacleLayer; // Sloj za provjeru prepreka
    public GameObject platformObject; // Specificni objekt platforme
    public float teleportCooldown = 5f; // Vrijeme izmeðu teleportacija
    public float angleThreshold = 80f; // Prag kuta za detekciju pogleda
    public float minTeleportDistance = 2f; // Minimalna udaljenost za teleportiranje

    private float lastTeleportTime = 0f;
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Time.time - lastTeleportTime >= teleportCooldown && !IsPlayerLookingAtObject())
        {
            TryTeleport();
        }
    }

    private bool IsPlayerLookingAtObject()
    {
        // Izracunaj smjer od objekta prema igraèu
        Vector3 directionToTarget = transform.position - playerCamera.position;

        // Izraèunaj kut izmedu smjera u kojem igrac gleda i smjera prema objektu
        float angle = Vector3.Angle(playerCamera.forward, directionToTarget);

        // Provjera udaljenosti izmeðu objekta i igraca
        float distanceToPlayer = directionToTarget.magnitude;

        // Ako je igrac preblizu objektu, smatra se da ga "gleda"
        if (distanceToPlayer <= minTeleportDistance)
        {
            return true;
        }

        // Ako igrac gleda objekt unutar praga kuta, vrati true
        return angle <= angleThreshold;
    }

    private void TryTeleport()
    {
        Vector3 randomPosition = Vector3.zero;
        bool validPositionFound = false;

        for (int i = 0; i < 10; i++) // Pokušaj do 10 puta
        {
            randomPosition = GetRandomPositionAroundPlayer();
            if (IsPositionAbovePlatform(randomPosition) && !IsPositionInObstacle(randomPosition))
            {
                validPositionFound = true;
                break;
            }
        }

        if (validPositionFound)
        {
            transform.position = randomPosition;
            lastTeleportTime = Time.time;
        }
    }

    private Vector3 GetRandomPositionAroundPlayer()
    {
        Vector2 randomCircle = Random.insideUnitCircle * teleportRadius;
        Vector3 randomPosition = new Vector3(
            playerCamera.position.x + randomCircle.x,
            transform.position.y, // Zakljucaj na Y osi
            playerCamera.position.z + randomCircle.y
        );

        // Provjeri smjer u koji igraè ne gleda
        Vector3 directionToPosition = (randomPosition - playerCamera.position).normalized;
        float dotProduct = Vector3.Dot(playerCamera.forward, directionToPosition);

        // Ako je u smjeru gdje igrac ne gleda
        if (dotProduct < 0)
        {
            return randomPosition;
        }

        // Rekurzija ako pozicija nije valjana
        return GetRandomPositionAroundPlayer();
    }

    private bool IsPositionAbovePlatform(Vector3 position)
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(position.x, position.y + 1f, position.z), Vector3.down, out hit, 2f))
        {
            // Provjeri da li je pogodak na specificnom objektu platforme
            return hit.collider.gameObject == platformObject;
        }
        return false;
    }

    private bool IsPositionInObstacle(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, 0.5f, obstacleLayer);
        return hitColliders.Length > 0;
    }

    private void OnDrawGizmos()
    {
        if (playerCamera != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(playerCamera.position, teleportRadius);
        }
    }
}
