using System;
using System.Collections.Generic;

namespace Lab5EvrMethod
{

    //TODO
    //Не перестраивается расписание при мутации

    class MainProgrammsClass
    {
        static void Main(string[] args)
        {
            Int32 countPC = 4;                            //Количество процессоров
            Int32 countTasks = 11;                         //Колчество задач
            Int32 lowRangeTasks = 10;                     //Нижняя граница расписания
            Int32 highRangeTasks = 21;                    //Верхняя граница расписания
            Int32 sizeRange = 255;                        //Размер границы генотипа
            Int32 countIndividualInPopulation = 10;        //Количество особей в популяции
            Int32 countBestIndividual = 10;                //Количество лучших особей
            Int32 propabilityCrossover = 85;             //Вероятность кроссовера
            Int32 propabilityMutation = 93;              //Вероятность мутации

            Population population = new Population(countPC, countTasks, lowRangeTasks, highRangeTasks, sizeRange,
                                                   countIndividualInPopulation, countBestIndividual, propabilityCrossover,
                                                   propabilityMutation);

            Console.WriteLine(population.CalculateAnswer());

            /*Individual individualOne = new Individual(255, 8, 100, new List<int> { 23, 34, 25, 14, 88, 13, 37, 34 });
            Console.WriteLine("Особь 1");
            Console.WriteLine(individualOne.ToString());

            //var individualTwo = individualOne.CloneIndividualNumber();
            //Console.WriteLine("Особь 2");
            //Console.WriteLine(individualTwo.ToString());

            //Crossover cs = new Crossover(100);
            //var item = cs.Crossbreeding(individualOne, individualTwo);

            ScheduleObject scheduleOne = new ScheduleObject(individualOne, 255, 3);
            //ScheduleObject scheduleTwo = new ScheduleObject(item.itemTwo, 255, 3);

            Console.WriteLine(scheduleOne);
            //Console.WriteLine(scheduleTwo);

            individualOne.GeneMutation();
            //individualTwo.GeneMutation();

            Console.WriteLine(individualOne.ToString());
            //Console.WriteLine(individualTwo.ToString());
            Console.WriteLine(scheduleOne);
            //Console.WriteLine(scheduleTwo);

            //Console.WriteLine(scheduleOne.MinSchedule(scheduleTwo));*/

            Console.ReadKey();
        }
    }
}
