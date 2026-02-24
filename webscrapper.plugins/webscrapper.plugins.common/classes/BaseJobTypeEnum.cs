namespace webscrapper.plugins.common.classes;

public class BaseJobTypeEnum
{
    public string Name { get; private set; }
    public int Value { get; private set; }

    protected BaseJobTypeEnum(int value, string name)
    {
        Value = value;
        Name = name;
    }

    public static readonly BaseJobTypeEnum AUTH = new BaseJobTypeEnum(1, "AUTH");

    public static bool operator ==(BaseJobTypeEnum a, BaseJobTypeEnum b) => a?.Value == b?.Value;
    public static bool operator !=(BaseJobTypeEnum a, BaseJobTypeEnum b) => !(a == b);

    public override bool Equals(object obj) => obj is BaseJobTypeEnum other && this == other;
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Name;
}
