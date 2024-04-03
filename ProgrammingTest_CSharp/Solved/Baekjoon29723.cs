
namespace ProgrammingTest_CSharp;

/*https://www.acmicpc.net/problem/29723*/
public class Baekjoon29723
{
    public static void Main()
    {
        var givenStr = Console.ReadLine()?.Split(' '); if(givenStr == null) return;
        
        var coursesTakenCount     = int.Parse(givenStr[0]);
        var coursesRequiredCount  = int.Parse(givenStr[1]);
        var publicCoursesCount    = int.Parse(givenStr[2]);
        var ambiguousCoursesCount = coursesTakenCount - publicCoursesCount;

        var coursesTaken          = new CourseScore[coursesTakenCount];
        var publicCourses         = new CourseScore[publicCoursesCount];
        var ambiguousCourses      = new List<CourseScore>(ambiguousCoursesCount);

        var minScore = 0;
        var maxScore = 0;
        
        
        for(var i=0; i<coursesTakenCount; i++) {
            var str = Console.ReadLine()?.Split(' '); if(str == null) return;
            coursesTaken[i]   = new CourseScore(str[0], int.Parse(str[1]));
        }
        
        for(var i=0; i<publicCoursesCount; i++) {
            var str = Console.ReadLine(); if(str == null) return;
            // publicCourses[i] = coursesTaken.First(cs => cs.CourseName.Equals(str));  // 백준코딩 C#에서는 LINQ가 지원되지 않는다.
            publicCourses[i] = GetIdenticalCourse(coursesTaken, str);
        }
        
        for(var i=0; i<coursesTakenCount; i++) {
            var courseTaken      = coursesTaken[i];
            
            // if(!publicCourses.Contains(courseTaken)) {
            if(!Contains(publicCourses, courseTaken)) {
                ambiguousCourses.Add(courseTaken);
            }
        }

        foreach(var publicCourse in publicCourses) {
            minScore += publicCourse.Score;
            maxScore += publicCourse.Score;
        }

        ambiguousCourses.Sort();

        var requiredAmbCourseCount = coursesRequiredCount - publicCoursesCount;
        for(var i=0; i<requiredAmbCourseCount; i++)
            minScore += ambiguousCourses[i].Score;
        for(var i=ambiguousCoursesCount-1; i>=ambiguousCoursesCount-requiredAmbCourseCount; i--)
            maxScore += ambiguousCourses[i].Score;
        
        Console.WriteLine($"{minScore} {maxScore}");
    }

    
    private readonly struct CourseScore : IComparable<CourseScore>
    {
        public string CourseName { get; }
        public int    Score      { get; }
        
        public CourseScore(string name, int score)
        {
            CourseName = name;
            Score      = score;
        }
        
        public override string ToString() { return $"{CourseName}: {Score}"; }
        public int CompareTo(CourseScore other) { return Score.CompareTo(other.Score); }
    }
    
    private static CourseScore GetIdenticalCourse(CourseScore[] coursesTaken, string courseName)
    {
        foreach(var course in coursesTaken) {
            if(course.CourseName.Equals(courseName))
                return course;
        }

        throw new Exception();
    }
    
    private static bool Contains(CourseScore[] courseArr, CourseScore courseToFind)
    {
        foreach(var course in courseArr) {
            if(course.CourseName.Equals(courseToFind.CourseName) && course.Score == courseToFind.Score)
                return true;
        }

        return false;
    }
}
