using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Common.Extensions;

public static class CollectionExtensions
{
    public static ICollection<Grade> Order(this ICollection<Grade> grades)
    {
        var nextGrades = grades.ToDictionary(grade => grade.Id, grade => grade.NextGrade);
        var sortedGrades = new List<Grade>();
        var currentGrade = grades.FirstOrDefault(grade => grade.PrevGradeId == null);
        while (currentGrade is not null)
        {
            sortedGrades.Add(currentGrade);
            currentGrade = nextGrades[currentGrade.Id];
        }

        return sortedGrades;
    }
}
