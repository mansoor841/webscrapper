using webscrapper.plugins.common.classes;

namespace webscrapper.plugins.venture.classes;

public class VentureJobTypeEnum : BaseJobTypeEnum
{
    public static readonly VentureJobTypeEnum EOD = new VentureJobTypeEnum(2, "EOD");
    public static readonly VentureJobTypeEnum UPDATE = new VentureJobTypeEnum(3, "UPDATE");

    private VentureJobTypeEnum(int value, string name) : base(value, name) { }
}
