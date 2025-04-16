//using System.Collections;
//using System.Collections.Generic;

//using UnityEngine;

//public class BezierCurve : MonoBehaviour
//{
//    public Transform P0, P1, P2, P3; // Points de contr�le
//    public LineRenderer curveRenderer; // LineRenderer pour la courbe
//    public LineRenderer polygonRenderer; // LineRenderer pour le polygone
//    public int resolution = 50; // Nombre de points de la courbe
//    private int selectedPoint = 0; // Point actuellement s�lectionn�

//    void Update()
//    {
//        DrawPolygon();
//        DrawBezierCurve();
//        HandleInput();
//    }

//    void DrawPolygon()
//    {
//        // Afficher le polygone reliant les points de contr�le
//        Vector3[] points = new Vector3[] { P0.position, P1.position, P2.position, P3.position };
//        polygonRenderer.positionCount = points.Length;
//        polygonRenderer.SetPositions(points);
//    }

//    void DrawBezierCurve()
//    {
//        // Calculer et dessiner la courbe de B�zier
//        Vector3[] curvePoints = new Vector3[resolution];
//        for (int i = 0; i < resolution; i++)
//        {
//            float t = i / (float)(resolution - 1);
//            curvePoints[i] = CalculateBezierPoint(t, P0.position, P1.position, P2.position, P3.position);
//        }
//        curveRenderer.positionCount = curvePoints.Length;
//        curveRenderer.SetPositions(curvePoints);
//    }

//    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
//    {
//        // Formule de B�zier cubique
//        float u = 1 - t;
//        return u * u * u * p0 +
//               3 * u * u * t * p1 +
//               3 * u * t * t * p2 +
//               t * t * t * p3;
//    }

//    void HandleInput()
//    {
//        // Changer le point de contr�le s�lectionn�
//        if (Input.GetKeyDown(KeyCode.Alpha0)) selectedPoint = 0;
//        if (Input.GetKeyDown(KeyCode.Alpha1)) selectedPoint = 1;
//        if (Input.GetKeyDown(KeyCode.Alpha2)) selectedPoint = 2;
//        if (Input.GetKeyDown(KeyCode.Alpha3)) selectedPoint = 3;

//        // D�placer le point s�lectionn�
//        Vector3 movement = Vector3.zero;
//        if (Input.GetKey(KeyCode.Z)) movement += Vector3.up * Time.deltaTime;
//        if (Input.GetKey(KeyCode.S)) movement += Vector3.down * Time.deltaTime;
//        if (Input.GetKey(KeyCode.Q)) movement += Vector3.left * Time.deltaTime;
//        if (Input.GetKey(KeyCode.D)) movement += Vector3.right * Time.deltaTime;

//        switch (selectedPoint)
//        {
//            case 0: P0.position += movement; break;
//            case 1: P1.position += movement; break;
//            case 2: P2.position += movement; break;
//            case 3: P3.position += movement; break;
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public Transform P0, P1, P2, P3; // Points de contr�le
    public LineRenderer curveRenderer; // LineRenderer pour la courbe
    public LineRenderer polygonRenderer; // LineRenderer pour le polygone
    public int resolution = 50; // Nombre de points de la courbe
    private int selectedPoint = 0; // Point actuellement s�lectionn�
    private Renderer[] pointRenderers; // Table pour les Renderers des points

    void Start()
    {
        // Initialisation des Renderers des points
        pointRenderers = new Renderer[4];
        pointRenderers[0] = P0.GetComponent<Renderer>();
        pointRenderers[1] = P1.GetComponent<Renderer>();
        pointRenderers[2] = P2.GetComponent<Renderer>();
        pointRenderers[3] = P3.GetComponent<Renderer>();

        // S'assurer qu'un Renderer existe sur chaque point
        for (int i = 0; i < pointRenderers.Length; i++)
        {
            if (pointRenderers[i] == null)
            {
                Debug.LogError($"Le point P{i} n'a pas de Renderer attach� !");
            }
        }

        UpdatePointColors(); // Mise � jour initiale des couleurs
    }

    void Update()
    {
        DrawPolygon();
        DrawBezierCurve();
        HandleInput();
    }

    void DrawPolygon()
    {
        // Afficher le polygone reliant les points de contr�le
        Vector3[] points = new Vector3[] { P0.position, P1.position, P2.position, P3.position };
        polygonRenderer.positionCount = points.Length;
        polygonRenderer.SetPositions(points);
    }

    void DrawBezierCurve()
    {
        // Calculer et dessiner la courbe de B�zier
        Vector3[] curvePoints = new Vector3[resolution];
        for (int i = 0; i < resolution; i++)
        {
            float t = i / (float)(resolution - 1);
            curvePoints[i] = CalculateBezierPoint(t, P0.position, P1.position, P2.position, P3.position);
        }
        curveRenderer.positionCount = curvePoints.Length;
        curveRenderer.SetPositions(curvePoints);
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        // Formule de B�zier cubique
        float u = 1 - t;
        return u * u * u * p0 +
               3 * u * u * t * p1 +
               3 * u * t * t * p2 +
               t * t * t * p3;
    }

    void HandleInput()
    {
        // Changer le point de contr�le s�lectionn�
        if (Input.GetKeyDown(KeyCode.Alpha0)) selectedPoint = 0;
        if (Input.GetKeyDown(KeyCode.Alpha1)) selectedPoint = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) selectedPoint = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3)) selectedPoint = 3;

        UpdatePointColors(); // Mettre � jour les couleurs des points

        // D�placer le point s�lectionn�
        Vector3 movement = Vector3.zero;
        if (Input.GetKey(KeyCode.Z)) movement += Vector3.up * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) movement += Vector3.down * Time.deltaTime;
        if (Input.GetKey(KeyCode.Q)) movement += Vector3.left * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) movement += Vector3.right * Time.deltaTime;

        switch (selectedPoint)
        {
            case 0: P0.position += movement; break;
            case 1: P1.position += movement; break;
            case 2: P2.position += movement; break;
            case 3: P3.position += movement; break;
        }
    }

    void UpdatePointColors()
    {
        // Modifier la couleur des points en fonction du point s�lectionn�
        for (int i = 0; i < pointRenderers.Length; i++)
        {
            if (pointRenderers[i] != null)
            {
                pointRenderers[i].material.color = (i == selectedPoint) ? Color.blue : Color.white;
            }
        }
    }
}
