using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backup.StorageDevice;

// COPY methodunda vaxti ve cihazi bir bir tapmaqdansa tuple ile qaytardim 

abstract class Storage
{
    private string? mediaName;
    private string? model;


    public string? MediaName
    {
        get { return mediaName; }
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException("Invalid Media name");
            mediaName = value;
        }
    }


    public string? Model
    {
        get { return model; }
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException("Invalid Model");
            model = value;
        }
    }

    protected Storage(string? mediaName, string? model)
    {
        MediaName=mediaName;
        Model=model;
    }

    public abstract (short deviceSize, TimeSpan needTime) Copy(double data);

    public abstract double FreeMemory();


    public override string ToString()
=> $@"Media Name: {MediaName}
Model: {Model}
Free Memory: {FreeMemory()}";

}