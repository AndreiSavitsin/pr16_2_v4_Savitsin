using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace pr16_v1_Savitsin
{
    public class Student
    {
        string name; //Имя
        string group; //Группа
        string speciality; //Специальность
        string subject; //Предметы
        int grade; //Оценка

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Group
        {
            get { return group; }
            set { group = value; }
        }

        public string Speciality
        {
            get { return speciality; }
            set { speciality = value; }
        }

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        public int Grade
        {
            get { return grade; }
            set { grade = value; }
        }

        public Student(string name, string group, string speciality, string subject, int grade)
        {
            this.name = name;
            this.group = group;
            this.speciality = speciality;
            this.subject = subject;
            this.grade = grade;
        }
        public override string ToString()
        {
            return $"{name} {group} {speciality} {subject} Оценка: {grade}";
        }
    }
}
