using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backup.StorageDevice;

enum _DVDVersion { FullSide, HalfSide }
internal class DVD : Storage
{
    public const double WriteSpeed = 1.38;

    public readonly _DVDVersion DVDVersion;
    private double usingMemory = 0;

    public double UsingMemory
    {
        get { return usingMemory; }
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException("Memory can't be lower than 0");

            if (DVDVersion == _DVDVersion.FullSide && value > 9 || DVDVersion == _DVDVersion.HalfSide && value >4.7)
                throw new ArgumentOutOfRangeException("Memory is FULL !");

            usingMemory = value;
        }
    }

    public DVD(string? mediaName, string? model, _DVDVersion DVDversion)
        : base(mediaName, model)
    {
        DVDVersion = DVDversion;
    }

    public override (short deviceSize, TimeSpan needTime) Copy(double data)
    {
        short temp;
        short deviceSize;
        if (DVDVersion == _DVDVersion.FullSide)
        {
            temp = Convert.ToInt16(data/ 9);

            deviceSize = temp;

            if ((data / 9) - temp > 0) ++deviceSize;
        }

        else
        {
            temp = Convert.ToInt16(data/ 4.7);

            deviceSize = temp;

            if ((data / 4.7) - temp > 0) ++deviceSize;
        }

        int second = Convert.ToInt32((data*1024)/WriteSpeed);
        return (deviceSize, new TimeSpan(0, 0, second));
    }

    public override double FreeMemory()
    {
        if (DVDVersion == _DVDVersion.FullSide)
            return 9 - usingMemory;

        return 4.7 - usingMemory;
    }

    public override string ToString()
=> $@"{base.ToString()}
DVD Version: {DVDVersion}
Write Speed: {WriteSpeed} MB/s
Using Memory: {UsingMemory} GB";
}