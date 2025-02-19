using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class calculateFiringSolution
{

    public Nullable<Vector3> Calculate(Vector3 start, Vector3 end, float muzzleV, Vector3 gravity)
    {
        /*(start: Vector,
        end: Vector,
        muzzleV: float,
        gravity: Vector) -> Vector:*/

        //Calls other function to do the math
        Nullable<float> ttt = GetTimeToTarget(start, end, muzzleV, gravity);

        if (!ttt.HasValue)
        {
            return null;
        }

        //Vector3 delta = start - end;
        Vector3 delta = end - start;

        //return the firing vector
        return ((delta * 2) - (gravity * (ttt.Value * ttt.Value)))/(2 * muzzleV * ttt.Value);
        //return (delta * 2 - gravity * (tttttt)) / (2 * muzzleV * ttt)
    }

    //Funtcion to get time to target.
    public Nullable<float> GetTimeToTarget(Vector3 start, Vector3 end, float muzzleV, Vector3 gravity)
    {
        //Calculate the vector from the target back to the start. 
        Vector3 delta = start-end;

        //Calculate the real-valued a,b,c coefficients of a
        //conventional quadratic equation.
        float a = gravity.magnitude * gravity.magnitude; //gravity.squareMagnitude()
        float b = -4 * (Vector3.Dot(gravity, delta) + muzzleV *muzzleV);//(dotProduct (gravity, delta) + muzzleV muzzleV);
        float c = 4 * delta.magnitude * delta.magnitude; ////delta.squareMagnitude()

        // Check for no real solutions.
        float b2minus4ac = (b * b)-(4 * a * c);
        if  (b2minus4ac < 0) {
            return null;
        }

        // Find the candidate times.
        float time0 = Mathf.Sqrt((-b + Mathf.Sqrt(b2minus4ac)) / (2 * a));
        float time1= Mathf.Sqrt((-b- Mathf.Sqrt(b2minus4ac)) / (2 * a));

        // Find the time to target.
		Nullable<float> ttt;//taken from example, must be nullable

        if (time0 < 0)
        {
            if (time1 < 0) {
            // We have no valid times.
                return null;
            }
            else {
                ttt = time1;
            }
        }
            
        else
        {
            if (time1 < 0) {
                ttt = time0;
            }
            else {
                ttt = Mathf.Min(time0, time1);
            } 
        }
    //Returned the firing vector in other function.
    //This just returns time to target
    return ttt;
    //return (delta 2 - gravity (tttttt)) / (2 muzzleV * ttt)
    }
}