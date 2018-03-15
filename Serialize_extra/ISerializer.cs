using System;
namespace Serialize_extra
{
    interface ISerializer
    {
        bool Serialize(Type type, object obj, string fileName);
        object Deserialize(Type type, string fileName);
    }
}
