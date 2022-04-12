
//BONNE VERSION DE PROJET

/*
 * Génère des courbes de Bézier successives en
 * subdivisant les points en sous-groupes de 4 points de repères.
 * Note: Il n'y a pas de nombre de points maximal.
 * Une courbe apparaît lorsque tous les points dont elle a besoin sont déterminés.
 * Note: Si on enlève un point au milieu de la série de points, toutes les courbes après ce point
 * disparaissent.
 */

using System;
using Unity.Collections;
using UnityEngine;
using Object = System.Object;
using System.Collections;
using System.Collections.Generic;

public class BezierCurveCreator : MonoBehaviour
{
    //Points to be used
    public Transform[] controlPoints;

    //Line renderer to draw the curve
    public LineRenderer[] lineRenderer;
    
    private void Start()
    {
        BezierCurve curve;
        float width = 0.3f;

        //S'ASSURER QUE LE NOMBRE DE LIGNES DANS LINERENDERER ET LE NOMBRE DE POINTS DANS CONTROLPOINTS SONT COMPATIBLES
        for (int i = 0; i < lineRenderer.Length; i++)
        {
            curve = new BezierCurve(controlPoints[3*i], 
                controlPoints[3*i + 3], 
                controlPoints[3*i + 1], 
                controlPoints[3*i + 2]);
            Vector3[] positions = curve.MakeCurve();
        
            lineRenderer[i].startColor = Color.black;
            lineRenderer[i].endColor = Color.black;
            lineRenderer[i].startWidth = width;
            lineRenderer[i].endWidth = width;
            lineRenderer[i].loop = false;
            lineRenderer[i].positionCount = positions.Length;
            lineRenderer[i].SetPositions(positions);
        }
        
        /*curve = new BezierCurve(controlPoints[3], 
            controlPoints[6], 
            controlPoints[4], 
            controlPoints[5]);
        Vector3[] positions2 = curve.MakeCurve();
        
        lineRenderer[1].startColor = Color.black;
        lineRenderer[1].endColor = Color.black;
        lineRenderer[1].startWidth = width;
        lineRenderer[1].endWidth = width;
        lineRenderer[1].loop = false;
        lineRenderer[1].positionCount = positions2.Length;
        lineRenderer[1].SetPositions(positions2);
        
        curve = new BezierCurve(controlPoints[6], 
            controlPoints[9], 
            controlPoints[7], 
            controlPoints[8]);
        Vector3[] positions3 = curve.MakeCurve();
        
        lineRenderer[2].startColor = Color.black;
        lineRenderer[2].endColor = Color.black;
        lineRenderer[2].startWidth = width;
        lineRenderer[2].endWidth = width;
        lineRenderer[2].loop = false;
        lineRenderer[2].positionCount = positions3.Length;
        lineRenderer[2].SetPositions(positions3);*/
    }
}
