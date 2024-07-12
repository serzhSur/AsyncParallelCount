// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
//Асинхронность позволяет вынести отдельные задачи из основного потока в специальные асинхронные методы
//и при этом более экономно использовать потоки. Асинхронные методы выполняются в отдельных потоках.

int[] num1 = new int[3] { 1,2,3 };
int[] num2 =new int[6] {1,2,3,4,5,6};
Stopwatch sw = Stopwatch.StartNew();
TimeSpan elapsedTime = sw.Elapsed;


Console.WriteLine("Start regular methods...");
sw.Start();
Counting(num1);
Counting(num2);
sw.Stop();
elapsedTime = sw.Elapsed;
Console.WriteLine($"Общее время выполнения: {elapsedTime}");

Console.WriteLine("Start Asynk sposob...");
sw.Reset();
sw.Start();
await CountingAsync(num1);
await CountingAsync(num2);
sw.Stop();
elapsedTime = sw.Elapsed;
Console.WriteLine($"Общее время выполнения: {elapsedTime}");

Console.WriteLine("Start PARALLEL Asynk sposob...");
//В данном случае задачи фактически запускаются при определении. А оператор await применяется лишь тогда,
//когда нам нужно дождаться завершения асинхронных операций - то есть в конце программы. 
sw.Reset();
sw.Start();

var num1Task = CountingAsync(num1);
var num2Task = CountingAsync(num2);

await num1Task;
await num2Task;
sw.Stop();
elapsedTime = sw.Elapsed;
Console.WriteLine($"Общее время выполнения: {elapsedTime}");


Console.WriteLine("Finish program.");
Console.ReadKey();


void Counting(int[] num)
{
    for (int i = 0; i < num.Length; i++)
    {
        Console.Write($"{num[i]} ");
        Thread.Sleep(1000);
    }
    Console.WriteLine($"\nвыполнено");
}

async Task CountingAsync(int[] num) 
{
    await Task.Run(() => Counting(num));
    //Пока выполняется асинхронная задача Task.Run(()=>Counting(num)) (а она может выполняться довольно продожительное время),
    //выполнение кода возвращается в вызывающий метод - то есть в метод Main.
}