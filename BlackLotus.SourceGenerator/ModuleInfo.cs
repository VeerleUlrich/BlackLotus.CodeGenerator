using System;

namespace BlackLotus.SourceGenerator;

public sealed class ModuleInfo : ISyntaxInfo, IEquatable<ModuleInfo>
{
    public ModuleInfo(string moduleName)
    {
        ModuleName = moduleName;
    }

    public string ModuleName { get; }


    public bool Equals(ModuleInfo? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return ModuleName == other.ModuleName;
    }

    public override bool Equals(object? obj)
        => ReferenceEquals(this, obj) ||
           (obj is ModuleInfo other && Equals(other));
    
}