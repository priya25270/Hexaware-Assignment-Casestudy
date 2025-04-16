using System.Collections.Generic;

namespace SISApp
{
    internal class StudentNameComparer : IComparer<Student>
    {
        public int Compare(Student x, Student y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}