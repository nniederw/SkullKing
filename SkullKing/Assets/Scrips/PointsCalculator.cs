using System;
using System.Collections.Generic;
using System.Linq;

public static class PointsCalculator
{
    private const int SkullkingBonusV1 = 5;
    private const int SkullkingBonusV2 = 4;
    private const int PirateBonus = 3;
    private const int MermaidBonus = 2;
    private const int LootBonus = 2;
    private const int Black14Bonus = 2;
    private const int Red14Bonus = 1;
    private const int Yellow14Bonus = 1;
    private const int Blue14Bonus = 1;
    private const int PiratesCount = 6;
    private const int MermaidCount = 2;
    private const int LootCount = 2;
    public static PossiblePoints CalcPossiblePoints(int tricks, int players, int startCards)
    {
        List<int> points = new List<int>();
        if (tricks == 0)
        {
            points.Add(-startCards);
            points.Add(startCards);
            if (players > 2)
            {
                points.Add(startCards + 2); // todo make it dynamic with loot count
                points.Add(startCards + 4);
            }
            else if (players == 2)
            {
                if (startCards == 1)
                {
                    points.Add(startCards + 1);
                    points.Add(startCards + 2);
                }
                else
                {
                    points.Add(startCards + 1);
                    points.Add(startCards + 2);
                    points.Add(startCards + 4);
                }
            }
        }
        else if (startCards == 0)
        {
            points.Add(-tricks);
        }
        else
        {
            int minpoints = Math.Min(-tricks, tricks - startCards);
            foreach (var i in minpoints.RangeTo(-1))
            {
                points.Add(i);
            }
            int minPositive = tricks * 2;
            int maxbonus1 = 0; // with Skull King
            maxbonus1 = Math.Min(PiratesCount, players - 1) * PirateBonus;
            List<int> bonuses = new List<int>();
            for (int i = 0; i < MermaidCount; i++)
            {
                bonuses.Add(MermaidBonus);
            }
            for (int i = 0; i < LootCount; i++)
            {
                bonuses.Add(LootBonus);
            }
            bonuses.Add(Black14Bonus);
            bonuses.Add(Red14Bonus);
            bonuses.Add(Yellow14Bonus);
            bonuses.Add(Blue14Bonus);
            bonuses.Sort((a, b) => a.CompareTo(b));
            var leftBonuses = bonuses.ToArray().ToList(); // list copy
            int leftTricks = tricks - 1;
            while (leftTricks > 0)
            {
                for (int i = 0; i < players; i++)
                {
                    if (!leftBonuses.Any()) break;
                    maxbonus1 += leftBonuses.Last();
                    leftBonuses.RemoveAt(leftBonuses.Count - 1);
                }
            }

            int maxbonus2 = 0; // without Skull King
            bonuses.Remove(MermaidBonus);
            bonuses.Add(Math.Max(SkullkingBonusV1, SkullkingBonusV2));
            leftBonuses = bonuses.ToArray().ToList(); // list copy
            leftTricks = tricks - 1;
            while (leftTricks > 0)
            {
                for (int i = 0; i < players; i++)
                {
                    if (!leftBonuses.Any()) break;
                    maxbonus2 += leftBonuses.Last();
                    leftBonuses.RemoveAt(leftBonuses.Count - 1);
                }
            }
            int maxbonus = Math.Min(maxbonus1, maxbonus2);
            foreach (var i in minPositive.RangeTo(minPositive + maxbonus))
            {
                points.Add(i);
            }
        }
        return new PossiblePoints(points);
    }
}
public struct PossiblePoints
{
    public int[] SortedNegativePoints; // highest number first
    public int[] SortedPositivePoints; // lowest number first
    public PossiblePoints(IEnumerable<int> points)
    {
        SortedSet<int> ppoints = new SortedSet<int>();
        SortedSet<int> npoints = new SortedSet<int>();
        foreach (int i in points)
        {
            if (i < 0)
            {
                npoints.Add(i);
                continue;
            }
            ppoints.Add(i);
        }
        SortedNegativePoints = npoints.Reverse().ToArray();
        SortedPositivePoints = ppoints.ToArray();
        CheckCorrectness(); // Todo remove as it should be redundant
    }
    private void CheckCorrectness()
    {
        int last = 0;
        for (int i = 0; i < SortedNegativePoints.Length; i++)
        {
            if (SortedNegativePoints[i] >= last) throw new Exception($"The {nameof(SortedNegativePoints)} haven't been properly sorted in {nameof(PossiblePoints)}");
            last = SortedNegativePoints[i];
        }
        last = -1;
        for (int i = 0; i < SortedPositivePoints.Length; i++)
        {
            if (SortedPositivePoints[i] <= last) throw new Exception($"The {nameof(SortedPositivePoints)} haven't been properly sorted in {nameof(PossiblePoints)}");
            last = SortedPositivePoints[i];
        }
    }
}