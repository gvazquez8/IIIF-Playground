using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaygroundClient.Messages;
public sealed class ImageUriParametersChangedMessage : ValueChangedMessage<string>
{
    public ImageUriParametersChangedMessage(string value) : base(value)
    {
    }
}
