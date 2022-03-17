
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
    public LineRenderer lineRenderer;
    
    //Positions to follow
    public Array PositionArray;
    public Vector3[] FinalPositionArray;
    public Vector3[] PositionTemp;
    
    //object to move along the line
    public Transform test;
    public int increment;

    private void Start()
    {
        increment = 0;
        //this.OnDrawGizmos();
        Vector3[] position = BezierCurveCreation();
        //configuration of the line renderer
        
        lineRenderer = GetComponent<LineRenderer>();

        float width = 0.3f;
        
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        lineRenderer.loop = false;
        lineRenderer.positionCount = FinalPositionArray.Length;
        
        print("START");
        for (int i = 0; i < position.Length; i++)
        {

            if ((Vector3) position.GetValue(i) != Vector3.zero)
            {
                print(i + " : " + position.GetValue(i));
            }
        }
        print("------------------------------------------------------------");
        print("UPDATE");
        //Draw this line segment with line renderer
        lineRenderer.SetPositions(FinalPositionArray);
    }
    
    //Test pour valider que les positions de la ligne fonctionnent (oui!)
    private void Update()
    { 
        //throw new NotImplementedException();
        StartCoroutine("Reset");
        
    }
    
    IEnumerator Reset() {
        if (increment < FinalPositionArray.Length)
        {
            test.position = FinalPositionArray[increment];
            yield return new WaitForSeconds(4);
            print(test.position);
            increment++;
        }
    } 

    public void OnDrawGizmos()
    {
        Vector3[] position = BezierCurveCreation();
        //mis en commentaire pour faciliter la lecture des erreurs
        /*for (int i = 0; i < position.Length; i++)
        {
            print(i + " : " + position.GetValue(i));
        }*/
    }
    
    public Vector3[] BezierCurveCreation()
    {
        //PositionArray = Array.CreateInstance(typeof(Vector3), 1000);

        PositionTemp = new Vector3[1000];
        
        BezierCurve curve;
        
        for (int i = 0; i < controlPoints.Length-3; i = i+3)
        {
            curve = new BezierCurve(controlPoints[i], 
                controlPoints[i+3], 
                controlPoints[i+1], 
                controlPoints[i+2],
                lineRenderer);
            //curve.OnDrawGizmos();
            curve.MakeCurve();
            
            for (int j = 0; j < curve.loops; j++)
            {
                
                PositionTemp[j + 4*i] = curve.Positions[j];
            } 
        }

        //FinalPositionArray = new Vector3[PositionArray.Length];
        //PositionArray.CopyTo(FinalPositionArray, 0);

        int l = 0;
        for (int i = 0; i < PositionTemp.Length; i++)
        {
            if (PositionTemp[i] != Vector3.zero) //problème si passe par (0,0,0)
            {
                l++;
            }
        }

        FinalPositionArray = new Vector3[l];
        for (int i = 0; i < FinalPositionArray.Length; i++)
        {
            FinalPositionArray[i] = PositionTemp[i];
        }
        
        return FinalPositionArray;
    }
    
}
