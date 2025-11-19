using WebCore.Debugging;

namespace WebCore;

public class WebCoreConsts
{
    public const string LocalizationSourceName = "WebCore";

    public const string ConnectionStringName = "Default";

    public const bool MultiTenancyEnabled = true;


    /// <summary>
    /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
    /// </summary>
    public static readonly string DefaultPassPhrase =
        DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "ca5e0dc9dd194ac2b072358c410e9ee9";
}
