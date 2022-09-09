using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backup.StorageDevice;

internal class Flash : Storage
{
    public const int USB3WriteSpeed = 640;
    public readonly double Memory;
    private double usingMemory = 0;

    public double UsingMemory
    {
        get { return usingMemory; }
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException("Memory can't be lower than 0");

            if (value > Memory)
                throw new ArgumentOutOfRangeException("Memory is FULL !");

            usingMemory = value;
        }
    }

    public Flash(string? mediaName, string? model, double memory)
        : base(mediaName, model)
    {
        Memory=memory;
    }

    public override (short deviceSize, TimeSpan needTime) Copy(double data)
    {
        short temp = Convert.ToInt16(data/ Memory);
        short deviceSize = temp;

        if ((data/Memory) - temp > 0)
            ++deviceSize;

        int second = Convert.ToInt32((data*1024)/USB3WriteSpeed);

        return (deviceSize, new TimeSpan(0, 0, second));
    }

    public override double FreeMemory()
    {
        return Memory - usingMemory;
    }

    public override string ToString()
=> $@"{base.ToString()}
Write Speed: {USB3WriteSpeed} MB/s
Memory: {Memory} GB
Using Memory: {UsingMemory} GB";
}
