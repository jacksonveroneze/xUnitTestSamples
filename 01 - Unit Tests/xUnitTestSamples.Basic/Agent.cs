using System;
using System.Collections.Generic;

namespace xUnitTestSamples.Basic
{
    public class Person
    {
        public string Name { get; protected set; }
        public string Nickname { get; set; }
    }

    public class Agent : Person
    {
        public double Salary { get; private set; }
        public ProfessionalLevel ProfessionalLevel { get; private set; }
        public IList<string> Skills { get; private set; }

        public Agent(string name, double salary)
        {
            Name = string.IsNullOrEmpty(name) ? "Fulano" : name;
            DefineSalary(salary);
            DefineSkills();
        }

        public void DefineSalary(double salary)
        {
            if (salary < 500) throw new Exception("Salário inferior ao permitido");

            Salary = salary;
            if (salary < 2000) ProfessionalLevel = ProfessionalLevel.Junior;
            else if (salary >= 2000 && salary < 8000) ProfessionalLevel = ProfessionalLevel.Pleno;
            else if (salary >= 8000) ProfessionalLevel = ProfessionalLevel.Senior;
        }

        private void DefineSkills()
        {
            var skillsBasics = new List<string>()
            {
                "Lógica de Programação",
                "OOP"
            };

            Skills = skillsBasics;

            switch (ProfessionalLevel)
            {
                case ProfessionalLevel.Pleno:
                    Skills.Add("Testes");
                    break;
                case ProfessionalLevel.Senior:
                    Skills.Add("Testes");
                    Skills.Add("Microservices");
                    break;
            }
        }
    }

    public enum ProfessionalLevel
    {
        Junior,
        Pleno,
        Senior
    }

    public class AgentFactory
    {
        public static Agent Create(string name, double salary)
        {
            return new Agent(name, salary);
        }
    }
}