using System;

namespace OpenTTD
{
    /**
     * Gets the author name to be shown in the 'Available Scripts' window.
     *
     * @returns The author name of the Script.
     * @note This function is required.
     */
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class AuthorAttribute : Attribute { public AuthorAttribute(string value) { } }

    /**
     * Gets the Scripts name. This is shown in the 'Available Scripts' window
     * and at all other places where the Script is mentioned, like the debug
     * window or OpenTTD's help message. The name is used to uniquely
     * identify an Script within OpenTTD and this name is used in savegames
     * and the configuration file.
     *
     * @returns The name of the Script.
     * @note This function is required.
     * @note This name is not used as library name by AIController.Import,
     * instead the name returned by 
     */
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class NameAttribute : Attribute { public NameAttribute(string value) { } }

    /**
     * Gets a 4 ASCII character short name of the Script to uniquely
     * identify it from other Scripts. The short name is primarily
     * used as unique identifier for the content system.
     * The content system uses besides the short name also the
     * MD5 checksum of all the source files to uniquely identify
     * a specific version of the Script.
     *
     * The short name must consist of precisely four ASCII
     * characters, or more precisely four non-zero bytes.
     *
     * @returns The name of the Script.
     * @note This function is required.
     */
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class ShortNameAttribute : Attribute { public ShortNameAttribute(string value) { } }

    /**
     * Gets the description to be shown in the 'Available Scripts' window.
     *
     * @returns The description for the Script.
     * @note This function is required.
     */
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class DescriptionAttribute : Attribute { public DescriptionAttribute(string value) { } }

    /**
     * Gets the version of the Script. This is a number to (in theory)
     * uniquely identify the versions of an Script. Generally the
     * 'instance' of an Script with the highest version is chosen to
     * be loaded.
     *
     * When OpenTTD finds, during starting, a duplicate Script with the
     * same version number one is randomly chosen. So it is
     * important that this number is regularly updated/incremented.
     *
     * @returns The version number of the Script.
     * @note This function is required.
     */
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class VersionAttribute : Attribute { public VersionAttribute(int value) { } }

    /**
     * Gets the lowest version of the Script that OpenTTD can still load
     * the savegame of. In other words, from which version until this
     * version can the Script load the savegames.
     *
     * If this function does not exist OpenTTD assumes it can only
     * load savegames of this version. As such it will not upgrade
     * to this version upon load.
     *
     * @returns The lowest version number we load the savegame data.
     * @note This function is optional.
     */
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class MinVersionToLoadAttribute : Attribute { public MinVersionToLoadAttribute(int value) { } }

    /**
     * Gets the development/release date of the Script.
     *
     * The intention of this is to give the user an idea how old the
     * Script is and whether there might be a newer version.
     *
     * @returns The development/release date for the Script.
     * @note This function is required.
     */
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class DateAttribute : Attribute { public DateAttribute(string value) { } }

    /**
     * Can this Script be used as random Script?
     *
     * The idea behind this function is to 'forbid' highly
     * competitive or other special Scripts from running in games unless
     * the user explicitly selects the Script to be loaded. This to
     * try to prevent users from complaining that the Script is too
     * aggressive or does not build profitable routes.
     *
     * If this function does not exist OpenTTD assumes the Script can
     * be used as random Script. As such it will be randomly chosen.
     *
     * @returns True if the Script can be used as random Script.
     * @note This function is optional.
     *
     */
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class UseAsRandomAIAttribute : Attribute { public UseAsRandomAIAttribute(bool value) { } }

    /**
     * Gets the API version this Script is written for. If this function
     * does not exist API compatibility with version 0.7 is assumed.
     * If the function returns something OpenTTD does not understand,
     * for example a newer version or a string that is not a version,
     * the Script will not be loaded.
     *
     * Although in the future we might need to make a separate
     * compatibility 'wrapper' for a specific version of OpenTTD, for
     * example '0.7.1', we will use only the major and minor number
     * and not the bugfix number as valid return for this function.
     *
     * Valid return values are:
     * - "0.7" (for AI only)
     * - "1.0" (for AI only)
     * - "1.1" (for AI only)
     * - "1.2" (for both AI and GS)
     * - "1.3" (for both AI and GS)
     *
     * @returns The version this Script is compatible with.
     */
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class APIVersionAttribute : Attribute { public APIVersionAttribute(string value) { } }

    /**
     * Gets the URL to be shown in the 'this Script has crashed' message
     * and in the 'Available Scripts' window. If this function does not
     * exist no URL will be shown.
     *
     * This function purely exists to redirect users of the Script to the
     * right place on the internet to discuss the Script and report bugs
     * of this Script.
     *
     * @returns The URL to show.
     * @note This function is optional.
     */
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class URLAttribute : Attribute { public URLAttribute(string value) { } }
}
