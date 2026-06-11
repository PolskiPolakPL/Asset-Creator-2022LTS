using System.Collections.Generic;
using UnityEngine;

public enum FormationTypes
{
    LINE,
    SQUARE,
    CIRCLE,
    TRIANGLE
}

public class SquadFormation
{
    public static List<Vector3> GetPositions(FormationTypes formation, int amount, float distance = 1)
    {
        if(amount<1)
            return null;
        List<Vector3> positions = new List<Vector3>();

        // add positions
        switch (formation)
        {
            case FormationTypes.LINE:
                positions = GetLinePositions(amount, distance);
                break;

            case FormationTypes.SQUARE:
                positions = GetSquarePositions(amount, distance);
                break;

            case FormationTypes.CIRCLE:
                positions = GetCirclePositions(amount, distance);
                break;

            case FormationTypes.TRIANGLE:
                positions = GetTrianglePositions(amount, distance);
                break;
        }

        return positions;

    }

    static List<Vector3> GetCirclePositions(int count, float distance)
    {
        List<Vector3> positions = new List<Vector3>();

        positions.Add(Vector3.zero);
        count--;

        float x, z, angle;
        for (int i=0; i<count; i++)
        {
            angle = 2 * Mathf.PI * (i) / (count);
            x = Mathf.Cos(angle) * distance;
            z = Mathf.Sin(angle) * distance;
            positions.Add(new Vector3(x, 0, z));
        }

        return positions;
    }

    static List<Vector3> GetLinePositions(int count, float distance)
    {
        List<Vector3> positions = new List<Vector3>();

        float x = 0;
        for (int i = 0; i < count; i++)
        {
            if (i == 0)
            {
                positions.Add(Vector3.zero);
                continue;
            }

            if (i%2==0)
                x = -(i/2)*distance;
            else
                x = ((i+1)/2)*distance;

            positions.Add(new Vector3(x, 0, 0));
        }

        return positions;
    }

    static List<Vector3> GetTrianglePositions(int count, float distance)
    {
        List<Vector3> positions = new List<Vector3>();

        positions.Add(Vector3.zero);

        for (int i = 1; i < count; i++)
        {
            int side = (i % 2 == 0) ? 1 : -1;   // lewo/prawo
            int row = (i + 1) / 2;              // g³êbokoœæ

            float x = side * row * distance;
            float z = -row * distance;

            positions.Add(new Vector3(x, 0, z));
        }

        return positions;
    }

    static List<Vector3> GetSquarePositions(int count, float distance)
    {
        List<Vector3> positions = new List<Vector3>();

        int width = Mathf.CeilToInt(Mathf.Sqrt(count));

        int generated = 0;
        int row = 0;

        while (generated < count)
        {
            int remaining = count - generated;
            int rowCount = Mathf.Min(width, remaining);

            float rowWidth = (rowCount - 1) * distance;
            float startX = -rowWidth * 0.5f;

            for (int col = 0; col < rowCount; col++)
            {
                float x = startX + col * distance;
                float z = row * distance;

                positions.Add(new Vector3(x, 0, z));

                generated++;

                if (generated >= count)
                    break;
            }

            row++;
        }

        return positions;
    }
}
