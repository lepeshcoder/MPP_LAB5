using ConsoleApp1;

A a = new A();
a.b = "kal";
a.c = 5;
a.d = 10.079;
StringFormatter stringFormater = new StringFormatter();
Console.WriteLine(stringFormater.Format("b = {e}, c = {c}, d = {d}", a));


public class A
{
    public string b;
    public int c;
    public double d;
}