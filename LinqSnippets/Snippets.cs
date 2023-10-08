using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqSnippets
{

    public class Snippets
    {
        public static void BasicLinQ()
        {
            string[] cars =
            {
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A5",
                "Fiat punto",
                "Seat Ibiza",
                "Seat Leon"
            };

            // 1 SELECT *

            var carList = from car in cars select car;

            foreach ( var car in carList )
            {
                Console.WriteLine(car);
            }

            // 2 SELECT WHERE

            var audiList = from car in cars where car.Contains("Audi") select car;

            foreach ( var audi in audiList)
            {
                Console.WriteLine(audi);
            }

        }

        public static void LinqNumbers() 
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9};

            // Each number multiplied by 3
            // take all number but 9
            // order numbers by ascending 

            var processedNumberList = numbers
                .Select(num => num * 3)
                .Where(num => num != 9)
                .OrderBy(num => num);
        }

       public static void SearchExamples()
       {
            List<string> textList = new List<string>
            {
                "a",
                "bx",
                "c",
                "d",
                "e",
                "cj",
                "f",
                "c"
            };

            // 1 first of all elements 

            var first = textList.First();

            // 2 first element that is "c"

            var cText = textList.First(text => text.Equals("c"));

            // 3 first element that contains "j"

            var jText = textList.First(text => text.Contains("j"));

            // 4 first element that contains Z or default
            var firstOrDefaultText = textList.FirstOrDefault(text => text.Contains("z"));

            // 5 last element that contains Z or default
            var lastOrDefaultText = textList.LastOrDefault(text => text.Contains("z"));

            // 6 single values
            var uniqueTexts = textList.Single();
            var uniqueOrDefaultTexts = textList.SingleOrDefault();

            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumbers = { 0, 2, 6 };

            // Obtain { 4, 8 }
            var myEvenNumbers = evenNumbers.Except(otherEvenNumbers); // {4, 8}

       }

        public static void MultipleSelects()
        {
            // select many

            string[] myOpinions =
            {
                "Opinion1, valor1",
                "Opinion2, valor2",
                "Opinion3, valor3"
            };

            var myOpinionSelection = myOpinions.SelectMany(option => option.Split(","));

            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id = 1,
                    Name = "Empresa 1",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id = 1,
                            Name = "Christopher",
                            Email = "testing1@snkr.cl",
                            Salary = 3000
                        },
                        new Employee
                        {
                            Id = 2,
                            Name = "Alex",
                            Email = "testing2@snkr.cl",
                            Salary = 2000
                        },
                        new Employee
                        {
                            Id = 3,
                            Name = "Ignacio",
                            Email = "testing33@snkr.cl",
                            Salary = 4000
                        }
                    }
                },
                new Enterprise()
                {
                    Id = 2,
                    Name = "Empresa 2",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id = 4,
                            Name = "Pedro",
                            Email = "testing4@snkr.cl",
                            Salary = 1000
                        },
                        new Employee
                        {
                            Id = 5,
                            Name = "Maxi",
                            Email = "testing5@snkr.cl",
                            Salary = 2000
                        },
                        new Employee
                        {
                            Id = 6,
                            Name = "Poncho",
                            Email = "testing6@snkr.cl",
                            Salary = 4000
                        }
                    }
                }
            };

            // Obtain all employees of all enterprises

            var employeeList = enterprises.SelectMany(enterprise => enterprise.Employees);

            // know if any list is empty

            bool hasEnterprises = enterprises.Any();
            bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());

            // All enterprises at least has an employee with more than 1000 usd of salary

            bool hasEmployeeWithSalaryMoreThanOrEqual1000 =
                enterprises.Any(enterprise =>
                enterprise.Employees.Any(employee => employee.Salary >= 1000));

        }

        public static void linqCollections()
        {
            var firstList = new List<string>() { "a", "b", "c" };
            var secondList = new List<string>() { "a", "c", "d" };

            // innerjoin

            var commonResult = from element in firstList
                               join secondElement in secondList
                               on element equals secondElement
                               select new { element, secondElement };

            var commonResult2 = firstList.Join(
                    secondList,
                    element => element,
                    secondElement => secondElement,
                    (element, secondElement) => new { element, secondElement }
                );

            // outer join left

            var leftOuterJoin = from element in firstList
                                join secondElement in secondList
                                on element equals secondElement
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where element != temporalElement
                                select new { Element = element };

            var leftOuterJoin2 = from element in firstList
                                 from secondElement in secondList.Where(s => s == element).DefaultIfEmpty()
                                 select new {Element = element, SecondElement = secondElement};

            // outer join right

            var rightOuterJoin = from secondElement in secondList
                                join element in firstList
                                on secondElement equals element
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where secondElement != temporalElement
                                select new { Element = secondElement };

            // union

            var unionList = leftOuterJoin.Union(rightOuterJoin);

        }

        public static void SkipTakeLinq()
        {
            var myList = new[]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10
            };

            var skipTwoFirstValues = myList.Skip(2); // { 3,4,5,6,7,8,9,10 }

            var skipLastTwoFirstValues = myList.SkipLast(2); // { 1,2,3,4,5,6,7,8 }

            var skipWhileSmallerThan4 = myList.SkipWhile(num => num < 4);  // {4,5,6,7,8}

            // take

            var takeFirstTwoValues = myList.Take(2); // { 1,2 }
            
            var takeLastTwoValues = myList.TakeLast(2); // { 9,10 }

            var takeWhileSmallerThan4 = myList.TakeWhile(num => num < 4); // {1,2,3}

        }

        // paging

        public static IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultsPerPage)
        {
            int startIndex = (pageNumber - 1) * resultsPerPage;
            return collection.Skip(startIndex).Take(resultsPerPage);
        }

        public static void LinqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var aboveAverage = from number in numbers
                               let average = numbers.Average()
                               let nSquared = Math.Pow(number, 2)        // Math.pow es un exponencial, en este caso al cuadrado
                               where nSquared > average
                               select number;

            Console.WriteLine("Average: {0}", numbers.Average());

            foreach (int number in aboveAverage)
            {
                Console.WriteLine("Number: {0} Square: {1}", number, Math.Pow(number, 2));
            }

        }

        // zip

        public static void ZipLinq()
        {
             int[] numbers = { 1, 2, 3, 4, 5 };
             string[] stringNumbers = { "one", "two", "three", "four", "five" };

             IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers, (number, word) => number + "=" + word); // { "1=one", "2=two", ... }

        }

        // repeat range

        public static void repeatRangeLinq()
        {
            // range 1 - 1000

            IEnumerable<int> first1000 = Enumerable.Range(1, 1000);

            // repeat a value N times

            IEnumerable<string> fiveXs = Enumerable.Repeat("x", 5); // "x", "x", "x", "x", "x"

        }

        public static void studentsLinq()
        {
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Maxi",
                    Grade = 44,
                    Certified = true
                },
                new Student
                {
                    Id = 1,
                    Name = "Pedro",
                    Grade = 10,
                    Certified = false
                },
                new Student
                {
                    Id = 1,
                    Name = "Liso",
                    Grade = 60,
                    Certified = true
                }
            };

            var certifiedStudents = from student in classRoom
                                    where student.Certified
                                    select student;

            var notCertifiedStudents = from student in classRoom
                                       where student.Certified == false
                                       select student;

            var approvedStudents = from student in classRoom
                                   where student.Grade >= 40 && student.Certified == true
                                   select student.Name;

        }   

        // all

        public static void AllLinq()
        {
            var numbers = new List<int>() { 1,2,3,4,5 };

            bool allAreSmallerThan10 = numbers.All(x => x < 10); // true
            bool allAreBiggerThan2 = numbers.All(x => x >= 2); // false

            var emptyList = new List<int>();

            bool allNumbersAreGreaterThan0 = numbers.All(x => x >= 0);

        }

        // aggregate
        public static void aggregateQueries()
        {
            int[] numbers = { 1, 2, 3, 4, 6, 7, 8, 9, 10 };

            // sum all numbers

            int sum = numbers.Aggregate((prevSum, current) => prevSum + current);

            string[] words = { "hello", "my", "name", "is", "christopher" };
            string greetings = words.Aggregate((prevGreeting, current) => prevGreeting + current);

        }

        // disctinct

        public static void distinctValues()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };
            IEnumerable<int> distinctValues = numbers.Distinct();
        }

        // groupby

        public static void groupByExamples()
        {
            List<int> numbers = new List<int>() { 1,2,3,4,5,6,7,8,9};

            // obtain only even numbers and generate two groups
            var grouped = numbers.GroupBy(x => x % 2 == 0);

            /*
             * we will have two groups
             * 1 the group that doesnt fit the condition (odd numbers)
             * 2 the group that fits the condition (even numbers)
             */

            foreach ( var group in grouped )
            {
                foreach ( var value in group)
                {
                    Console.WriteLine(value);
                }
            }

            // otro ejemplo

            var classRoom = new[]
{
                new Student
                {
                    Id = 1,
                    Name = "Maxi",
                    Grade = 44,
                    Certified = true
                },
                new Student
                {
                    Id = 1,
                    Name = "Pedro",
                    Grade = 10,
                    Certified = false
                },
                new Student
                {
                    Id = 1,
                    Name = "Liso",
                    Grade = 60,
                    Certified = true
                }
            };

            var certifiedQuery = classRoom.GroupBy(student => student.Certified);

            /*
             * we will obtain two groups
             * 1 not certified students
             * 2 certified students
             */

            foreach (var group in certifiedQuery)
            {
                Console.WriteLine("---------- {0} ----------", group.Key);
                foreach (var student in group)
                {
                    Console.WriteLine(student);
                }
            }
        }

        public static void relationsLinq()
        {
            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Title = "Test",
                    Content = "Test",
                    Created = DateTime.Now,
                    Commits = new List<Commit>()
                    {
                        new Commit()
                        {
                            id = 1,
                            Created = DateTime.Now,
                            Title = "commit1",
                            Content = "my content"
                        },
                        new Commit()
                        {
                            id = 2,
                            Created = DateTime.Now,
                            Title = "commit2",
                            Content = "my content"
                        },
                        new Commit()
                        {
                            id = 3,
                            Created = DateTime.Now,
                            Title = "commit3",
                            Content = "my content"
                        }
                    }
                },
                new Post()
                {
                    Id = 2,
                    Title = "Test2",
                    Content = "Test2",
                    Created = DateTime.Now,
                    Commits = new List<Commit>()
                    {
                        new Commit()
                        {
                            id = 4,
                            Created = DateTime.Now,
                            Title = "commit4",
                            Content = "my content"
                        },
                        new Commit()
                        {
                            id = 5,
                            Created = DateTime.Now,
                            Title = "commit5",
                            Content = "my content"
                        },
                        new Commit()
                        {
                            id = 6,
                            Created = DateTime.Now,
                            Title = "commit6",
                            Content = "my content"
                        }
                    }
                }
            };


            var commentsWithContent = posts.SelectMany(post => post.Commits, 
                (post, commit) => new { PostId = post.Id, CommitContent = commit.Content });



        }

    }
}