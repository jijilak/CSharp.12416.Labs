# CSharp.12416.Labs
Начало занятий с 12.05.2022. Группа 124-16 
  
\\Первое домашнее задание это Lab1Ex1-Lab1Ex4
{
Лабораторная работа 1. Создание простой C#-программы.
Цель работы: изучение структуры программы на языке C# и приобретение навыков ее компиляции и отладки.
  {
  Lab1Ex1: Создание простой программы.
  В этом упражнении Вы напишите программу на языке C#, используя среду разработки Visual Studio.NET. Программа будет спрашивать, как Вас зовут и затем здороваться с Вами по имени, выполнила 14.05.2022.
  }
  {
  Lab1Ex2: Компиляция и запуск C#-программы из командной строки.
  В этом упражнении Вы откомпилируете и запустите Вашу программу из командной строки, выполнила 14.05.2022.
  }
  {
  Lab1Ex3: Использование отладчика Visual Studio .NET.
  В этом задании Вы приобретете навыки работы с интегрированным отладчиком Visual Studio .NET, изучите порядок отладки программы по шагам и окна для просмотра значения переменных, выполнила 14.05.2022.
  }
  {
  Lab1Ex4: Добавление в C#-программу обработчика исключительных ситуаций.
  В этом упражнении Вы напишите программу, в которой будет использоваться обработчик исключительных ситуаций, который будет отлавливать ошибки времени выполнения. Программа будет запрашивать у пользователя два целых числа, делить первое число на второе и выводить полученный результат, выполнила 14.05.2022.
  }
}
  
\\Второе домашнее задание это Lab2Ex1-Lab2Ex2
{
Лабораторная работа 2. Создание и использование размерных типов данных.
Цель работы: изучение размерных типов данных и приобретение навыков работы со структурными типами.
   {
   Lab2Ex1: Создание перечисления.
   В этом упражнении Вы создадите перечисление для представления различных типов банковских счетов. Затем Вы используете это перечисление для создания двух переменных, которым Вы присвоите значения Checking и Deposit. Далее Вы выведете на экран значения этих переменных, используя функцию System.Console.WriteLine, выполнила 16.05.2022.
   }
   {
   Lab2Ex2: Создание и использование структуры (Struct).
   В этом упражнении Вы создадите структуру, которую можно использовать для представления банковских счетов. Для хранения номеров счетов (тип данных long), балансов счетов (тип данных decimal) и типов счетов (перечисление, созданное в упражнении 1) будете использовать переменные. Затем создадите переменную типа структуры, заполните ее данными и выведете результаты на консоль, выполнила 16.05.2022.
   }
}
\\Третье домашнее задание это Lab3Ex1-Lab3Ex3
{
Лабораторная работа 3. Использование выражений и исключений.
Цель работы: изучение и приобретение навыков использования управляющих конструкций для организации вычислений и механизма обработки исключительных ситуаций.
   {
   Lab3Ex1: Преобразование дня года в дату типа месяц - день.
   В этом упражнении Вы напишите программу, которая считывает целое число, являющееся днем года (от 1 до 365) с экрана консоли и сохраняет его в целочисленной переменной. Далее программа преобразует это число в название и день месяца и выводит результат на консоль. Например, вводим числом 40, получаем результат “February 9”. (В данном упражнении не учитываются високосные годы), выполнила 19.05.2022.
   }
   {
   Lab3Ex2: Проверка вводимого пользователем значения дня года.
   В этом упражнении Вы расширите возможности программы, созданной в упражнении 1.  Программа будет проверять день года, вводимого пользователем. Если он будет меньше 1 или больше 365, то будет выбрасываться исключение InvalidArgument (“Day out of range”). Программа будет перехватывать это исключение в блоке catch и выводить на консоль сообщение об ошибке, выполнила 23.05.2022.
   }
   {
   Lab3Ex3: Учет високосных годов.
   В этом упражнении Вы расширите возможности программы, разработанной в упражнении 2. Конечный вариант программы будет запрашивать у пользователя не только день года, но и сам год. Программа будет определять, является ли год високосным. Если да, то будет проверяться, попадает ли значение дня года в диапазон от 1 до 366. Если год не является високосным, то проверяется попадание значения дня года в диапазон от 1 до 365, выполнила 23.05.2022.
   }
}

\\Четвёртое домашнее задание это Lab4Ex1-Lab4Ex2
{
Лабораторная работа 4. Создание и использование методов.
Цель работы: изучение и приобретение навыков работы с методами класса.
   {
   Lab4Ex1: Использование параметров в методах, возвращающих значения.
   В этом упражнении Вы создадите класс Utils, в котором определите метод Greater. Этот метод будет принимать два целочисленных параметра и возвращать больший из них.
Для тестирования работы данного класса Вы создадите еще один класс (класс Test), в котором у пользователя будут запрашиваться два числа, далее будет вызываться метод Utils.Greater, после чего на экран консоли будет выводиться результат.  
   }
   {
   Lab4Ex2: Использование в методах параметров, передаваемых по ссылке.
   В этом упражнении Вы создадите метод Swap, который поменяет местами значения параметров. При этом вы будете использовать параметры, передаваемые по ссылке.
   }
}

\\Пятое домашнее задание это Lab5Ex1
{
Лабораторная работа 5. Создание и использование массивов.
Цель работы: изучение массивов и приобретение навыков работы с ними.
   {
   Lab5Ex1: Работа с массивами размерных типов.
   В этом упражнении Вы напишите программу, в которой в метод Main в качестве аргумента будет передаваться имя текстового файла. Содержимое текстового файла будет считываться в массив символов, а дальше будут производиться итерации по всему массиву для подсчета количества гласных и согласных. В итоге, на консоль будет выводиться информация об общем количестве символов, гласных, согласных и строк.
   }
}

\\Шестое домашнее задание это Lab6Ex1-Lab6Ex2, Lab7Ex1-Lab7Ex2


