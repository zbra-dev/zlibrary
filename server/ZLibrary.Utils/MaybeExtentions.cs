using Impress;

namespace ZLibrary.Utils
{
    public static class MaybeExtentions
    {

        public static T OrNull<T>(this Maybe<T> original)
            where T: class
        {
            return original.Or(null);
        }
    }
}
