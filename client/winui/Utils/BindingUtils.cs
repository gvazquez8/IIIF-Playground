using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaygroundClient.Utils;
public class BindingUtils
{
    public static bool IsEmpty(string? str) => string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
    public static bool IsNotEmpty(string? str) => !IsEmpty(str);
}
