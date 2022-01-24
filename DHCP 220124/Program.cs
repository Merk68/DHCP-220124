using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DHCP_220124
{
  class Program
  {
    static List<int> nemkioszthato = new List<int>();
    static Dictionary<string, int> fenttartott = new Dictionary<string, int>();
    static Dictionary<string, int> kiosztott = new Dictionary<string, int>();
    static string tart = "192.168.10.";
    static int elsoip = 100;
    static int utolsoip = 199;
    static void Main(string[] args)
    {
      FileBeolvasas();
      FileKiiras();
      Feladat();
      KonzolbaKiiras();
      Console.ReadKey();
    }

    private static void KonzolbaKiiras()
    {
      foreach (var k in kiosztott)
      {
        Console.WriteLine("{0}; {1}{2}", k.Key, tart, k.Value);
      }
    }

    private static void Feladat()
    {
      foreach (string item in File.ReadAllLines("test.csv"))
      { 
        string muvelet = item.Split(';')[0];
        if (muvelet == "release")
        {
          int ip = int.Parse(item.Split(';', '.')[4]);
          if (kiosztott.ContainsValue(ip))
        {
          kiosztott.Remove(kiosztott.First(d => d.Value == ip).Key);
        }
      }
    }
    }

    private static void FileKiiras()
    {
      StreamWriter sw = new StreamWriter("dhcp_kesz.csv");
      foreach (var d in kiosztott)
      {
        sw.WriteLine("{0};{1} {2}", d.Key, tart, d.Value);
      }
      sw.Close();
    }

    private static void FileBeolvasas()
    {
      foreach (string item in File.ReadAllLines("excluded.csv"))
      {
        nemkioszthato.Add(int.Parse(item.Split('.')[3]));
      }
      foreach (string item in File.ReadAllLines("reserved.csv"))
      {
        fenttartott.Add(item.Split(';')[0], int.Parse(item.Split(';', '.')[4]));
      }
      foreach (string item in File.ReadAllLines("dhcp.csv"))
      {
        kiosztott.Add(item.Split(';')[0], int.Parse(item.Split(';', '.')[4]));
      }
    }
  }
}
