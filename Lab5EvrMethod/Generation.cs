using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5EvrMethod
{
    class Generation
    {
        private readonly Int32 countIndividual;
        private Int32 sizeRange;
        private Int32 countPC;

        private Crossover cS;
        private static Random rnd = new Random();

        private List<Individual> lIndividual;
        public List<Individual> Getlindividual() => lIndividual;

        public Generation(Int32 _countIndividual, Int32 _sizeRange, 
                          Int32 _countPC, Int32 _propability)
        {
            countIndividual = _countIndividual;
            sizeRange = _sizeRange;
            countPC = _countPC;
            lIndividual = new List<Individual>();
            cS = new Crossover(_propability);
        }

        internal void AddIndividual(Individual individual)
        {
            if (lIndividual.Count < countIndividual)
                lIndividual.Add(individual);
        }

        public void FillingGeneration(Generation generation)
        {
            var lGenIndividual = generation.Getlindividual();

            for (int iter = lIndividual.Count; iter < countIndividual; iter++)
            {
                Int32 randPos = rnd.Next(0, countIndividual);
                while(randPos == iter)
                    randPos = rnd.Next(0, countIndividual);

                Console.WriteLine("Берём {0} и {1} особи\n", iter, randPos);

                var resultCrossbreeding = cS.Crossbreeding(lGenIndividual[iter], lGenIndividual[randPos]);
                if (resultCrossbreeding.flag)
                {
                    ScheduleObject schOne = new ScheduleObject(lGenIndividual[iter], sizeRange, countPC);

                    ScheduleObject schTwo = new ScheduleObject(resultCrossbreeding.itemOne, sizeRange, countPC);
                    Console.WriteLine("Первая особь после кроссинговера:\n" + resultCrossbreeding.itemOne.ToString());
                    Console.WriteLine("Расписание на её основе:\n" + schTwo.ToString());
                    if (resultCrossbreeding.itemOne.GeneMutation())
                    {
                        Console.WriteLine("Мутировавшая первая особь:\n" + resultCrossbreeding.itemOne.ToString());
                        Console.WriteLine("Расписание на её основе:\n" + schTwo.ToString());
                    }else
                        Console.WriteLine("Первый ребёнок не мутировал.\n");

                    ScheduleObject schThree = new ScheduleObject(resultCrossbreeding.itemTwo, sizeRange, countPC);
                    Console.WriteLine("Вторая особь после кроссинговера:\n" + resultCrossbreeding.itemTwo.ToString());
                    Console.WriteLine("Расписание на её основе:\n" + schThree.ToString());
                    if (resultCrossbreeding.itemTwo.GeneMutation())
                    {
                        Console.WriteLine("Мутировавшая вторая особь:\n" + resultCrossbreeding.itemTwo.ToString());
                        Console.WriteLine("Расписание на её основе:\n" + schThree.ToString());
                    }
                    else
                        Console.WriteLine("Второй ребёнок не мутировал.\n");

                    var resEqual = schOne.MinSchedule(schTwo).MinSchedule(schThree);
                    lIndividual.Add(resEqual.GetIndividual());
                    Console.WriteLine("В новое поколение добавлена следующая особь: ");
                    Console.WriteLine(resEqual.GetIndividual().ToString());
                }
                else
                {
                    Console.WriteLine("Родители не скрестились.");
                    var buffInv = lGenIndividual[iter].CloneIndividualFull();
                    Console.WriteLine("Пытаемся мутировать родителя.\n");
                    if (buffInv.GeneMutation())
                    {
                        Console.WriteLine("Мутировали родителя:\n" + buffInv.ToString());
                        ScheduleObject schOne = new ScheduleObject(lGenIndividual[iter], sizeRange, countPC);
                        ScheduleObject schTwo = new ScheduleObject(buffInv, sizeRange, countPC);

                        Console.WriteLine("Расписание на основе мутированного родителя:\n" + schTwo.ToString());
                        var item = schOne.MinSchedule(schTwo);
                        Console.WriteLine("В популяцию попадает данная особь:\n" + item.GetIndividual().ToString());
                        lIndividual.Add(item.GetIndividual());
                    }
                    else
                    {
                        Console.WriteLine("Мутации родителя не произошло.\n");
                        lIndividual.Add(lGenIndividual[iter]);
                    }
                }     
            }

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in lIndividual)
                sb.Append(item.ToString() + "\n");
            return sb.ToString();
        }

    }
}
