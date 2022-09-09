namespace Backup;
using Backup.StorageDevice;

class Program
{
    static void Main()
    {
       
    Flash flash = new("SanDisk Ultra Dual Drive", "032G-G46", 32);
        DVD dvd = new("LG", "BP60NB10", _DVDVersion.FullSide);
        HDD hdd = new("Seagate BarraCuda", "ST2000DM008", 2048);

        double data = 0;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\tDEVICE INFO\n\n\n\n");

            Console.WriteLine(flash);
            Console.WriteLine();

            Console.WriteLine(dvd);
            Console.WriteLine();

            Console.WriteLine(hdd);
            Console.ReadKey(false);

            Console.Write("\nEnter data size(GB): ");
            try
            {
                if (!double.TryParse(Console.ReadLine(), out data))
                    throw new InvalidCastException();

            }
            catch (Exception ex)
            {

                Console.Clear();
                Console.WriteLine(ex.Message);

                Thread.Sleep(1500);
                continue;
            }
            break;
        }



        (short deviceSize, TimeSpan needTime) flashInfo = flash.Copy(data);

        Console.WriteLine($@"
Device Size: {flashInfo.deviceSize}
Needed Time: {flashInfo.needTime}");


        (short deviceSize, TimeSpan needTime) DVDInfo = dvd.Copy(data);

        Console.WriteLine($@"
Device Size: {DVDInfo.deviceSize}
Needed Time: {DVDInfo.needTime}");


        (short deviceSize, TimeSpan needTime) HDDInfo = hdd.Copy(data);

        Console.WriteLine($@"
Device Size: {HDDInfo.deviceSize}
Needed Time: {HDDInfo.needTime}");


    }
}