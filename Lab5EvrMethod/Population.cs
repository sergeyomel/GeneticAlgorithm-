using System;
using System.Collections.Generic;

namespace Lab5EvrMethod
{
    class Population
    {
        //НАБОР ПЕРЕМЕННЫХ
        private readonly Int32 countPC;                         //Количество процессоров
        private readonly Int32 countTasks;                      //Колчество задач
        private readonly Int32 lowRangeTasks;                   //Нижняя граница расписания
        private readonly Int32 highRangeTasks;                  //Верхняя граница расписания
        private readonly Int32 sizeRange;                       //Размер границы генотипа
        private readonly Int32 countIndividualInPopulation;     //Количество особей в популяции
        private readonly Int32 countBestIndividual;             //Количество лучших особей
        private readonly Int32 propabilityCrossover;            //Вероятность кроссовера
        private readonly Int32 propabilityMutation;             //Вероятность мутации
        private Int32 countSearchIndividual = 0;                //Сколько сейчас лучших особей
        private Int32 minTime = Int32.MaxValue;                 //Минимальное затраченное время
        private static Random rnd = new Random();

        private List<Generation> lGeneration = new List<Generation>();

        public Population(Int32 _countProcessor, Int32 _countTasks, Int32 _lowRangeTasks,
                          Int32 _highRangeTasks, Int32 _sizeRange, Int32 _countIndividualInPopulation, 
                          Int32 _countBestIndividual, Int32 _propabilityCrossover, Int32 _propabilityMutation)
        {
            countPC = _countProcessor;
            countTasks = _countTasks;
            lowRangeTasks = _lowRangeTasks;
            highRangeTasks = _highRangeTasks;
            sizeRange = _sizeRange;
            countIndividualInPopulation = _countIndividualInPopulation;
            countBestIndividual = _countBestIndividual;
            propabilityCrossover = _propabilityCrossover;
            propabilityMutation = _propabilityMutation;

            InitialFilling();
        }

        private void InitialFilling()
        {
            var lNumber = new List<Int32>();
            for (int iter = 0; iter < countTasks; iter++)
                lNumber.Add(rnd.Next(lowRangeTasks, highRangeTasks));

            lGeneration.Add(new Generation(countIndividualInPopulation, sizeRange, countPC, propabilityCrossover));

            for (int iter = 0; iter < countIndividualInPopulation; iter++)
                lGeneration[0].AddIndividual(new Individual(sizeRange, countTasks, propabilityMutation, lNumber));

            lGeneration.Add(null);
        }

        public String CalculateAnswer()
        {
            Int32 countGen = 1;
            while(countSearchIndividual < countBestIndividual)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("{0,5} поколение:\n", countGen++);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine(lGeneration[0].ToString());

                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Количество лучших особей - {0,5} / Лучшая особь - {1,5}\n", countSearchIndividual, minTime);
                Console.BackgroundColor = ConsoleColor.Black;

                lGeneration[1] = new Generation(countIndividualInPopulation, sizeRange, countPC, propabilityCrossover);
                lGeneration[1].FillingGeneration(lGeneration[0]);

                CalculateCountIndividual();

                lGeneration[0] = lGeneration[1];
                lGeneration[1] = null;
            }
            Console.BackgroundColor = ConsoleColor.Red;
            //Console.WriteLine("ПОСЛЕДНЕЕ ПОКОЛЕНИЕ:\n");
            Console.WriteLine("КОЛИЧЕСТВО ЛУЧШИХ ОСОБЕЙ: {0}",countSearchIndividual);
            Console.WriteLine("ЛУЧШАЯ ОСОБЬ: {0}",minTime);
            Console.BackgroundColor = ConsoleColor.Black;
            //Console.WriteLine(lGeneration[0]);

            Console.BackgroundColor = ConsoleColor.Red;
            return String.Format("ПОКОЛЕНИЙ ПРОШЛО СО СТАРТА - {0,5}\n", countGen);
        }

        private void CalculateCountIndividual()
        {
            foreach(var item in lGeneration[1].Getlindividual())
            {
                var weight = new ScheduleObject(item, sizeRange, countPC).Weight().weight;

                if (weight == minTime)
                    countSearchIndividual++;
                if (weight < minTime)
                {
                    countSearchIndividual = 1;
                    minTime = weight;
                }
            }
        }
    }
}
